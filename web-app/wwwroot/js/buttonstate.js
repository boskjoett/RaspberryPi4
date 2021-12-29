"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notifications").build();

connection.on("SendButtonState", function (pressed) {

    console.log("SendButtonState called - pressed: " + pressed);

    var divElement = document.getElementById("buttonState");
    if (pressed) {
        divElement.innerHTML = "<p><b>Button is pressed</b></p>";
    } else {
        divElement.innerHTML = "<p>Button is not pressed</p>";
    }
});

connection.start().then(function () {
    console.log("SignalR connection started");
}).catch(function (err) {
    return console.error(err.toString());
});
