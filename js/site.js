(function ()
{
    var curImgId = 0;
    var numberOfImages = 4; // Change this to the number of background images
    window.setInterval(function ()
    {
        $('body').css('background-image', 'url(/image/background' + curImgId + '.jpg)');
        curImgId = (curImgId+1) % numberOfImages;
    }, 5 * 1000);
    
})
();