using System.Dynamic;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using RequirementsTestsDomain.Models;

namespace RequirementsTestsServices.UseCases.LinuxInfo;

public enum ChainIds
{
    BLKID_CHAIN_SUBLKS, /* FS/RAID superblocks (enabled by default) */
    BLKID_CHAIN_TOPLGY, /* Block device topology */
    BLKID_CHAIN_PARTS, /* Partition tables */
    BLKID_NCHAINS /* number of chains */
}

[StructLayout(LayoutKind.Sequential)]
public struct list_head
{
    public nint next;
    public nint prev;
};

[StructLayout(LayoutKind.Sequential)]
public struct blkid_struct_dev
{
    list_head bid_devs; /* All devices in the cache */
    list_head bid_tags; /* All tags for this device */
    nint bid_cache; /* Dev belongs to this cache */

    [MarshalAs(UnmanagedType.LPStr, SizeConst = 120)]
    public string bid_name; /* Device real path (as used in cache) */

    public string bid_xname; /* Device path as used by application (maybe symlink..) */

    [MarshalAs(UnmanagedType.LPStr, SizeConst = 120)]
    public string bid_type; /* Preferred device TYPE */

    int bid_pri; /* Device priority */
    int bid_devno; /* Device major/minor number */
    long bid_time; /* Last update time of device */
    ulong bid_utime; /* Last update time (microseconds) */
    uint bid_flags; /* Device status bitflags */

    [MarshalAs(UnmanagedType.LPStr, SizeConst = 120)]
    public string bid_label; /* Shortcut to device LABEL */

    [MarshalAs(UnmanagedType.LPStr, SizeConst = 120)]
    public string bid_uuid; /* Shortcut to binary UUID */
}

[StructLayout(LayoutKind.Sequential)]
public struct blkid_struct_dev_iterate
{
    int magic;
    nint cache;

    [MarshalAs(UnmanagedType.LPStr, SizeConst = 120)]
    string search_type;

    [MarshalAs(UnmanagedType.LPStr, SizeConst = 120)]
    string search_value;

    nint p;
}

[StructLayout(LayoutKind.Sequential)]
public struct blkid_struct_cache
{
    public list_head bic_devs; /* List head of all devices */
    list_head bic_tags; /* List head of all tag types */
    long bic_time; /* Last probe time */
    long bic_ftime; /* Mod time of the cachefile */
    uint bic_flags; /* Status flags of the cache */
    string bic_filename; /* filename of cache */
    public nint probe; /* low-level probing stuff */
};

[StructLayout(LayoutKind.Sequential)]
public struct blkid_chain
{
    nint driver; /* chain driver */
    int enabled; /* boolean */
    int flags; /* BLKID_<chain>_* */
    int binary; /* boolean */
    int idx; /* index of the current prober (or -1) */
    nint fltr; /* filter or NULL */
    nint data; /* private chain data or NULL */
}

// [StructLayout(LayoutKind.Sequential)]
// public struct blkid_chaindrv
// {
//     long id; /* BLKID_CHAIN_* */
//     [MarshalAs(UnmanagedType.LPStr, SizeConst = 120)] string name; /* name of chain (for debug purpose) */
//     int dflt_flags; /* default chain flags */
//     int dflt_enabled; /* default enabled boolean */
//     int has_fltr; /* boolean */
//
//     nint idinfos; /* description of probing functions */
//     long nidinfos; /* number of idinfos */
// }

[StructLayout(LayoutKind.Sequential)]
public struct blkid_struct_probe
{
    int fd; /* device file descriptor */
    ulong off; /* begin of data on the device */
    ulong size; /* end of data on the device */

    ulong devno; /* device number (st.st_rdev) */
    ulong disk_devno; /* devno of the whole-disk or 0 */
    uint blkssz; /* sector size (BLKSSZGET ioctl) */
    uint mode; /* struct stat.sb_mode */
    ulong zone_size; /* zone size (BLKGETZONESZ ioctl) */

    int flags; /* private library flags */
    int prob_flags; /* always zeroized by blkid_do_*() */

    ulong wipe_off; /* begin of the wiped area */
    ulong wipe_size; /* size of the wiped area */
    nint wipe_chain; /* superblock, partition, ... */

    list_head buffers; /* list of buffers */
    list_head hints;

    nint chains; /* array of chains */
    nint cur_chain; /* current chain */

    list_head values; /* results */

    nint parent; /* for clones */
    nint disk_probe; /* whole-disk probing */
}

public static class LinuxInfoHelpers
{
    [DllImport("blkid.so")]
    public static extern int blkid_get_cache(ref nint cache,
        [MarshalAs(UnmanagedType.LPStr, SizeConst = 120)] string filename);

    [DllImport("blkid.so")]
    public static extern int blkid_probe_all(nint cache);

    [DllImport("blkid.so")]
    public static extern nint blkid_dev_iterate_begin(nint cache);

    [DllImport("blkid.so")]
    public static extern int blkid_dev_next(nint iter, out nint ret_dev);

    [DllImport("blkid.so")]
    public static extern string blkid_dev_devname(nint dev);

    [DllImport("blkid.so")]
    public static extern int blkid_do_probe(nint pr);

    [DllImport("blkid.so")]
    public static extern int blkid_probe_lookup_value(
        nint pr,[MarshalAs(UnmanagedType.LPStr, SizeConst = 120)] string name,[MarshalAs(UnmanagedType.LPStr, SizeConst = 120)] out string data, nint len);
    
    [DllImport("blkid.so")]
    public static extern nint blkid_new_probe_from_filename(
        [MarshalAs(UnmanagedType.LPStr, SizeConst = 120)] string filename);

    public static Info<Dictionary<TKey, TValue>> GenerateInfo<TKey, TValue>(Dictionary<TKey, TValue> dictionary,
        string categoryName, string name, string description = "For future needs")
    {
        var info = new Info<Dictionary<TKey, TValue>>
        {
            Name = name,
            Category = new InfoCategory {Name = categoryName},
            Value = dictionary,
            Description = description,
        };
        return info;
    }

    public static Info<T> GenerateInfo<T>(T value, string categoryName, string name,
        string description = "For future needs")
    {
        var info = new Info<T>
        {
            Name = name,
            Category = new InfoCategory {Name = categoryName},
            Value = value,
            Description = description,
        };
        return info;
    }

    public static async Task<string> ReadToTheEndAsync(string path)
    {
        using var sReader = new StreamReader(path);
        return await sReader.ReadToEndAsync();
    }
}