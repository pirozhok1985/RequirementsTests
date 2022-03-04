using System.Net.NetworkInformation;
using RequirementsTestsDomain.Models;
using RequirementsTestsServices.UseCases.Interfaces;

namespace RequirementsTestsServices.UseCases.LinuxInfo;

public class OperatingSystemInfo : IGetOsInfo
{
    private const string CategoryName = "Operating system";
    public async Task<Info<Dictionary<string, string>>> GetOsInfo()
    {
        var result = await LinuxInfoHelpers.ReadToTheEndAsync("/etc/lsb-release");
        var query = result.Split("\n").Where(e => e != String.Empty).ToDictionary(k => k.Split("=")[0],v => v.Split("=")[1]);
        return LinuxInfoHelpers.GenerateInfo(query, CategoryName, "Os Info");
    }

    public async Task<Info<Dictionary<string, Dictionary<string,string[]>>>> GetNetworkConfigInfo()
    {
        var result = new Dictionary<string, Dictionary<string,string[]>>();
        await Task.Run(() =>
        {
            var nics = NetworkInterface.GetAllNetworkInterfaces()
                .Where(i => !i.Name.Contains("lo"))
                .Where(i => !i.Name.Contains("docker"));
            foreach (var nic in nics)
            {
                var dict = new Dictionary<string, string[]>();
                dict.Add("Ip Address", nic.GetIPProperties().UnicastAddresses.Select(a => a.Address.ToString()).ToArray());
                dict.Add("Gateway", nic.GetIPProperties().GatewayAddresses.Select(g => g.Address.ToString()).ToArray());
                dict.Add("Dns Servers", nic.GetIPProperties().DnsAddresses.Select(d => d.ToString()).ToArray());
                result.Add(nic.Name,dict);
            }
        });
        return LinuxInfoHelpers.GenerateInfo(result,CategoryName,"Network Config");
    }

    public Task<Info<Dictionary<string, string>>> GetDiskDrivePartitionInfo()
    {
        throw new NotImplementedException();
    }
}