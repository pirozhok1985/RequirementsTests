using RequirementsTests.Domain.Base;

namespace RequirementsTests.Domain.Models.InventoryInfoTypes;

public class GeneralDeviceInfo : BaseInfo, IBaseInfo
{
    public string? Vendor { get; set; }
    public string? Model { get; set; }
    public string? SerialNumber { get; set; }
}