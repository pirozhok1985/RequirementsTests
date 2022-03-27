using System;
using RequirementsTests.Domain.Base;

namespace RequirementsTests.Domain.Models;

public class CategoryInfo : BaseInfo
{
    public string Name { get; set; } = String.Empty;
    public int? ParentId { get; set; }
}