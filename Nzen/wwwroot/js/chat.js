"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var connectionId = "";

connection.on("ReceiveMessage", function (user, message, callerId) {

    showNewMessage(user, message, callerId);

});

connection.on("GetConnectionId", function (Id) {

    connectionId = Id;

});

connection.start().catch(function (err) {

    return console.error(err.toString());

});

document.getElementById("sendButton").addEventListener("click", function (event) {

    newMessage();

});

window.addEventListener('keydown', function (e) {
    if (e.which == 13) {
        newMessage();
        return false;
    }
});

function newMessage() {
    var user = document.getElementById("userName").value;
    var message = document.getElementById("messageInput").value;
    var group = document.getElementById("groupId").value;
    connection.invoke("JoinGroup", group).catch(function (err) {
        return console.error(err.toString());
    });
    connection.invoke("SendMessageToGroups", user, message, group).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
};

function showNewMessage(user, message, callerId) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var div = document.createElement("li");
    msg = "<img src=\"imageholder\" alt=\"\" /><p>chatmessageholder</p>".replace("chatmessageholder", msg)

    if (callerId === connectionId) {
        msg = msg.replace("imageholder", "./../images/Me.png");
        div.className = "replies"
    } else {
        msg = msg.replace("imageholder", "./../images/You.png");
        div.className = "sent"
    }

    div.innerHTML = msg;
    document.getElementById("messagesList").appendChild(div);
    $(".messages").animate({ scrollTop: $(".messages")[0].scrollHeight }, "fast");
}