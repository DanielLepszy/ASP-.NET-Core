// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

    var prevScrollpos = window.pageYOffset;
            window.onscroll = function () {
                var currentScrollPos = window.pageYOffset;
                if (prevScrollpos > currentScrollPos) {
        document.getElementById("navBar").style.top = "0";
    } else {
        document.getElementById("navBar").style.top = "-60px";
    }
    prevScrollpos = currentScrollPos;
}

//document.getElementsByClassName("btn").style.display = 'none';
//if (!ViewData.ModelState.IsValid && ViewData.ModelState["Error"].Errors.Count > 0) {
//    <text>
//        $(document).ready(function() {
//            alert('@ViewData.ModelState["Error"].Errors.First().ErrorMessage');
//        });
//            </text>
//}