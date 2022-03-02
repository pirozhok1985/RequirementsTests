using RequirementsTestsDomain.Models;
using RequirementsTestsServices.UseCases.Interfaces;

namespace RequirementsTestsServices.UseCases.LinuxInfo;

public class HardwareInfo : IGetHardwareInfo
{
    private const string CategoryName = "Hardware";
    
    public Info<string> GetVendorInfo()
    {
        using var sReader = new StreamReader(@"/sys/devices/virtual/dmi/id/board_vendor");
        var vendorInfo = new Info<string>
        {
            Category = new InfoCategory{Name = CategoryName},
            Value = sReader.ReadToEnd(),
            Description = "For future needs",
        };
        return vendorInfo;
    }

    public Info<string> GetModelInfo()
    {
        throw new NotImplementedException();
    }

    public Info<string> GetSerialNumberInfo()
    {
        throw new NotImplementedException();
    }

    public Info<int> GetRamInfo()
    {
        throw new NotImplementedException();
    }

    public Info<string> GetCpuInfo()
    {
        throw new NotImplementedException();
    }

    public Info<string> GetDiskDriveInfo()
    {
        throw new NotImplementedException();
    }

    public Info<string> GetFirmWareInfo()
    {
        throw new NotImplementedException();
    }

    public Info<Dictionary<string, string>> GetLocalPrinterInfo()
    {
        throw new NotImplementedException();
    }

    public Info<Dictionary<string, string>> GetNetworkPrinterInfo()
    {
        throw new NotImplementedException();
    }
}