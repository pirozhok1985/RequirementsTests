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
    [TestMethod]
    public async Task CheckIfReturnsCorrectValue()
    {
        var expectedCategory = "Hardware";

        var resultModel = await new HardwareInfo().GetModelInfoAsync();
        var resultRam = await new HardwareInfo().GetRamInfoAsync(new[] {"MemTotal","MemFree","Mapped"});

        Assert.IsType<Info<string>>(resultModel);
        Assert.NotEmpty(resultModel.Value!);
        Assert.Equal(expectedCategory,resultModel.Category!.Name);

        Assert.IsType<Info<Dictionary<string,long>>>(resultRam);
        Assert.NotEmpty(resultRam.Value!);
        Assert.Equal(expectedCategory,resultRam.Category!.Name);
    }
}