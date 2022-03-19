using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RequirementsTests.Domain.Models;
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
        var resultModel = await new HardwareInfo().GetModelInfoAsync();

        Assert.IsType<Info<string>>(resultModel);
        Assert.NotEmpty(resultModel.Value!);
        Assert.Equal(ExpectedCategory,resultModel.Category!.Name);
    }  
    
    [TestMethod]
    public async Task CheckIfGetVendorInfoReturnsCorrectValue()
    {
        var resultVendor = await new HardwareInfo().GetVendorInfoAsync();

        Assert.IsType<Info<string>>(resultVendor);
        Assert.NotEmpty(resultVendor.Value!);
        Assert.Equal(ExpectedCategory,resultVendor.Category!.Name);
    }    
    
    // [TestMethod]
    // public async Task CheckIfGetSerialNumberInfoReturnsCorrectValue() //Permission denied - Test Failed!
    // {
    //     var resultSerial = await new HardwareInfo().GetSerialNumberInfoAsync();
    //
    //     Assert.IsType<Info<string>>(resultSerial);
    //     Assert.NotEmpty(resultSerial.Value!);
    //     Assert.Equal(ExpectedCategory,resultSerial.Category!.Name);
    // }  
    
    [TestMethod]
    public async Task CheckIfGetRamInfoReturnsCorrectValue()
    {
        var resultRam = await new HardwareInfo().GetRamInfoAsync();

        Assert.IsType<Info<RamInfo>>(resultRam);
        Assert.NotNull(resultRam.Value);
        Assert.NotEqual(0, resultRam.Value?.Free);
        Assert.NotEqual(0, resultRam.Value?.Total);
        Assert.Equal(ExpectedCategory,resultRam.Category!.Name);
    }  
    
    [TestMethod]
    public async Task CheckIfGetCpuInfoReturnsCorrectValue()
    {
        var resultCpu = await new HardwareInfo().GetCpuInfoAsync();

        Assert.IsType<Info<CpuInfo>>(resultCpu);
        Assert.NotNull(resultCpu.Value);
        Assert.NotNull(resultCpu.Value?.Model);
        Assert.NotEqual(0,resultCpu.Value?.CoresCount);
        Assert.Equal(ExpectedCategory,resultCpu.Category!.Name);
    }

    [TestMethod]
    public async Task CheckIfGetFirmwareInfoReturnsCorrectValue()
    {
        var resultFirmWare = await new HardwareInfo().GetFirmWareInfoAsync();

        Assert.IsType<Info<FirmwareInfo>>(resultFirmWare);
        Assert.NotNull(resultFirmWare.Value);
        Assert.NotNull(resultFirmWare.Value?.Date);
        Assert.NotNull(resultFirmWare.Value?.Release);
        Assert.NotNull(resultFirmWare.Value?.Vendor);
        Assert.NotNull(resultFirmWare.Value?.Version);
        Assert.Equal(ExpectedCategory,resultFirmWare.Category!.Name);
    }
    
}