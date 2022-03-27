using RequirementsTests.Domain.Models;

namespace RequirementsTests.Domain.Base;

public interface IBaseInfo
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public CategoryInfo? Category { get; set; }
}