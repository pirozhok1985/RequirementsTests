using System.Globalization;
using RequirementsTestsDomain.Models;
using RequirementsTestsServices.UseCases.Interfaces;

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

    public async Task<Info<Dictionary<string, long>>> GetRamInfoAsync(string[] keys)
    {
        var result = new Dictionary<string, long>();
        using var sReader = new StreamReader(@"/proc/meminfo");
        var count = 0;
        while (!sReader.EndOfStream && count < keys.Length)
        {
            var str = await sReader.ReadLineAsync();
            for (int i = 0; i < keys.Length; i++)
            {
                if (str!.Contains(keys[i]))
                {
                    long value;
                    string strValue = str.Split(":")[1].Trim().Split(" ")[0];
                    result.Add(keys[i], value = long.TryParse(strValue, out var val) ? val : default);
                    count++;
                }
            }
        }
        
        return LinuxInfoHelpers.GenerateInfo(result,CategoryName,"Ram");
    }

    public async Task<Info<Dictionary<string, string>>> GetCpuInfoAsync(string[] keys)
    {
        var result = new Dictionary<string, string>();
        using var sReader = new StreamReader(@"/proc/cpuinfo");
        var count = 0;
        while (!sReader.EndOfStream && count < keys.Length)
        {
            var str = await sReader.ReadLineAsync();
            for (int i = 0; i < keys.Length; i++)
            {
                if (str!.Contains(keys[i]))
                {
                    string strValue = str.Split(":")[1].Trim();
                    result.Add(keys[i], strValue);
                    count++;
                }
            }
        }
        
        return LinuxInfoHelpers.GenerateInfo(result,CategoryName,"Cpu");
    }

    public async Task<Info<Dictionary<string, string>>> GetDiskDriveInfoAsync()
    {
        var dirs = Directory.EnumerateDirectories("/sys/block/").Where(d => !d.Contains("loop"));
        var result = new Dictionary<string, string>();
        foreach (var dir in dirs)
        {
            var key = dir.Split("/").Last();
            var value = await LinuxInfoHelpers.ReadToTheEndAsync($@"{dir}/device/model");
            result.Add(key!,value);
        }

        return LinuxInfoHelpers.GenerateInfo(result,CategoryName,"Disk");
    }

    public async Task<Info<Dictionary<string, string>>> GetFirmWareInfoAsync()
    {
        var result = new Dictionary<string, string>();
        string[] keys = {"bios_version", "bios_vendor", "bios_release", "bios_date"};
        foreach (var key in keys)
        {
            var value = await LinuxInfoHelpers.ReadToTheEndAsync($@"/sys/devices/virtual/dmi/id/{key}");
            result.Add(key,value);
        }

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