using RequirementsTests.Domain.Base;

namespace RequirementsTests.Domain.Models.InventoryInfoTypes;

public class CpuInfo : BaseInfo, IBaseInfo
{
    public string? Model { get; set; }
    public int CoresCount { get; set; }
}