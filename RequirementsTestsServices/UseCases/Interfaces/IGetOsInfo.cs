using RequirementsTestsDomain.Models;

namespace RequirementsTestsServices.UseCases.Interfaces;

public interface IGetOsInfo<T>
{
    public Info<T> GetOsInfo();
    public Info<T> GetImageVersionInfo();
    public Info<T> GetNetworkConfigInfo();
    public Info<T> GetDiskDrivePartitionInfo();
    public Info<T> GetDiskDriveEncryptionInfo();
    public Info<T> GetAdDomainInfo();
    public Info<T> GetRootCertInfo();
}