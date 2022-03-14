using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using RequirementsTestsDomain.Models;
using RequirementsTestsServices.UseCases.Interfaces;
using RequirementsTestsServices.UseCases.InventoryInfoTypes;

namespace RequirementsTestsServices.UseCases.LinuxInfo;

public class OperatingSystemInfo : IGetOsInfo
{
    private const string CategoryName = "Operating system";
    public async Task<Info<OsInfo>> GetOsInfo()
    {
        var osInfo = new OsInfo();
        var result = await LinuxInfoHelpers.ReadToTheEndAsync("/etc/lsb-release");
        var query = result.Split("\n").Where(e => e != String.Empty)
            .ToDictionary(k => k.Split("=")[0],v => v.Split("=")[1]);
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
                    osInfo.Id = kv.Value;
                    break;
                case "DISTRIB_DESCRIPTION":
                    osInfo.Id = kv.Value;
                    break;
            }
        }
        return LinuxInfoHelpers.GenerateInfo(osInfo, CategoryName, "Os Info");
    }

    public async Task<Info<IList<NetworkConfigInfo>>> GetNetworkConfigInfo()
    {
        IList<NetworkConfigInfo> result = new List<NetworkConfigInfo>();
        var netConfig = new NetworkConfigInfo();
        await Task.Run(() =>
        {
            var nics = NetworkInterface.GetAllNetworkInterfaces()
                .Where(i => !i.Name.Contains("lo"))
                .Where(i => !i.Name.Contains("docker"));
            foreach (var nic in nics)
            {
                netConfig.IpAddresses = nic.GetIPProperties().UnicastAddresses.Select(a => a.Address.ToString()).ToArray();
                netConfig.Gateway = nic.GetIPProperties().GatewayAddresses.Select(g => g.Address.ToString()).FirstOrDefault()!;
                netConfig.DnsServers = nic.GetIPProperties().DnsAddresses.Select(d => d.ToString()).ToArray();
                netConfig.InterfaceName = nic.Name;
                result.Add(netConfig);
            }
        });
        return LinuxInfoHelpers.GenerateInfo(result,CategoryName,"Network Config");
    }
    
    public async Task<Info<IList<DiskDrivePartitionInfo>>> GetDiskDrivePartitionInfo()
    {
        IList<DiskDrivePartitionInfo> result = new List<DiskDrivePartitionInfo>();
        await Task.Run(() =>
        {
            var partInfo = new DiskDrivePartitionInfo();
            var devNameDir = Directory.EnumerateDirectories("/sys/block/").Where(d => !d.Contains("loop"));
            foreach (var devDir in devNameDir)
            {
                var devName = @$"/dev/{devDir.Split("/").Last()}";
                var partProbeRef = LinuxInfoHelpers.NewProbeFromFilename(devName);
                var pList = LinuxInfoHelpers.GetPartitions(partProbeRef);
                var partNum = LinuxInfoHelpers.GetNumberOfPartitions(pList);
                for (int i = 0; i < partNum; i++)
                {
                    var pPart = LinuxInfoHelpers.GetPartition(pList, i);
                    unsafe
                    {
                        partInfo.PartUuid = Marshal.PtrToStringAnsi((IntPtr) LinuxInfoHelpers.GetUuid(pPart))!;
                        partInfo.PartLabel = Marshal.PtrToStringAnsi((IntPtr) LinuxInfoHelpers.GetName(pPart))!;
                    }

                    var partName = devName.Contains("nvme") ? $"{devName}p{i + 1}" : $"{devName}{i + 1}";
                    var devProbeRef = LinuxInfoHelpers.NewProbeFromFilename(@$"/dev/{partName.Split("/").Last()}");
                    LinuxInfoHelpers.DoFullProbe(devProbeRef);
                    var retCode1 = LinuxInfoHelpers.LookupValue(devProbeRef, "LABEL", out string label, IntPtr.Zero);
                    var retCode2 = LinuxInfoHelpers.LookupValue(devProbeRef, "UUID", out string uuid, IntPtr.Zero);
                    var retCode5 = LinuxInfoHelpers.LookupValue(devProbeRef, "TYPE", out string type, IntPtr.Zero);
                    partInfo.Label = label;
                    partInfo.Type = type;
                    partInfo.Name = partName;
                    partInfo.Uuid = uuid;
                }
                result.Add(partInfo);
            }
        });
        return LinuxInfoHelpers.GenerateInfo(result,CategoryName,"Partition configuration");;
    }
}