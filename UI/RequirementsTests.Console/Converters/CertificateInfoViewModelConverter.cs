using RequirementsTests.Console.ViewModels;
using RequirementsTests.Domain.Models.InventoryInfoTypes;

namespace RequirementsTests.Console.Converters;

public static class CertificateInfoViewModelConverter
{
    public static CertificateInfoViewModel ToViewModel(this CertificateInfo certificateInfo)
    {
        return new CertificateInfoViewModel
        {
            CertificateName = certificateInfo.CertificateName,
            EffectiveDate = certificateInfo.EffectiveDate.ToShortDateString(),
            ExpirationDate = certificateInfo.ExpirationDate.ToShortDateString(),
        };
    }
    
    public static IList<CertificateInfoViewModel> ToViewModel(this IList<CertificateInfo> certificateInfos) =>
        certificateInfos.Select(c => c.ToViewModel()).ToList();
}