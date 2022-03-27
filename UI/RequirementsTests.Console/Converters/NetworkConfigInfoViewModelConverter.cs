using RequirementsTests.Console.ViewModels;
using RequirementsTests.Domain.Models.InventoryInfoTypes;

namespace RequirementsTests.Console.Converters;

public static class NetworkConfigInfoViewModelConverter
{
    public static NetworkConfigInfoViewModel ToViewModel(this NetworkConfigInfo networkConfigInfo)
    {
        return new NetworkConfigInfoViewModel()
        {
            Gateway = networkConfigInfo.Gateway,
            DnsServers = networkConfigInfo.DnsServers,
            InterfaceName = networkConfigInfo.InterfaceName,
            IpAddresses = networkConfigInfo.IpAddresses,
        };
    }
    
    public static IList<NetworkConfigInfoViewModel> ToViewModel(this IList<NetworkConfigInfo> networkConfigInfos) =>
        networkConfigInfos.Select(n => n.ToViewModel()).ToList();
}