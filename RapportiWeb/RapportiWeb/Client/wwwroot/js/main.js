function downloadFile(contentType, base64Data, fileName) {
    console.log("ciao");
    const linkSource = `data:${contentType};base64,${base64Data}`;
    const downloadLink = document.createElement("a");
    downloadLink.href = linkSource;
    downloadLink.download = fileName;
    downloadLink.click();
    
}

//window.onbeforeunload = function (e) {
    
//    return undefined
//};

//window.onunload = function () {
//    console.log("ciao123");
//    window.localstorage.clear();
//}

window.onload = function () {
    localStorage.removeItem(authToken);
    return '';
};