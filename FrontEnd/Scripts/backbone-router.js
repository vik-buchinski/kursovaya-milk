var AppRouter = Backbone.Router.extend({

    routes: {
        "": "home",
        "login": "login"
    },
    login: function () {
        server.getLoginView(function(data) {
            $("#content").html(data);
        });
    },
    home: function () {
        $("#content").html("");
    }
});