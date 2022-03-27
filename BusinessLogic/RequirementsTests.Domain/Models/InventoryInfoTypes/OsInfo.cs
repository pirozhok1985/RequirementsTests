using RequirementsTests.Domain.Base;

namespace RequirementsTests.Domain.Models.InventoryInfoTypes;

public class OsInfo : BaseInfo, IBaseInfo
{
    public string? Id { get; set; }
    public string? Release { get; set; }
    public string? CodeName { get; set; }
    public string? Description { get; set; }
}