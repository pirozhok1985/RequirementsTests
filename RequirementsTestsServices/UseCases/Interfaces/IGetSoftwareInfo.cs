using RequirementsTestsDomain.Models;

namespace RequirementsTestsServices.UseCases.Interfaces;

public interface IGetSoftwareInfo<T>
{
    public Info<T> GetSccmClientInfo();
    public Info<T> GetOmiClientInfo();
    public Info<T> GetSberEmpClientInfo();
    public Info<T> GetAntivirusInfo();
    public Info<T> GetVdiInfo();
    public Info<T> GetAtmosphereInfo();
}