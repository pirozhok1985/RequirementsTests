using RequirementsTests.Console.ViewModels;
using RequirementsTests.Domain.Models.InventoryInfoTypes;

namespace RequirementsTests.Console.Converters;

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