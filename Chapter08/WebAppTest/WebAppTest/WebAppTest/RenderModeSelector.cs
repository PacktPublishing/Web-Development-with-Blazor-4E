using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace WebAppTest;

public class RenderModeSelector
{
    public IComponentRenderMode? SelectedRenderMode { get; set; } = RenderMode.InteractiveAuto;
}
