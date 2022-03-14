namespace RequirementsTestsServices.UseCases.InventoryInfoTypes;

public class NetworkConfigInfo
{
    public string[] IpAddresses { get; set; }
    public string Gateway { get; set; } = String.Empty;
    public string[]? DnsServers { get; set; }
    public string InterfaceName { get; set; } = String.Empty;
}