using RequirementsTestsDomain.Models;
using RequirementsTestsDomain.Models.InventoryInfoTypes;

namespace RequirementsTestsServices.UseCases.Interfaces;

public interface IGetOsInfo
{
    public Task<Info<OsInfo>> GetOsInfoAsync();
    public Task<Info<IList<NetworkConfigInfo>>> GetNetworkConfigInfoAsync();
    public Task<Info<IList<DiskDrivePartitionInfo>>> GetDiskDrivePartitionInfoAsync();
    public Task<Info<IList<CertificateInfo>>> GetCertificateInfoAsync();
    // public Info<T> GetDiskDriveEncryptionInfo();
    // public Info<T> GetAdDomainInfo();
}