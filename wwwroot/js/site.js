// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

function validateForm() {
    error = 0;
    message = "";
    var x = document.forms["loan"]["pv"].value;
    x = x.replace(',', '.');
    if (isNaN(x) || x < 0 || x == '') {
        message = "Kwota kredytu jest wymagana i musi być liczbą większą od zera";
        error = 1;
    }
    var x = document.forms["loan"]["rate"].value;
    x = x.replace(',', '.');
    if (isNaN(x) || x < 0 || x == '') {
        if (error == 1) {
            message = message + "<br /><br />";
        }
        message = message + "Orocentowanie jest wymagane i musi być liczbą większą od zera";
        error = 1;
    }
    var x = document.forms["loan"]["quantity"].value;
    x = x.replace(',', '.');
    if (isNaN(x) || x < 0 || x == '') {
        if (error == 1) {
            message = message + "<br /><br />";
        }
        message = message + "Ilość rat jest wymagana i musi być liczbą większą od zera";
        error = 1;
    }

    if (error == 1) {
        document.getElementById("errorMessage").innerHTML = message;
    } else {
        loan.submit();
    }
}
