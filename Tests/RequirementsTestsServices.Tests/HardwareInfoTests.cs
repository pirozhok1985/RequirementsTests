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
    public async Task CheckIfGetModelInfoReturnsCorrectValue()
    {
        var expectedCategory = "Hardware";
        var expectedName = "Model";

        var result = await new HardwareInfo().GetModelInfoAsync();

        Assert.IsType<Info<string>>(result);
        Assert.Equal(expectedCategory,result.Category.Name);
        Assert.Equal(expectedName,result.Name);
    }
}