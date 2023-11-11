using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Eudaimonia.Presentation;

public class NamespaceBasedControllerModelConvention : IControllerModelConvention
{
    private readonly string _namespaceToInclude;

    public NamespaceBasedControllerModelConvention(string namespaceToInclude)
    {
        _namespaceToInclude = namespaceToInclude;
    }

    public void Apply(ControllerModel controller)
    {
        if (controller.ControllerType.Namespace?.Contains(_namespaceToInclude) != true)
        {
            controller.ApiExplorer.IsVisible = false;
            controller.Actions.Clear();
        }
    }
}