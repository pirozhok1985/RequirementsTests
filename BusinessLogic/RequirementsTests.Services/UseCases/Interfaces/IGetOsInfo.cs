using RequirementsTests.Domain.Models;
using RequirementsTests.Domain.Models.InventoryInfoTypes;

namespace RequirementsTests.Services.UseCases.Interfaces;

public interface IGetOsInfo
{
    public Task<Info<OsInfo>> GetOsInfoAsync();
    public Task<Info<IList<NetworkConfigInfo>>> GetNetworkConfigInfoAsync();
    public Task<Info<IList<DiskDrivePartitionInfo>>> GetDiskDrivePartitionInfoAsync();
    public Task<Info<IList<CertificateInfo>>> GetCertificateInfoAsync();
    // public Info<T> GetDiskDriveEncryptionInfo();
    // public Info<T> GetAdDomainInfo();
}