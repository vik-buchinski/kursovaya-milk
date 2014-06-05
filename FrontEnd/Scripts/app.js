app = {
    // Application Constructor
    initialize: function () {
        this.bindEvents();
    },
    // Bind Event Listeners
    // Bind any events that are required on startup. Common events are: 'load', 'deviceready', 'offline', and 'online'.
    bindEvents: function () {
        document.addEventListener('deviceready', this.onDeviceReady(), false);
        document.addEventListener('load', this.onLoadReady(), false);
        document.addEventListener('offline', this.onOfflineReady(), false);
        document.addEventListener('online', this.onOnlineReady(), XMLHttpRequest);
    },
    // deviceready Event Handler
    // The scope of 'this' is the event. In order to call the 'receivedEvent'
    // function, we must explicity call 'app.receivedEvent(...);'
    onDeviceReady: function () {
        window.router = new AppRouter();
        Backbone.history.start();
    },
    onLoadReady: function () {
    },
    onOfflineReady: function () {
    },
    onOnlineReady: function () {
    }
};

$(document).ready(function () {
    //First: Bind the events     
    app.initialize();
});