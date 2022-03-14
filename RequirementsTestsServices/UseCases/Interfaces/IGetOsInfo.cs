using RequirementsTestsDomain.Models;
using RequirementsTestsServices.UseCases.InventoryInfoTypes;

namespace RequirementsTestsServices.UseCases.Interfaces;

public interface IGetOsInfo
{
    public Task<Info<OsInfo>> GetOsInfo();
    public Task<Info<IList<NetworkConfigInfo>>> GetNetworkConfigInfo();
    public Task<Info<IList<DiskDrivePartitionInfo>>> GetDiskDrivePartitionInfo();
    // public Info<T> GetDiskDriveEncryptionInfo();
    // public Info<T> GetAdDomainInfo();
    // public Info<T> GetRootCertInfo();
}