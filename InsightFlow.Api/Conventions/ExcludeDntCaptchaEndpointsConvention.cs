using InsightFlow.Common.Constants;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace InsightFlow.Api.Conventions;

public class ExcludeDntCaptchaEndpointsConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        if (!controller.ControllerName.Contains(ApplicationConstants.DntCaptchaName, StringComparison.InvariantCultureIgnoreCase))
        {
            return;
        }

        controller.ApiExplorer.IsVisible = false;
        controller.Actions.Clear();
    }
}