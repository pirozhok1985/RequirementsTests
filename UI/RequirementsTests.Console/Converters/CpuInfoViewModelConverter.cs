using RequirementsTests.Console.ViewModels;
using RequirementsTests.Domain.Models.InventoryInfoTypes;

namespace RequirementsTests.Console.Converters;

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