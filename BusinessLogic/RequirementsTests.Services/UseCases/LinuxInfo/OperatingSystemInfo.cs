using System.Net.NetworkInformation;
using System.Net.Security;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using RequirementsTests.Domain.Models;
using RequirementsTests.Domain.Models.InventoryInfoTypes;
using RequirementsTests.Services.UseCases.Interfaces;

namespace RequirementsTests.Services.UseCases.LinuxInfo;

public class OperatingSystemInfo : IGetOsInfo
{
    private const string CategoryName = "Operating system";

    public async Task<Info<OsInfo>> GetOsInfoAsync()
    {
        var osInfo = new OsInfo();
        var result = await LinuxInfoHelpers.ReadToTheEndAsync("/etc/lsb-release");
        var query = result.Split("\n").Where(e => e != String.Empty)
            .ToDictionary(k => k.Split("=")[0], v => v.Split("=")[1]);
        foreach (var kv in query)
        {
            switch (kv.Key)
            {
                case "DISTRIB_CODENAME":
                    osInfo.CodeName = kv.Value;
                    break;
                case "DISTRIB_ID":
                    osInfo.Id = kv.Value;
                    break;
                case "DISTRIB_RELEASE":
                    osInfo.Release = kv.Value;
                    break;
                case "DISTRIB_DESCRIPTION":
                    osInfo.Description = kv.Value;
                    break;
            }
        }

        return LinuxInfoHelpers.GenerateInfo(osInfo, CategoryName, "Os Info");
    }

    public async Task<Info<IList<NetworkConfigInfo>>> GetNetworkConfigInfoAsync()
    {
        IList<NetworkConfigInfo> result = new List<NetworkConfigInfo>();
        await Task.Run(() =>
        {
            var nics = NetworkInterface.GetAllNetworkInterfaces()
                .Where(i => !i.Name.Contains("lo"))
                .Where(i => !i.Name.Contains("docker"));
            foreach (var nic in nics)
            {
                result.Add( new NetworkConfigInfo()
                {
                    IpAddresses = nic.GetIPProperties().UnicastAddresses.Select(a => a.Address.ToString()).ToArray(),
                    Gateway = nic.GetIPProperties().GatewayAddresses.Select(g => g.Address.ToString()).FirstOrDefault()!,
                    DnsServers = nic.GetIPProperties().DnsAddresses.Select(d => d.ToString()).ToArray(),
                    InterfaceName = nic.Name
                });
            }
        });
        return LinuxInfoHelpers.GenerateInfo(result, CategoryName, "Network Config");
    }

    public async Task<Info<IList<DiskDrivePartitionInfo>>> GetDiskDrivePartitionInfoAsync()
    {
        IList<DiskDrivePartitionInfo> diskDrivePartitionInfoList  = new List<DiskDrivePartitionInfo>();
        var devDirName = Directory.EnumerateDirectories("/sys/block/").Where(d => !d.Contains("loop"));
        foreach (var devDir in devDirName)
        {
             diskDrivePartitionInfoList.Add(await CreateDiskDrivePartitionInfoAsync(devDir));
        }

        return LinuxInfoHelpers.GenerateInfo(diskDrivePartitionInfoList, CategoryName, "Partition configuration");
    }

    private async Task<DiskDrivePartitionInfo> CreateDiskDrivePartitionInfoAsync(string devDir)
    {
        var partInfo = new DiskDrivePartitionInfo();
        partInfo.DiskDriveModel = await LinuxInfoHelpers.ReadToTheEndAsync($@"{devDir}/device/model");
        var partNum = GetPartitionNumber(devDir, out string devName, out nint partListPointer);
        for (int i = 0; i < partNum; i++)
        {
            partInfo.PartUuid = GetPartitionInfo(devName, partListPointer, i, "PartUuid");
            partInfo.PartLabel = GetPartitionInfo(devName, partListPointer, i, "PartLabel");
            partInfo.Label = GetPartitionInfo(devName, partListPointer, i, "LABEL");
            ;
            partInfo.Type = GetPartitionInfo(devName, partListPointer, i, "TYPE");
            partInfo.PartName = devName;
            partInfo.Uuid = GetPartitionInfo(devName, partListPointer, i, "UUID");
        }

        return partInfo;
    }

    private int GetPartitionNumber(string deviceDir, out string devName, out nint partListPointer)
    {
        
        devName = @$"/dev/{deviceDir.Split("/").Last()}";
        var partProbePointer = LinuxInfoHelpers.NewProbeFromFilename(devName);
        partListPointer = LinuxInfoHelpers.GetPartitions(partProbePointer);
        return LinuxInfoHelpers.GetNumberOfPartitions(partListPointer);
    }

    private unsafe string GetPartitionInfo(string devName, nint partListPointer, int partNumber, string lookupInfo)
    {
        var partPointer = LinuxInfoHelpers.GetPartition(partListPointer, partNumber);
        if(string.Equals(lookupInfo, "PartUuid", StringComparison.OrdinalIgnoreCase))
            return Marshal.PtrToStringAnsi((IntPtr) LinuxInfoHelpers.GetPartUuid(partPointer))!;
        if(string.Equals(lookupInfo, "PartLabel", StringComparison.OrdinalIgnoreCase))
            return Marshal.PtrToStringAnsi((IntPtr) LinuxInfoHelpers.GetPartLabel(partPointer))!;
        
        var partName = devName.Contains("nvme") ? $"{devName}p{partNumber + 1}" : $"{devName}{partNumber + 1}";
        var devProbePointer = LinuxInfoHelpers.NewProbeFromFilename(@$"/dev/{partName.Split("/").Last()}");
        LinuxInfoHelpers.DoFullProbe(devProbePointer);
        var retCode1 = LinuxInfoHelpers.LookupValue(devProbePointer, lookupInfo, out string value, IntPtr.Zero);
        return value;
    }

    public async Task<Info<IList<CertificateInfo>>> GetCertificateInfoAsync()
    {
        IList<CertificateInfo> certificateInfoList = new List<CertificateInfo>();
        await Task.Run(() =>
        {
            var certDirectory = Directory.EnumerateFiles("/home/anisimov/Downloads/Certs");
            foreach (var certificate in certDirectory)
            {
                certificateInfoList.Add(GetCertificateInfoFromFile(certificate));
            }
        });

        return LinuxInfoHelpers.GenerateInfo(certificateInfoList, CategoryName, "Root Certificate Information");
    }

    private CertificateInfo GetCertificateInfoFromFile(string pathToFile)
    {
        using var fileStream = new FileStream(pathToFile, FileMode.Open);
        using var memStream = new MemoryStream();
        fileStream.CopyTo(memStream);
        var rawX509CertData = new X509Certificate2(memStream.ToArray());
        var certificateInfo = new CertificateInfo()
        {
            Name = rawX509CertData.FriendlyName,
            EffectiveDate = rawX509CertData.NotBefore,
            ExpirationDate = rawX509CertData.NotAfter
        };
        return certificateInfo;
    }
}