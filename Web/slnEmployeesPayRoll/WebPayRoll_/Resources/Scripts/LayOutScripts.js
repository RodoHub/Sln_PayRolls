window.onscroll = function () { myFunction() };

var navbar = document.getElementById("navbar");
var sticky = navbar.offsetTop;

function myFunction()
{

    if (window.pageYOffset > sticky) {
        navbar.classList.add("sticky")
    }
    if (window.pageYOffset <= sticky) {
        navbar.classList.remove("sticky");
    }
}