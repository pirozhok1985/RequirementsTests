using RequirementsTestsDomain.Models;

namespace RequirementsTestsServices.UseCases.Interfaces;

public interface IGetOsInfo
{
    public Info<Dictionary<string,string>> GetOsInfo();
    public Info<Dictionary<string,Dictionary<string,string[]>>> GetNetworkConfigInfo();
    public Info<Dictionary<string,string>> GetDiskDrivePartitionInfo();
    // public Info<T> GetDiskDriveEncryptionInfo();
    // public Info<T> GetAdDomainInfo();
    // public Info<T> GetRootCertInfo();
}