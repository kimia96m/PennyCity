var t = 1;
function CRateOut(rating) {
    if (t == 1) {
        for (var i = 1; i <= rating; i++) {
            var spns = "span" + i;
            var spans = document.getElementById(spns);
            spans.setAttribute("class", "fa fa-star");
        }
    }
   
}
function CRateOver(rating) {
    for (var i = 1; i <= rating; i++) {
        var spns = "span" + i;
        var spans = document.getElementById(spns);

         spans.setAttribute("class", "fa fa-star checked");
        //$("#span" + i).attr('class', 'fa fa-star checked');
    }
}
function CRateClick(rating) {
    var c = document.getElementById('lblRating');
    c.value = rating;
    var rate = rating + 1;
    for (var i = 1; i <= rating; i++) {
        var spns = "span" + i;
        var spans = document.getElementById(spns);
        spans.setAttribute("class", "fa fa-star checked");
    }
    for (var i = rate ; i <= 5; i++) {
        var spnos = "span" + i;
        var spans = document.getElementById(spnos);
        spans.setAttribute("class", "fa fa-star");
    }
    t++;
}
function CRateSelect() {
    var rating = getElementById('lblRating').val();
    for (var i = 1; i <= rating; i++) {
        var spns = "span" + i;
        var spans = document.getElementById(spns);
        spans.setAttribute("class", "fa fa-star checked");
    }
}
function VerifyRating() {
    var rating = getElementById('lblRating').val();

    if (rating == "0") {
        return false;
    }
}