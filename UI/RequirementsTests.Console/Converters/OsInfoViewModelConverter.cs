using RequirementsTests.Console.ViewModels;
using RequirementsTests.Domain.Models.InventoryInfoTypes;

namespace RequirementsTests.Console.Converters;

public static class OsInfoViewModelConverter
{
    public static OsInfoViewModel ToViewModel(this OsInfo osInfo)
    {
        return new OsInfoViewModel()
        {
            PrettyName = osInfo.DistribDescription,
            Id = osInfo.DistribId,
            Release = osInfo.DistribRelease,
            CodeName = osInfo.DistribCodeName,
        };
    }
}