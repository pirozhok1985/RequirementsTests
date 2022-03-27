using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RequirementsTests.Domain.Models.InventoryInfoTypes;
using RequirementsTests.Services.UseCases.LinuxInfo;
using Assert = Xunit.Assert;

namespace RequirementsTests.Services.Tests;

[TestClass]
public class HardwareInfoTests
{
    private const string ExpectedCategory = "Hardware";

    [TestMethod]
    public async Task CheckIfGetModelInfoReturnsCorrectValue()
    {
        const string expectedName = "GeneralDeviceInfo";
        var deviceInfo = await new HardwareInfo().GetGeneralDeviceInfoAsync();

        Assert.IsType<GeneralDeviceInfo>(deviceInfo);
        Assert.NotNull(deviceInfo);
        Assert.NotNull(deviceInfo.Model);
        Assert.NotNull(deviceInfo.Vendor);
        Assert.Equal(ExpectedCategory,deviceInfo.Category!.Name);
        Assert.Equal(expectedName, deviceInfo.Name);
    }

    [TestMethod]
    public async Task CheckIfGetRamInfoReturnsCorrectValue()
    {
        const string expectedName = "RamInfo";
        var ramInfo = await new HardwareInfo().GetRamInfoAsync();

        Assert.IsType<RamInfo>(ramInfo);
        Assert.NotNull(ramInfo);
        Assert.NotEqual(0, ramInfo.Free);
        Assert.NotEqual(0, ramInfo.Total);
        Assert.Equal(ExpectedCategory,ramInfo.Category!.Name);
        Assert.Equal(expectedName, ramInfo.Name);
    }  
    
    [TestMethod]
    public async Task CheckIfGetCpuInfoReturnsCorrectValue()
    {
        const string expectedName = "CpuInfo";
        var cpuInfo = await new HardwareInfo().GetCpuInfoAsync();

        Assert.IsType<CpuInfo>(cpuInfo);
        Assert.NotNull(cpuInfo);
        Assert.NotNull(cpuInfo.Model);
        Assert.NotEqual(0,cpuInfo.CoresCount);
        Assert.Equal(ExpectedCategory,cpuInfo.Category!.Name);
        Assert.Equal(expectedName, cpuInfo.Name);
    }

    [TestMethod]
    public async Task CheckIfGetFirmwareInfoReturnsCorrectValue()
    {
        const string expectedName = "FirmwareInfo";
        var firmWareInfo = await new HardwareInfo().GetFirmWareInfoAsync();

        Assert.IsType<FirmwareInfo>(firmWareInfo);
        Assert.NotNull(firmWareInfo);
        Assert.NotNull(firmWareInfo.Date);
        Assert.NotNull(firmWareInfo.Release);
        Assert.NotNull(firmWareInfo.Vendor);
        Assert.NotNull(firmWareInfo.Version);
        Assert.Equal(ExpectedCategory,firmWareInfo.Category!.Name);
        Assert.Equal(expectedName, firmWareInfo.Name);
    }
    
}