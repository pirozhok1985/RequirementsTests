using System.Globalization;
using RequirementsTests.Domain.Models;
using RequirementsTests.Domain.Models.InventoryInfoTypes;
using RequirementsTests.Services.UseCases.Interfaces;

namespace RequirementsTests.Services.UseCases.LinuxInfo;

public class HardwareInfo : IGetHardwareInfo
{
    private const string CategoryName = "Hardware";

    public async Task<GeneralDeviceInfo> GetGeneralDeviceInfoAsync()
    {
        var generalDeviceInfo =
            LinuxInfoHelpers.GenerateInfo(new GeneralDeviceInfo(), CategoryName, nameof(GeneralDeviceInfo)) as GeneralDeviceInfo;
        var vendor = (await LinuxInfoHelpers.ReadToTheEndAsync(@"/sys/devices/virtual/dmi/id/board_vendor")).Replace("\n","");
        var model = (await LinuxInfoHelpers.ReadToTheEndAsync(@"/sys/devices/virtual/dmi/id/product_name")).Replace("\n","");
        // var serial = (await LinuxInfoHelpers.ReadToTheEndAsync(@"/sys/devices/virtual/dmi/id/product_serial")).Replace("\n","");
        generalDeviceInfo!.Vendor = vendor;
        generalDeviceInfo.Model = model;
        // generalDeviceInfo.SerialNumber = serial;
        return generalDeviceInfo;
    }

    public async Task<RamInfo> GetRamInfoAsync()
    {
        var ramInfo = LinuxInfoHelpers.GenerateInfo(new RamInfo(),CategoryName,nameof(RamInfo)) as RamInfo;
        using var sReader = new StreamReader(@"/proc/meminfo");
        while (!sReader.EndOfStream)
        {
            var str = await sReader.ReadLineAsync();
            var strVal = str?.Split(":")[1].Trim().Split(" ")[0];
            if (str!.Contains("MemTotal")) ramInfo!.Total = long.TryParse(strVal, out var val) ? val : default;
            else if (str.Contains("MemFree")) ramInfo!.Free = long.TryParse(strVal, out var val) ? val : default;
        }

        return ramInfo!;
    }

    public async Task<CpuInfo> GetCpuInfoAsync()
    {
        var cpuInfo = LinuxInfoHelpers.GenerateInfo(new CpuInfo(),CategoryName,nameof(CpuInfo)) as CpuInfo;;
        using var sReader = new StreamReader(@"/proc/cpuinfo");
        do
        {
            var str = await sReader.ReadLineAsync();
            if (str!.Contains("model name")) cpuInfo!.Model = str.Split(":")[1].Trim();
            else if (str!.Contains("cpu cores")) cpuInfo!.CoresCount = int.TryParse(str.Split(":")[1].Trim(), out int val) ? val : default;
        } while (!sReader.EndOfStream && cpuInfo!.CoresCount == 0);
        
        return cpuInfo!;
    }

    public async Task<FirmwareInfo> GetFirmWareInfoAsync()
    {
        var firmWareInfo =
            LinuxInfoHelpers.GenerateInfo(new FirmwareInfo(), CategoryName, nameof(FirmwareInfo)) as FirmwareInfo;
        firmWareInfo!.Date = (await LinuxInfoHelpers.ReadToTheEndAsync($@"/sys/devices/virtual/dmi/id/bios_date")).Replace("\n","");
        firmWareInfo.Release = (await LinuxInfoHelpers.ReadToTheEndAsync($@"/sys/devices/virtual/dmi/id/bios_release")).Replace("\n","");
        firmWareInfo.Vendor = (await LinuxInfoHelpers.ReadToTheEndAsync($@"/sys/devices/virtual/dmi/id/bios_vendor")).Replace("\n","");
        firmWareInfo.Version = (await LinuxInfoHelpers.ReadToTheEndAsync($@"/sys/devices/virtual/dmi/id/bios_version")).Replace("\n","");

        return firmWareInfo;
    }
}