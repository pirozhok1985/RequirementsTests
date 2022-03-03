// See https://aka.ms/new-console-template for more information

using System.Threading.Channels;
using Microsoft.VisualBasic;
using RequirementsTestsServices.UseCases.LinuxInfo;

var hwinfo = new HardwareInfo();
var vendorinfo = await hwinfo.GetVendorInfoAsync();
var cpuInfo = await hwinfo.GetCpuInfoAsync(new[] {"model name", "cpu cores"});
var memInfo = await hwinfo.GetRamInfoAsync(new[] {"MemTotal","MemFree","Mapped"});
var firmWare = await hwinfo.GetFirmWareInfoAsync();
var drives = DriveInfo.GetDrives();
var disks = await hwinfo.GetDiskDriveInfoAsync();

Console.ReadLine();

