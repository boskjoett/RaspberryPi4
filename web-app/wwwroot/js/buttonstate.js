"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notifications").build();

connection.on("SendButtonState", function (pressed) {

    console.log("SendButtonState called - pressed: " + pressed);

    var divElement = document.getElementById("buttonState");
    divElement.innerHTML = "<p>Button state changed</p>";
});

connection.start().then(function () {
    console.log("SignalR connection started");
}).catch(function (err) {
    return console.error(err.toString());
});
