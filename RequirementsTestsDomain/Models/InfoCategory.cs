using RequirementsTestsDomain.Base;

namespace RequirementsTestsDomain.Models;

public class InfoCategory : BaseInfo
{
    public string Name { get; set; } = String.Empty;
    public int? ParentId { get; set; }
}