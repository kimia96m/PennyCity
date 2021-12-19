function myFunction() {
    document.getElementById("myDropdown").classList.toggle("show");
}
window.onclick = function (event) {
    if
        (!event.target.matches('.dropdown')) {
        var dropdown = document.getElementsByClassName("dropdown-content");
        var i;
        for (i = 0; i < dropdown.length; i++) {
            var openDropdown = dropdown[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
    
}


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
