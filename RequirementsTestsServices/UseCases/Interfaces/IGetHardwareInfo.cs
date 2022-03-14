using RequirementsTestsDomain.Models;
using RequirementsTestsServices.UseCases.InventoryInfoTypes;

namespace RequirementsTestsServices.UseCases.Interfaces;

public interface IGetHardwareInfo
{
    public Task<Info<string>> GetVendorInfoAsync();
    public Task<Info<string>> GetModelInfoAsync();
    public Task<Info<string>> GetSerialNumberInfoAsync();
    public Task<Info<RamInfo>> GetRamInfoAsync();
    public Task<Info<CpuInfo>> GetCpuInfoAsync();
    public Task<Info<FirmwareInfo>> GetFirmWareInfoAsync();
    public Task<Info<Dictionary<string,string>>> GetLocalPrinterInfoAsync();
    public Task<Info<Dictionary<string,string>>> GetNetworkPrinterInfoAsync();
}