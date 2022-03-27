using RequirementsTests.Domain.Models.InventoryInfoTypes;
using RequirementsTests.UIHelpers.ViewModels;

namespace RequirementsTests.UIHelpers.Converters;

public static class RamInfoViewModelConverter
{
    public static RamInfoViewModel ToViewModel(this RamInfo ramInfo)
    {
        return new RamInfoViewModel()
        {
            Free = $"{ramInfo.Free / 1024} MB",
            Total = $"{ramInfo.Total / 1024} MB",
        };
    }
}