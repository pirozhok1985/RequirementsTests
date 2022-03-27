using RequirementsTests.Console.ViewModels;
using RequirementsTests.Domain.Models.InventoryInfoTypes;

namespace RequirementsTests.Console.Converters;

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