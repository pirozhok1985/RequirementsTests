using RequirementsTests.Domain.Base;

namespace RequirementsTests.Domain.Models.InventoryInfoTypes;

public class OsInfo : BaseInfo, IBaseInfo
{
    public string? DistribId { get; set; }
    public string? DistribRelease { get; set; }
    public string? DistribCodeName { get; set; }
    public string? DistribDescription { get; set; }
}