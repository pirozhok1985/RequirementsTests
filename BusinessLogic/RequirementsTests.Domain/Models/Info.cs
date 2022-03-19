using RequirementsTests.Domain.Base;

namespace RequirementsTests.Domain.Models;

public class Info<T> : BaseInfo
{
    public string? Name { get; set; }
    public T? Value { get; set; }
    public string? Description { get; set; }
    public InfoCategory? Category { get; set; }

    public bool IsCompliant { get; set; } = false;
}