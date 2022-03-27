using RequirementsTests.Domain.Base;

namespace RequirementsTests.Domain.Models.InventoryInfoTypes;

public class RamInfo : BaseInfo, IBaseInfo
{
    public long Total { get; set; }
    public long Free { get; set; }
}