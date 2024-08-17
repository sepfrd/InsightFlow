using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace InsightFlow.Api.Conventions;

public class ControllerDocumentationConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        foreach (var attribute in controller.Attributes)
        {
            if (attribute is not RouteAttribute routeAttribute)
            {
                continue;
            }

            if (!string.IsNullOrEmpty(routeAttribute.Name))
            {
                controller.ControllerName = routeAttribute.Name;
            }
        }
    }
}