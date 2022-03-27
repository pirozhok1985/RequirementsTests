using RequirementsTests.Domain.Models.InventoryInfoTypes;
using RequirementsTests.UIHelpers.ViewModels;

namespace RequirementsTests.UIHelpers.Converters;

public static class GeneralDeviceInfoViewModelConverter
{
    public static GeneralDeviceInfoViewModel ToViewModel(this GeneralDeviceInfo generalDeviceInfo)
    {
        return new GeneralDeviceInfoViewModel()
        {
            Model = generalDeviceInfo.Model,
            Vendor = generalDeviceInfo.Vendor,
            SerialNumber = generalDeviceInfo.SerialNumber,
        };
    }
}