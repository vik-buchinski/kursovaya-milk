var requestsLaunched = 0;

var showLoader = true;

$(document).ajaxStart(function () {
    requestsLaunched++;
    if (showLoader) {
        $(".spinner").show();
    }
});
$(document).ajaxStop(function () {
    requestsLaunched--;
    if (requestsLaunched == 0 && showLoader) {
        $(".spinner").hide();
    }
});

var modalSignIn = {
    show: function () {
        $('#signin-modal').modal('show');
    },

    close: function () {
        $('#signin-modal').modal('hide');
    }
};

var signInArea = {
    update: function () {
        server.getSignInContent(function (data) {
            $("#signin-area").html(data);
        });
    }
};

function makeValidationSummaryRed() {
    $(".validation-summary-errors > ul li").addClass("label");
    $(".validation-summary-errors > ul li").addClass("label-danger");
}

function ajaxOnFailure(error) {
    alert(JSON.parse(error.responseText));
}