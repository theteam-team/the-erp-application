
$(document).ready(function () {

    $("#sidebar").mCustomScrollbar({
        theme: "minimal"
    });

    $('#sidebarCollapse').on('click', function () {
        // open or close navbar
        $('#sidebar').toggleClass('active');
        $('#content').toggleClass('active');
        // close dropdowns
        $('.collapse.in').toggleClass('in');
        // and also adjust aria-expanded attributes we use for the open/closed arrows
        // in our CSS
        $('a[aria-expanded=true]').attr('aria-expanded', 'false');
    });

    
    

    

    var x = 0;
    var s = "";
    console.log("Hello Pluralsight");
    var button = $("#buyButton");
    button.on("click", function () {
        console.log("Buying Item");
    });
    
    var productInfo = $(".product-props li");
    productInfo.on("click", function () {
        console.log("clicked one of the items: " + $(this).text());
    });
    
    var $loginToggle = $("#loginToggle");
    var $SignInForm = $("#signIn-form");
    var $signInToggle = $("#registerToggle");
    var $SignUpForm = $("#register-form");
    //setUp();
    //function setUp() {
    //    $SignUpForm.hide()
    //    $SignInForm.hide();

    //    $loginToggle.on("click", function () {
    //        $SignInForm.toggle(400);
    //        $SignUpForm.hide()
    //    });
    //    $signInToggle.on("click", function () {
    //        $SignUpForm.toggle(400);
    //        $SignInForm.hide();
    //    });
    //}
    function openCity(evt, cityName) {
        var i, tabcontent, tablinks;
        tabcontent = document.getElementsByClassName("tabcontent");
        for (i = 0; i < tabcontent.length; i++) {
            tabcontent[i].style.display = "none";
        }
        tablinks = document.getElementsByClassName("tablinks");
        for (i = 0; i < tablinks.length; i++) {
            tablinks[i].className = tablinks[i].className.replace(" active", "");
        }
        document.getElementById(cityName).style.display = "block";
        evt.currentTarget.className += " active";
    }

    // Get the element with id="defaultOpen" and click on it
    document.getElementById("defaultOpen").click();

    

});














