// See https://aka.ms/new-console-template for more information

using System.Threading.Channels;
using RequirementsTestsServices.UseCases.LinuxInfo;

var hwinfo = new HardwareInfo();
var vendorinfo = hwinfo.GetVendorInfo();
Console.WriteLine("Here is vendor info {0}", vendorinfo.Value);