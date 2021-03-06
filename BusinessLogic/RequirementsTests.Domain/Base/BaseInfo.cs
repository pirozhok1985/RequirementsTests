using RequirementsTests.Domain.Models;

namespace RequirementsTests.Domain.Base;

public abstract class BaseInfo
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    
    public bool IsCompliant { get; set; } = false;

    public int CategoryId { get; set; }
    public CategoryInfo? Category { get; set; }
}