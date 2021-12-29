"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notifications").build();

connection.on("ButtonStateChanged", function (pressed) {

    console.log("ButtonState message received");

    var divElement = document.getElementById("buttonState");
    divElement.innerHTML = "<p>Button state changed</p>";
});

connection.start();
