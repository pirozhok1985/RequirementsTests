using RequirementsTests.Domain.Base;

namespace RequirementsTests.Domain.Models;

public class InfoCategory : BaseInfo
{
    public string Name { get; set; } = String.Empty;
    public int? ParentId { get; set; }
}