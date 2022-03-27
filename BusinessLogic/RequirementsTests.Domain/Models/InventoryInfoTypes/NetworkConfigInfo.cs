using RequirementsTests.Domain.Base;

namespace RequirementsTests.Domain.Models.InventoryInfoTypes;

public class NetworkConfigInfo : BaseInfo, IBaseInfo
{
    public string[]? IpAddresses { get; set; }
    public string? Gateway { get; set; }
    public string[]? DnsServers { get; set; }
    public string? InterfaceName { get; set; }
}