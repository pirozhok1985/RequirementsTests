using RequirementsTestsDomain.Models;

namespace RequirementsTestsServices.UseCases.Interfaces;

public interface IGetOsInfo
{
    public Task<Info<Dictionary<string,string>>> GetOsInfo();
    public Task<Info<Dictionary<string,Dictionary<string,string[]>>>> GetNetworkConfigInfo();
    public Task<Info<Dictionary<string,string>>> GetDiskDrivePartitionInfo();
    // public Info<T> GetDiskDriveEncryptionInfo();
    // public Info<T> GetAdDomainInfo();
    // public Info<T> GetRootCertInfo();
}