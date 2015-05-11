

function LoadFromLocalStorage(key, defaultval) {
    if (!defaultval)
        defaultval = "";
    var v = window.localStorage.getItem("'" + key + "'");
    return v ? v : defaultval;
}

function SaveInLocalStorage(key, val) {
    if (typeof (localStorage) != 'undefined') {
        window.localStorage.removeItem("'" + key + "'");
        window.localStorage.setItem("'" + key + "'", val);
        return true;
    }
    else {
        alert("Your browser does not support local storage, please upgrade to latest browser");
        return false;
    }
}

function RemoveFromLocalStorage(key) {
    window.localStorage.removeItem("'" + key + "'");
}



//(function ()
//{
//    var curImgId = 0;
//    var numberOfImages = 4; // Change this to the number of background images
//    window.setInterval(function ()
//    {
//        $('body').css('background-image', 'url(/image/background' + curImgId + '.jpg)');
//        curImgId = (curImgId+1) % numberOfImages;
//    }, 5 * 1000);
    
//})
//();