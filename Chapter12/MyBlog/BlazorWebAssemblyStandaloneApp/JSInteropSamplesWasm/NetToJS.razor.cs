using System.Runtime.InteropServices.JavaScript;
namespace BlazorWebAssemblyStandaloneApp.JSInteropSamplesWasm;
public partial class NetToJS
{
    [JSImport("showAlert", "nettojs")]
    internal static partial string ShowAlert(string message);
}
