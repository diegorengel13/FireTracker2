

window.onload = choosePic;

var myPix = ["url('../../Image/customizer-image-background.jpg')", "url('/Image/dark-desktop-background-img.jpg')",
    "url('/Image/photo-1480497490787-505ec076689f.jpeg')", "url('/Image/19443_en_1.jpeg')", "url('/Image/42467.jpg')", "url('/Image/slr9lgqjejz21.jpg')", "url('/Image/294406.jpg')", "url('/Image/294400.jpg')"];


function choosePic() {
   
    randomNum = Math.floor(Math.random() * myPix.length);
    document.getElementById("randomimg").style.backgroundImage = myPix[randomNum];
}

