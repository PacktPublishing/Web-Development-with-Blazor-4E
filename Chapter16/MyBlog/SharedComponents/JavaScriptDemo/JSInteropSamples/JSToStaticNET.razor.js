
window.callnetfromjs = () => {
    DotNet.invokeMethodAsync('SharedComponents', 'MadeUpName')
        .then(data => {
            data.push(4);
            alert(data);
        });
};
