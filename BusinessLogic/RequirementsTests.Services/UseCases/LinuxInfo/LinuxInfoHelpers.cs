using System.Runtime.InteropServices;
using RequirementsTests.Domain.Models;

namespace RequirementsTests.Services.UseCases.LinuxInfo;

public static class LinuxInfoHelpers
{
    #region extern methods(libblkid)

    [DllImport("blkid.so",EntryPoint = "blkid_do_fullprobe")]
    public static extern int DoFullProbe(nint pr); // Try to probe specific device(dev or part) in order to fetch information from it 
    
    [DllImport("blkid.so", EntryPoint = "blkid_partlist_numof_partitions")]
    public static extern int GetNumberOfPartitions(nint ls);

    [DllImport("blkid.so", EntryPoint = "blkid_probe_lookup_value")]
    public static extern int LookupValue(
        nint pr, [MarshalAs(UnmanagedType.LPStr)] string name,
        [MarshalAs(UnmanagedType.LPStr)] out string data, nint len); // Lookup value according to the 'name' parameter and [out] result to 'data'

    [DllImport("blkid.so", EntryPoint = "blkid_new_probe_from_filename")]
    public static extern nint NewProbeFromFilename(
        [MarshalAs(UnmanagedType.LPStr)]
        string filename);

    [DllImport("blkid.so", EntryPoint = "blkid_probe_get_partitions")]
    public static extern nint
        GetPartitions(nint pr); //Get pointer to partition list, pointer to dev probe as a parameter

    [DllImport("blkid.so",EntryPoint = "blkid_partlist_get_partition")]
    public static extern nint
        GetPartition(nint ls, int num); //Get pointer to partition, pointer to partition list as a parameter

    [DllImport("blkid.so", EntryPoint = "blkid_partition_get_uuid")]
    public static extern unsafe char *
        GetPartUuid(nint par); //Get partition uuid as char*, Pointer to the particular partition as a parameter;

    [DllImport("blkid.so", EntryPoint = "blkid_partition_get_name")]
    public static extern unsafe char *
        GetPartLabel(nint par); //Get partition name as char*, Pointer to the particular partition as a parameter;

    #endregion
    
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