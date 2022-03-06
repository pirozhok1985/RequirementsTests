// See https://aka.ms/new-console-template for more information

using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Channels;
using Microsoft.VisualBasic;
using RequirementsTestsServices.UseCases.LinuxInfo;

// var hwinfo = new HardwareInfo();
// var vendorinfo = await hwinfo.GetVendorInfoAsync();
// var cpuInfo = await hwinfo.GetCpuInfoAsync(new[] {"model name", "cpu cores"});
// var memInfo = await hwinfo.GetRamInfoAsync(new[] {"MemTotal","MemFree","Mapped"});
// var firmWare = await hwinfo.GetFirmWareInfoAsync();
// var drives = DriveInfo.GetDrives();
// var disks = await hwinfo.GetDiskDriveInfoAsync();
// var nics = NetworkInterface.GetAllNetworkInterfaces()
//     .Where(i => !i.Name.Contains("lo"))
//     .Where(i => !i.Name.Contains("docker"));
// string[] nicProp;
// foreach (var nic in nics)
// {
//     nicProp = nic.GetIPProperties().UnicastAddresses.Select(a => a.Address.ToString()).ToArray();
// }
//
// var osInfo = new OperatingSystemInfo();
// var networkInfo = await osInfo.GetNetworkConfigInfo();
// var osData = await osInfo.GetOsInfo();

// var result = DriveInfo.GetDrives();
blkid_struct_cache cache;
    blkid_struct_dev_iterate devIterate;
blkid_struct_dev dev;
// blkid_struct_probe probe;
    nint point = IntPtr.Zero;

    var result = LinuxInfoHelpers.blkid_get_cache(ref point, String.Empty);
    var result1 = LinuxInfoHelpers.blkid_probe_all(point);
// LinuxInfoHelpers.blkid_put_cache(point);

// var pointer = LinuxInfoHelpers.blkid_new_probe_from_filename("/dev/sdb1");
// var result = LinuxInfoHelpers.blkid_do_probe(pointer);
// var result1 = LinuxInfoHelpers.blkid_probe_lookup_value(pointer, "UUID", out string data, IntPtr.Zero);
// devIterate = LinuxInfoHelpers.blkid_dev_iterate_begin(cache);
// var result1 = LinuxInfoHelpers.blkid_dev_next(devIterate, ref point);
// dev = (blkid_struct_dev) Marshal.PtrToStructure(point, typeof(blkid_struct_dev));

    var iter = LinuxInfoHelpers.blkid_dev_iterate_begin(point);
    while (LinuxInfoHelpers.blkid_dev_next(iter, out IntPtr devP) == 0)
    {
        var res = LinuxInfoHelpers.blkid_dev_devname(devP);
        var probeRef = LinuxInfoHelpers.blkid_new_probe_from_filename(res);
        var retCode = LinuxInfoHelpers.blkid_do_probe(probeRef);
        var retCode1 = LinuxInfoHelpers.blkid_probe_lookup_value(probeRef, "LABEL", out string uuid, IntPtr.Zero);
    }





Console.ReadLine();

