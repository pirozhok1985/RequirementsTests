using RequirementsTestsDomain.Base;

namespace RequirementsTestsDomain.Models;

public class Info<T> : BaseInfo
{
    public T? Value { get; set; }
    public string? Description { get; set; }
    public InfoCategory? Category { get; set; }

    public bool IsCompliant { get; set; } = false;
}