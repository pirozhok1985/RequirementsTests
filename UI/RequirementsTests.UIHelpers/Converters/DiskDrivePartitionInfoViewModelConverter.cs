using RequirementsTests.Domain.Models.InventoryInfoTypes;
using RequirementsTests.UIHelpers.ViewModels;

namespace RequirementsTests.UIHelpers.Converters;

public static class DiskDrivePartitionInfoViewModelConverter
{
    public static DiskDrivePartitionInfoViewModel ToViewModel(this DiskDrivePartitionInfo diskDrivePartitionInfo)
    {
        return new DiskDrivePartitionInfoViewModel
        {
            Label = diskDrivePartitionInfo.Label,
            Type = diskDrivePartitionInfo.Type,
            Uuid = diskDrivePartitionInfo.Uuid,
            PartLabel = diskDrivePartitionInfo.PartLabel,
            PartName = diskDrivePartitionInfo.PartName,
            PartUuid = diskDrivePartitionInfo.PartUuid,
            DiskDriveModel = diskDrivePartitionInfo.DiskDriveModel,
        };
    }
    
    public static IList<DiskDrivePartitionInfoViewModel> ToViewModel(this IList<DiskDrivePartitionInfo> diskDrivePartitionInfos) =>
        diskDrivePartitionInfos.Select(d => d.ToViewModel()).ToList();
}