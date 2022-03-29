// See https://aka.ms/new-console-template for more information

using RequirementsTests.Services.UseCases.LinuxInfo;
using RequirementsTests.UIHelpers.Converters;

var hwInfo = new HardwareInfo();
var osInfo = new OperatingSystemInfo();
var cpuInfo = await hwInfo.GetCpuInfoAsync();
var ramInfo = await hwInfo.GetRamInfoAsync();
var certInfo = await osInfo.GetCertificateInfoAsync();
var firmwareInfo = await hwInfo.GetFirmWareInfoAsync();
var generalDeviceInfo = await hwInfo.GetGeneralDeviceInfoAsync();
var diskDrivePartitionInfo = await osInfo.GetDiskDrivePartitionInfoAsync();
// var netConfigInfo = await osInfo.GetNetworkConfigInfoAsync();
// var operatingSystemInfo = await osInfo.GetOsInfoAsync();
Console.WriteLine("========================Requirements Information and Testing================================\n\n");

Console.WriteLine("===============================Category: Hardware===========================================\n");

Console.WriteLine("CpuInfo:");
Console.WriteLine("--------------------------------------------------------");
Console.WriteLine("|  Model                                 |  CoresCount |");
Console.WriteLine("--------------------------------------------------------");
Console.WriteLine("|{0,39} |{1,12} |", cpuInfo.ToViewModel().Model, cpuInfo.ToViewModel().CoresCount);
Console.WriteLine("--------------------------------------------------------\n");

Console.WriteLine("RamInfo:");
Console.WriteLine("-------------------------");
Console.WriteLine("|   Total   |   Free    |");
Console.WriteLine("-------------------------");
Console.WriteLine("|{0,10} |{1,10} |", ramInfo.ToViewModel().Total, ramInfo.ToViewModel().Free);
Console.WriteLine("-------------------------\n");

Console.WriteLine("FirmWareInfo:");
Console.WriteLine("--------------------------------------------------------------");
Console.WriteLine("| Version |          Vendor          |  Release |    Date    |");
Console.WriteLine("--------------------------------------------------------------");
Console.WriteLine("|{0,8} |{1,25} |{2,9} |{3,11} |"
    ,firmwareInfo.ToViewModel().Version
    ,firmwareInfo.ToViewModel().Vendor
    ,firmwareInfo.ToViewModel().Release
    ,firmwareInfo.ToViewModel().Date);
Console.WriteLine("--------------------------------------------------------------\n");

Console.WriteLine("GeneralDeviceInfo:");
Console.WriteLine("-------------------------------------------------------------------------------------");
Console.WriteLine("|                Vendor              |  Model  |               Serial               |", generalDeviceInfo.ToViewModel().Vendor);
Console.WriteLine("-------------------------------------------------------------------------------------");
Console.WriteLine("|{0,35} |{1,8} |{2,35} |"
    ,generalDeviceInfo.ToViewModel().Vendor
    ,generalDeviceInfo.ToViewModel().Model
    ,generalDeviceInfo.ToViewModel().SerialNumber);
Console.WriteLine("-------------------------------------------------------------------------------------\n\n");



Console.WriteLine("===============================Category: Operation System===========================================\n");
Console.WriteLine("CertificateInfo:");
Console.WriteLine("--------------------------------------------------------------------");
Console.WriteLine("|        Certificate Name       | Effective Date | Expiration Date |");
Console.WriteLine("--------------------------------------------------------------------");
foreach (var cert in certInfo)
{
    Console.WriteLine("|{0,30} |{1,15} |{2,16} |"
        ,cert.ToViewModel().CertificateName
        ,cert.ToViewModel().EffectiveDate
        ,cert.ToViewModel().ExpirationDate);
    Console.WriteLine("--------------------------------------------------------------------");
}

Console.WriteLine();

Console.WriteLine("DiskDrivePartitionInfo:");

foreach (var diskDrive in diskDrivePartitionInfo)
{
    Console.WriteLine("{0,60}",diskDrive.Key);
    Console.WriteLine("----------------------------------------------------------------------------------------");
    Console.WriteLine("|    PartName    |       PartLabel      |             Partition UUID            | Type |");
    Console.WriteLine("----------------------------------------------------------------------------------------");
    foreach (var partition in diskDrive.Value)
    {
        Console.WriteLine("|{0,15} |{1,21} |{2,38} |{3,5} |"
            , partition.ToViewModel().PartName
            , partition.ToViewModel().PartLabel
            , partition.ToViewModel().PartUuid
            , partition.ToViewModel().Type);
        Console.WriteLine("----------------------------------------------------------------------------------------");
    }

    Console.WriteLine();
}