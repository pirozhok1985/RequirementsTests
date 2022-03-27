using RequirementsTests.Domain.Base;

namespace RequirementsTests.Domain.Models.InventoryInfoTypes;

public class FirmwareInfo : BaseInfo, IBaseInfo
{
    public string? Version { get; set; }
    public string? Vendor { get; set; }
    public string? Release { get; set; }
    public string? Date { get; set; }
}