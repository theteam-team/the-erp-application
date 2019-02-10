$(document).ready(function () {
  var x = 0;
  var s = "";

  console.log("Hello Pluralsight");



  //var theForm = $("#theForm");
  //theForm.hide();

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
    setUp();
    function setUp() {
        $SignUpForm.hide()
        $SignInForm.hide();

        $loginToggle.on("click", function () {
            $SignInForm.toggle(1000);
            $SignUpForm.hide()
        });
        $signInToggle.on("click", function () {
            $SignUpForm.toggle(1000);
            $SignInForm.hide();
        });
    }



});













