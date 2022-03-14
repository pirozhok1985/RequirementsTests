using System.Globalization;
using RequirementsTestsDomain.Models;
using RequirementsTestsServices.UseCases.Interfaces;
using RequirementsTestsServices.UseCases.InventoryInfoTypes;

namespace RequirementsTestsServices.UseCases.LinuxInfo;

public class HardwareInfo : IGetHardwareInfo
{
    private const string CategoryName = "Hardware";

    public async Task<Info<string>> GetVendorInfoAsync()
    {
        var value = await LinuxInfoHelpers.ReadToTheEndAsync(@"/sys/devices/virtual/dmi/id/board_vendor");
        return LinuxInfoHelpers.GenerateInfo(value,CategoryName,"Vendor");
    }

    public async Task<Info<string>> GetModelInfoAsync()
    {
        var value = await LinuxInfoHelpers.ReadToTheEndAsync(@"/sys/devices/virtual/dmi/id/product_name");
        return LinuxInfoHelpers.GenerateInfo(value,CategoryName,"Model");
    }

    public async Task<Info<string>> GetSerialNumberInfoAsync()
    {
        try
        {
            var value = await LinuxInfoHelpers.ReadToTheEndAsync(@"/sys/devices/virtual/dmi/id/product_serial");
            return LinuxInfoHelpers.GenerateInfo(value,CategoryName,"Serial Number");
        }
        catch (IOException e)
        {
            return LinuxInfoHelpers.GenerateInfo(e.Message,CategoryName,"Serial Number");
        }
    }

    public async Task<Info<RamInfo>> GetRamInfoAsync()
    {
        var result = new RamInfo();
        using var sReader = new StreamReader(@"/proc/meminfo");
        while (!sReader.EndOfStream)
        {
            var str = await sReader.ReadLineAsync();
            var strVal = str?.Split(":")[1].Trim().Split(" ")[0];
            if (str!.Contains("MemTotal")) result.Total = long.TryParse(strVal, out var val) ? val : default;
            else if (str!.Contains("MemFree")) result.Free = long.TryParse(strVal, out var val) ? val : default;
        }
        return LinuxInfoHelpers.GenerateInfo(result,CategoryName,"Ram");
    }

    public async Task<Info<CpuInfo>> GetCpuInfoAsync()
    {
        var result = new CpuInfo();
        using var sReader = new StreamReader(@"/proc/cpuinfo");
        var str = String.Empty;
        do
        {
            str = await sReader.ReadLineAsync();
            if (str!.Contains("model name")) result.Model = str.Split(":")[1].Trim();
            else if (str!.Contains("cpu cores")) result.CoresCount = int.TryParse(str.Split(":")[1].Trim(), out int val) ? val : default;
        } while (!sReader.EndOfStream && result.CoresCount == 0);
        
        return LinuxInfoHelpers.GenerateInfo(result,CategoryName,"Cpu");
    }

    public async Task<Info<FirmwareInfo>> GetFirmWareInfoAsync()
    {
        var result = new FirmwareInfo
        {
            Date = await LinuxInfoHelpers.ReadToTheEndAsync($@"/sys/devices/virtual/dmi/id/bios_date"),
            Release = await LinuxInfoHelpers.ReadToTheEndAsync($@"/sys/devices/virtual/dmi/id/bios_release"),
            Vendor = await LinuxInfoHelpers.ReadToTheEndAsync($@"/sys/devices/virtual/dmi/id/bios_vendor"),
            Version = await LinuxInfoHelpers.ReadToTheEndAsync($@"/sys/devices/virtual/dmi/id/bios_version")
        };

        return LinuxInfoHelpers.GenerateInfo(result,CategoryName,"Bios");
    }

    public Task<Info<Dictionary<string, string>>> GetLocalPrinterInfoAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Info<Dictionary<string, string>>> GetNetworkPrinterInfoAsync()
    {
        throw new NotImplementedException();
    }
}