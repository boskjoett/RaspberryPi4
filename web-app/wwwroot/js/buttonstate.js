"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notifications").build();

connection.on("ButtonState", function (pressed) {
    var divElement = document.getElementById("buttonState");
    divElement.innerHTML = "<p>Button state changed</p>";
});

connection.start();
