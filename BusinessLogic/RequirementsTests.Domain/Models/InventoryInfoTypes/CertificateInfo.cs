namespace RequirementsTests.Domain.Models.InventoryInfoTypes;

public class CertificateInfo
{
    public string? Name { get; set; }
    public DateTime EffectiveDate { get; set; }
    public DateTime ExpirationDate { get; set; }
}