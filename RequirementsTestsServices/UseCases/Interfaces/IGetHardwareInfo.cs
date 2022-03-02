using RequirementsTestsDomain.Models;

namespace RequirementsTestsServices.UseCases.Interfaces;

public interface IGetHardwareInfo
{
    public Info<string> GetVendorInfo();
    public Info<string> GetModelInfo();
    public Info<string> GetSerialNumberInfo();
    public Info<int> GetRamInfo();
    public Info<string> GetCpuInfo();
    public Info<string> GetDiskDriveInfo();
    public Info<string> GetFirmWareInfo();
    public Info<Dictionary<string,string>> GetLocalPrinterInfo();
    public Info<Dictionary<string,string>> GetNetworkPrinterInfo();
}