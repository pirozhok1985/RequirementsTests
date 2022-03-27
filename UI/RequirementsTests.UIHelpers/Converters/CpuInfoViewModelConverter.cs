using RequirementsTests.Domain.Models.InventoryInfoTypes;
using RequirementsTests.UIHelpers.ViewModels;

namespace RequirementsTests.UIHelpers.Converters;

public static class CpuInfoViewModelConverter
{
    public static CpuInfoViewModel ToViewModel(this CpuInfo cpuInfo)
    {
        return new CpuInfoViewModel
        {
            Model = cpuInfo.Model,
            CoresCount = cpuInfo.CoresCount.ToString(),
        };
    }
}