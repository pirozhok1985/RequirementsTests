using RequirementsTests.Console.ViewModels;
using RequirementsTests.Domain.Models.InventoryInfoTypes;

namespace RequirementsTests.Console.Converters;

public static class RamInfoViewModelConverter
{
    public static RamInfoViewModel ToViewModel(this RamInfo ramInfo)
    {
        return new RamInfoViewModel()
        {
            Free = ramInfo.Free.ToString(),
            Total = ramInfo.Total.ToString(),
        };
    }
}