function confirmEmail() {

    var email = $("#email").val()

    var pattern = /^\w+@\w+\.\w{2,4}$/i;
    //var pattern = /([A-Za-z0-9]?[._-][A-Za-z0-9]?)+@@([A-Za-z0-9][A-Za-z0-9\-]+[A-Za-z0-9])(\.[A-Za-z]{2,6})+\b/i

    if (email.match(pattern) == null) {
        console.log("false");
        $("div.forEmail").addClass("has-error");
        $("button.registration").attr({ disabled: "disabled" });
    }
    else {
        console.log("true");
        $("div.forEmail").removeClass("has-error");
        $("button.registration").removeAttr("disabled");
    }

}