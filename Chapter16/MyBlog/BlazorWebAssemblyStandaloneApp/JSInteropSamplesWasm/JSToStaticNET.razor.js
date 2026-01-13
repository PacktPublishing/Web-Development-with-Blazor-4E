export async function setMessage() {
    const { getAssemblyExports } = await globalThis.getDotnetRuntime(0);
    var exports = await getAssemblyExports("BlazorWebAssemblyStandaloneApp.dll");
    alert(exports.BlazorWebAssemblyStandaloneApp.JSInteropSamplesWasm.JSToStaticNET.GetAMessageFromNET());
}

export async function showMessage() {
    await setMessage();
}

