using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace RequestLocalization.Controllers.Conventions;

public class CultureRouteConvention : IApplicationModelConvention
{
    private const string CultureTemplate = "{culture?}";

    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            foreach (var selector in controller.Selectors)
            {
                var attributeRouteModel = selector.AttributeRouteModel;

                // Prepend {culture?} to existing routes, or add a default if none exist
                if (attributeRouteModel != null)
                {
                    if (attributeRouteModel.Template is null || !attributeRouteModel.Template.Contains(CultureTemplate))
                        attributeRouteModel.Template = $"{CultureTemplate}/{attributeRouteModel.Template}";
                }
                else
                {
                    selector.AttributeRouteModel = new AttributeRouteModel
                    {
                        Template = $"{CultureTemplate}/{{controller=Home}}/{{action=Index}}/{{id?}}"
                    };
                }
            }
        }
    }
}