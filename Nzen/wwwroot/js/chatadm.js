"use strict";

var connection = connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
connection.on("ReceiveMessage", function (user, message) {
    nicoscreen.add(message);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

window.onload = function () {
    var groupId = document.getElementById("groupId").value;
    connection.invoke("JoinGroup", groupId).catch(function (err) {
        return console.error(err.toString());
    });
};