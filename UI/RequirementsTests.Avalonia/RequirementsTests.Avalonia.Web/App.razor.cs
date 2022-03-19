using Avalonia.Web.Blazor;

namespace RequirementsTests.Avalonia.Web;

public partial class App
{
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        WebAppBuilder.Configure<RequirementsTests.Avalonia.App>()
            .SetupWithSingleViewLifetime();
    }
}