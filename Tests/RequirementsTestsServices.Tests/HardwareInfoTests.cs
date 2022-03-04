using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RequirementsTestsDomain.Models;
using RequirementsTestsServices.UseCases.LinuxInfo;
using Assert = Xunit.Assert;

namespace RequirementsTestsServices.Tests;

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
    
    [TestMethod]
    public async Task CheckIfGetSerialNumberInfoReturnsCorrectValue() //Permission denied - Test Failed!
    {
        var resultSerial = await new HardwareInfo().GetSerialNumberInfoAsync();

        Assert.IsType<Info<string>>(resultSerial);
        Assert.NotEmpty(resultSerial.Value!);
        Assert.Equal(ExpectedCategory,resultSerial.Category!.Name);
    }  
    
    [TestMethod]
    public async Task CheckIfGetRamInfoReturnsCorrectValue()
    {
        var resultRam = await new HardwareInfo().GetRamInfoAsync(new[] {"MemTotal","MemFree","Mapped"});

        Assert.IsType<Info<Dictionary<string,long>>>(resultRam);
        Assert.NotEmpty(resultRam.Value!);
        Assert.Equal(ExpectedCategory,resultRam.Category!.Name);
    }  
    
    [TestMethod]
    public async Task CheckIfGetCpuInfoReturnsCorrectValue()
    {
        var resultCpu = await new HardwareInfo().GetCpuInfoAsync(new[] {"model name", "cpu cores"});

        Assert.IsType<Info<Dictionary<string,string>>>(resultCpu);
        Assert.NotEmpty(resultCpu.Value!);
        Assert.Equal(ExpectedCategory,resultCpu.Category!.Name);
    }    
    
    [TestMethod]
    public async Task CheckIfGetDiskDriveInfoReturnsCorrectValue()
    {
        var resultDiskDrive = await new HardwareInfo().GetDiskDriveInfoAsync();

        Assert.IsType<Info<Dictionary<string,string>>>(resultDiskDrive);
        Assert.NotEmpty(resultDiskDrive.Value!);
        Assert.Equal(ExpectedCategory,resultDiskDrive.Category!.Name);
    }   
    
    [TestMethod]
    public async Task CheckIfGetFirmwareInfoReturnsCorrectValue()
    {
        var resultFirmWare = await new HardwareInfo().GetFirmWareInfoAsync();

        Assert.IsType<Info<Dictionary<string,string>>>(resultFirmWare);
        Assert.NotEmpty(resultFirmWare.Value!);
        Assert.Equal(ExpectedCategory,resultFirmWare.Category!.Name);
    }
    
}