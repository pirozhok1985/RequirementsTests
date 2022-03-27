using System;
using RequirementsTests.Domain.Base;

namespace RequirementsTests.Domain.Models.InventoryInfoTypes;

public class CertificateInfo : BaseInfo, IBaseInfo
{
    public string? CertificateName { get; set; }
    public DateTime EffectiveDate { get; set; }
    public DateTime ExpirationDate { get; set; }
}