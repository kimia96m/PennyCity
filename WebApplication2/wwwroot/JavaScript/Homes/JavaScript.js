function openCity(evt, cityName) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName('home-spec-offer-item');
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName('home-spec-offer-side-button');
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" selected", "");
    }
    document.getElementById(cityName).style.display = "block";
    evt.currentTarget.className += " selected";
}

// Get the element with id="defaultOpen" and click on it
document.getElementById("defaultOpen").click();