using RequirementsTests.Domain.Models.InventoryInfoTypes;
using RequirementsTests.UIHelpers.ViewModels;

namespace RequirementsTests.UIHelpers.Converters;

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