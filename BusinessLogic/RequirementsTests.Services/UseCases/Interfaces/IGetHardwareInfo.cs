using RequirementsTests.Domain.Models;
using RequirementsTests.Domain.Models.InventoryInfoTypes;

namespace RequirementsTests.Services.UseCases.Interfaces;

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