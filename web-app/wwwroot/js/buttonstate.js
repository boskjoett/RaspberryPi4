"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notifications").build();

connection.on("ButtonStateChanged", function (pressed) {

    console.log("ButtonStateChanged message received");

    var divElement = document.getElementById("buttonState");
    divElement.innerHTML = "<p>Button state changed</p>";
});

connection.start().then(function () {
    console.log("SignalR connection started");
}).catch(function (err) {
    return console.error(err.toString());
});
