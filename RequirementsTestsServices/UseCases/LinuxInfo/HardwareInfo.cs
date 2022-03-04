using System.Globalization;
using RequirementsTestsDomain.Models;
using RequirementsTestsServices.UseCases.Interfaces;

namespace RequirementsTestsServices.UseCases.LinuxInfo;

public class HardwareInfo : IGetHardwareInfo
{
    private const string CategoryName = "Hardware";

    private async Task<string> ReadToTheEndAsync(string path)
    {
        using var sReader = new StreamReader(path);
        return await sReader.ReadToEndAsync();
    }

    private Info<Dictionary<TKey,TValue>> GenerateInfo<TKey,TValue>(Dictionary<TKey,TValue> dictionary, string name, string description = "For future needs")
    {
        var info = new Info<Dictionary<TKey, TValue>>
        {
            Name = name,
            Category = new InfoCategory{Name = CategoryName},
            Value = dictionary,
            Description = description,
        };
        return info;
    }

    private Info<T> GenerateInfo<T>(T value, string name, string description = "For future needs")
    {
        var info = new Info<T>
        {
            Name = name,
            Category = new InfoCategory{Name = CategoryName},
            Value = value,
            Description = description,
        };
        return info;
    }

    public async Task<Info<string>> GetVendorInfoAsync()
    {
        var value = await ReadToTheEndAsync(@"/sys/devices/virtual/dmi/id/board_vendor");
        return GenerateInfo(value,"Vendor");
    }

    public async Task<Info<string>> GetModelInfoAsync()
    {
        var value = await ReadToTheEndAsync(@"/sys/devices/virtual/dmi/id/product_name");
        return GenerateInfo(value,"Model");
    }

    public async Task<Info<string>> GetSerialNumberInfoAsync()
    {
        try
        {
            var value = await ReadToTheEndAsync(@"/sys/devices/virtual/dmi/id/product_serial");
            return GenerateInfo(value,"Serial Number");
        }
        catch (IOException e)
        {
            return GenerateInfo(e.Message,"Serial Number");
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
        
        return GenerateInfo(result,"Ram");
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
        
        return GenerateInfo(result,"Cpu");
    }

    public async Task<Info<Dictionary<string, string>>> GetDiskDriveInfoAsync()
    {
        var dirs = Directory.EnumerateDirectories("/sys/block/").Where(d => !d.Contains("loop"));
        var result = new Dictionary<string, string>();
        foreach (var dir in dirs)
        {
            var key = dir.Split("/").Last();
            var value = await ReadToTheEndAsync($@"{dir}/device/model");
            result.Add(key!,value);
        }

        return GenerateInfo(result,"Disk");
    }

    public async Task<Info<Dictionary<string, string>>> GetFirmWareInfoAsync()
    {
        var result = new Dictionary<string, string>();
        string[] keys = {"bios_version", "bios_vendor", "bios_release", "bios_date"};
        foreach (var key in keys)
        {
            var value = await ReadToTheEndAsync($@"/sys/devices/virtual/dmi/id/{key}");
            result.Add(key,value);
        }

        return GenerateInfo(result,"Bios");
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