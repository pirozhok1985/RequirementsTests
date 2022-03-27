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
        const string expectedName = "OsInfo";
        var osInfo = await new OperatingSystemInfo().GetOsInfoAsync();

        Assert.IsType<OsInfo>(osInfo);
        Assert.NotNull(osInfo);
        Assert.NotNull(osInfo.DistribCodeName);
        Assert.NotNull(osInfo.DistribDescription);
        Assert.NotNull(osInfo.DistribRelease);
        Assert.NotNull(osInfo.DistribId);
        Assert.Equal(ExpectedCategory,osInfo.Category!.Name);
        Assert.Equal(expectedName, osInfo.Name);
    }  
    
    [TestMethod]
    public async Task CheckIfGetNetworkConfigInfoReturnsCorrectValue()
    {
        const string expectedName = "NetworkConfigInfo";
        var networkConfigInfoList = await new OperatingSystemInfo().GetNetworkConfigInfoAsync();
        var networkConfigInfo = networkConfigInfoList.FirstOrDefault(ni => ni.Gateway is not null);
            
        Assert.NotEmpty(networkConfigInfoList);
        Assert.IsType<NetworkConfigInfo>(networkConfigInfo);
        Assert.NotNull(networkConfigInfoList);
        Assert.NotNull(networkConfigInfo!.InterfaceName);
        Assert.NotEmpty(networkConfigInfo.IpAddresses!);
        Assert.NotEmpty(networkConfigInfo.DnsServers!);
        
        Assert.Equal(ExpectedCategory,networkConfigInfo.Category!.Name);
        Assert.Equal(expectedName, networkConfigInfo.Name);
    }  
    
    [TestMethod]
    public async Task CheckIfGetDiskDrivePartitionInfoReturnsCorrectValue()
    {
        const string expectedName = "DiskDrivePartitionInfo";
        var partInfoList = await new OperatingSystemInfo().GetDiskDrivePartitionInfoAsync();
        var partitionInfo = partInfoList.FirstOrDefault();
        
        Assert.IsType<DiskDrivePartitionInfo>(partitionInfo);
        Assert.NotNull(partInfoList);
        Assert.NotNull(partitionInfo!.PartLabel);
        Assert.NotNull(partitionInfo.PartName);
        Assert.NotNull(partitionInfo.PartUuid);
        Assert.NotNull(partitionInfo.DiskDriveModel);
        
        Assert.Equal(ExpectedCategory,partitionInfo.Category!.Name);
        Assert.Equal(expectedName, partitionInfo.Name);
    }  
    
    [TestMethod]
    public async Task CheckIfGetCertificateInfoReturnsCorrectValue()
    {
        const string expectedName = "CertificateInfo";
        var certificateInfoList = await new OperatingSystemInfo().GetCertificateInfoAsync();
        var certificateInfo = certificateInfoList.FirstOrDefault();
        
        Assert.IsType<CertificateInfo>(certificateInfo);
        Assert.NotNull(certificateInfoList);
        Assert.NotNull(certificateInfo!.CertificateName);
        Assert.IsType<DateTime>(certificateInfo.EffectiveDate);
        Assert.IsType<DateTime>(certificateInfo.ExpirationDate);

        Assert.Equal(ExpectedCategory,certificateInfo.Category!.Name);
        Assert.Equal(expectedName, certificateInfo.Name);
    }  
}