using RequirementsTests.Domain.Models.InventoryInfoTypes;
using RequirementsTests.UIHelpers.ViewModels;

namespace RequirementsTests.UIHelpers.Converters;

public static class FirmWareInfoViewModelConverter
{
    public static FirmwareInfoViewModel ToViewModel(this FirmwareInfo firmwareInfo)
    {
        return new FirmwareInfoViewModel
        {
            Date = firmwareInfo.Date,
            Release = firmwareInfo.Release,
            Vendor = firmwareInfo.Vendor,
            Version = firmwareInfo.Version,
        };
    }
}