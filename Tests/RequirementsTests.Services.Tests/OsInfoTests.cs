using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RequirementsTests.Domain.Models;
using RequirementsTests.Domain.Models.InventoryInfoTypes;
using RequirementsTests.Services.UseCases.LinuxInfo;
using Assert = Xunit.Assert;
namespace RequirementsTests.Services.Tests;

[TestClass]
public class OsInfoTests
{
    private const string ExpectedCategory = "Operating system";

    [TestMethod]
    public async Task CheckIfGetOsInfoReturnsCorrectValue()
    {
        var osInfo = await new OperatingSystemInfo().GetOsInfoAsync();

        Assert.IsType<Info<OsInfo>>(osInfo);
        Assert.NotNull(osInfo.Value);
        Assert.NotNull(osInfo.Value?.CodeName);
        Assert.NotNull(osInfo.Value?.Description);
        Assert.NotNull(osInfo.Value?.Release);
        Assert.NotNull(osInfo.Value?.Id);
        Assert.Equal(ExpectedCategory,osInfo.Category!.Name);
    }  
    
    [TestMethod]
    public async Task CheckIfGetNetworkConfigInfoReturnsCorrectValue()
    {
        var networkConfigInfoList = await new OperatingSystemInfo().GetNetworkConfigInfoAsync();
        var networkConfigInfo = networkConfigInfoList.Value?.Where(ni => ni.Gateway is not null).FirstOrDefault();
            
        Assert.IsType<NetworkConfigInfo>(networkConfigInfo);
        Assert.NotNull(networkConfigInfoList);
        Assert.NotNull(networkConfigInfo?.InterfaceName);
        Assert.NotEmpty(networkConfigInfo?.IpAddresses!);
        Assert.NotEmpty(networkConfigInfo?.DnsServers!);
        
        Assert.Equal(ExpectedCategory,networkConfigInfoList.Category!.Name);
    }  
    
    [TestMethod]
    public async Task CheckIfGetDiskDrivePartitionInfoReturnsCorrectValue()
    {
        var partInfoList = await new OperatingSystemInfo().GetDiskDrivePartitionInfoAsync();
        var partitionInfo = partInfoList.Value?.FirstOrDefault();
        
        Assert.IsType<DiskDrivePartitionInfo>(partitionInfo);
        Assert.NotNull(partInfoList);
        Assert.NotNull(partitionInfo?.PartLabel);
        Assert.NotNull(partitionInfo?.PartName);
        Assert.NotNull(partitionInfo?.PartUuid);
        Assert.NotNull(partitionInfo?.DiskDriveModel);
        
        Assert.Equal(ExpectedCategory,partInfoList.Category!.Name);
    }  
    
    [TestMethod]
    public async Task CheckIfGetCertificateInfoReturnsCorrectValue()
    {
        var certificateInfoList = await new OperatingSystemInfo().GetCertificateInfoAsync();
        var certificateInfo = certificateInfoList.Value?.FirstOrDefault();
        
        Assert.IsType<CertificateInfo>(certificateInfo);
        Assert.NotNull(certificateInfoList);
        Assert.NotNull(certificateInfo?.Name);
        Assert.IsType<DateTime>(certificateInfo?.EffectiveDate);
        Assert.IsType<DateTime>(certificateInfo?.ExpirationDate);

        Assert.Equal(ExpectedCategory,certificateInfoList.Category!.Name);
    }  
}