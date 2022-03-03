using RequirementsTestsDomain.Models;

namespace RequirementsTestsServices.UseCases.Interfaces;

public interface IGetHardwareInfo
{
    public Task<Info<string>> GetVendorInfoAsync();
    public Task<Info<string>> GetModelInfoAsync();
    public Task<Info<string>> GetSerialNumberInfoAsync();
    public Task<Info<Dictionary<string,long>>> GetRamInfoAsync(string[] keys);
    public Task<Info<Dictionary<string,string>>> GetCpuInfoAsync(string[] keys);
    public Task<Info<Dictionary<string,string>>> GetDiskDriveInfoAsync();
    public Task<Info<Dictionary<string,string>>> GetFirmWareInfoAsync();
    public Task<Info<Dictionary<string,string>>> GetLocalPrinterInfoAsync();
    public Task<Info<Dictionary<string,string>>> GetNetworkPrinterInfoAsync();
}