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
    public async Task CheckIfReturnCorrectInfo()
    {
        var expectedCategory = "Hardware";
        var expectedName = "Model";
        var expectedValue = "MS-7A38\n";
        var expectedDescription = "For future needs";
        
        var result = await new HardwareInfo().GetModelInfoAsync();

        Assert.IsType<Info<string>>(result);
        Assert.Equal(expectedCategory,result.Category.Name);
        Assert.Equal(expectedDescription,result.Description);
        Assert.Equal(expectedName,result.Name);
        Assert.Equal(expectedValue,result.Value);
    }
}