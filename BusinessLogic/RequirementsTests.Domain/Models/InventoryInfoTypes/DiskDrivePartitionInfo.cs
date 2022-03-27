using RequirementsTests.Domain.Base;

namespace RequirementsTests.Domain.Models.InventoryInfoTypes;

public class DiskDrivePartitionInfo : BaseInfo, IBaseInfo
{
    public string? PartName { get; set; }
    public string? DiskDriveModel { get; set; }
    public string? PartLabel { get; set; }
    public string? PartUuid { get; set; }
    public string? Type { get; set; }
    public string? Label { get; set; }
    public string? Uuid { get; set; }
}