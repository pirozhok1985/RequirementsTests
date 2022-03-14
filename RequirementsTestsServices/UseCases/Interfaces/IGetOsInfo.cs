using RequirementsTestsDomain.Models;
using RequirementsTestsServices.UseCases.InventoryInfoTypes;

namespace RequirementsTestsServices.UseCases.Interfaces;

public interface IGetOsInfo
{
    public Task<Info<OsInfo>> GetOsInfoAsync();
    public Task<Info<IList<NetworkConfigInfo>>> GetNetworkConfigInfoAsync();
    public Task<Info<IList<DiskDrivePartitionInfo>>> GetDiskDrivePartitionInfoAsync();
    // public Info<T> GetDiskDriveEncryptionInfo();
    // public Info<T> GetAdDomainInfo();
    // public Info<T> GetRootCertInfo();
}