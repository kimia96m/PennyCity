//function myFunction() {
//    document.getElementById("myDropdown").classList.toggle("show");
//}
//window.onclick = function (event) {
//    if
//        (!event.target.matches('.dropdown')) {
//        var dropdown = document.getElementsByClassName("dropdown-content");
//        var i;
//        for (i = 0; i < dropdown.length; i++) {
//            var openDropdown = dropdown[i];
//            if (openDropdown.classList.contains('show')) {
//                openDropdown.classList.remove('show');
//            }
//        }
//    }
    
//}


function togglePW() {
    var password = document.querySelector('[name=password]');

    if (password.getAttribute('type') === 'password') {
        password.setAttribute('type', 'text');
        document.getElementById("font").style.color = 'black';

    }
    else {
        password.setAttribute('type','password');
        document.getElementById("font").style.color = 'black';
 
    }
}
function toggleCPW() {
    var password = document.querySelector('[name=confirmpassword]');

    if (password.getAttribute('type') === 'password') {
        password.setAttribute('type', 'text');
        document.getElementById("font").style.color = 'black';

    }
    else {
        password.setAttribute('type', 'password');
        document.getElementById("font").style.color = 'black';

    }
}


var slideIndex = 1;
showSlides(slideIndex);

function plusSlides(n) {
    showSlides(slideIndex += n);
}

function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    var i;
    var slides = document.getElementsByClassName("home-gallery-item");
    var dots = document.getElementsByClassName("home-gallery-pager-btn");
    if (n > slides.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = slides.length }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" selected", "");
    }
    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " selected";
}



//function openCity(evt, cityName) {
//    var i, tabcontent, tablinks;
//    tabcontent = document.getElementsByClassName("home-spec-offer-item");
//    for (i = 0; i < tabcontent.length; i++) {
//        tabcontent[i].style.display = "none";
//    }
//    tablinks = document.getElementsByClassName("home-spec-offer-side-button");
//    for (i = 0; i < tablinks.length; i++) {
//        tablinks[i].className = tablinks[i].className.replace(" selected", "");
//    }
//    document.getElementById(cityName).style.display = "block";
//    evt.currentTarget.className += " selected";
//}
//document.getElementById("defaultOpen").click();