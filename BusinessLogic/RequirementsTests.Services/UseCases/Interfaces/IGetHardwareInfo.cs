using RequirementsTests.Domain.Models;
using RequirementsTests.Domain.Models.InventoryInfoTypes;

namespace RequirementsTests.Services.UseCases.Interfaces;

public interface IGetHardwareInfo
{
    public Task<GeneralDeviceInfo> GetGeneralDeviceInfoAsync();
    public Task<RamInfo> GetRamInfoAsync();
    public Task<CpuInfo> GetCpuInfoAsync();
    public Task<FirmwareInfo> GetFirmWareInfoAsync();
    //TODO:
    // public Task<Dictionary<string,string>> GetLocalPrinterInfoAsync();
    // public Task<Dictionary<string,string>> GetNetworkPrinterInfoAsync();
}