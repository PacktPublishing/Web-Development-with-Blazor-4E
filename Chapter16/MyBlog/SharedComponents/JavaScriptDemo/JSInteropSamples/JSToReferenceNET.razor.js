export function callreferencenetfromjs(dotnetHelper) {
    return dotnetHelper.invokeMethodAsync('GetHelloMessage').then(r => alert(r));
}
