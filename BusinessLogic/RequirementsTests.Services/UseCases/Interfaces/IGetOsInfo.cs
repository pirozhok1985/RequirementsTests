using RequirementsTests.Domain.Models;
using RequirementsTests.Domain.Models.InventoryInfoTypes;

namespace RequirementsTests.Services.UseCases.Interfaces;

public interface IGetOsInfo
{
    public Task<OsInfo> GetOsInfoAsync();
    public Task<IList<NetworkConfigInfo>> GetNetworkConfigInfoAsync();
    public Task<IList<DiskDrivePartitionInfo>> GetDiskDrivePartitionInfoAsync();
    public Task<IList<CertificateInfo>> GetCertificateInfoAsync();
    // public Info<T> GetDiskDriveEncryptionInfo();
    // public Info<T> GetAdDomainInfo();
}