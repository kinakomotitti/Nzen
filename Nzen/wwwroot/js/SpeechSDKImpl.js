"use strict";

// status fields and start button in UI
var phraseDiv;
var startRecognizeOnceAsyncButton;

// subscription key and region key for speech services.
var subscriptionKey, regionKey;
var authorizationToken;
var SpeechSDK;
var recognizer;
var lastRecognized = "";

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


document.addEventListener("DOMContentLoaded", function () {

    subscriptionKey = document.getElementById("subscriptionKey").value;
    regionKey = document.getElementById("regionKey").value;

    phraseDiv = document.getElementById("phraseDiv");
    phraseDiv.innerHTML = "";

    var audioConfig = SpeechSDK.AudioConfig.fromDefaultMicrophoneInput();
    var speechConfig;

    if (authorizationToken) {

        speechConfig = SpeechSDK.SpeechConfig.fromAuthorizationToken(authorizationToken, regionKey);

    } else {

        speechConfig = SpeechSDK.SpeechConfig.fromSubscription(subscriptionKey, regionKey);

    }
    speechConfig.speechRecognitionLanguage = "ja-jp";
    recognizer = new SpeechSDK.SpeechRecognizer(speechConfig, audioConfig);

    recognizer.recognizing = function (s, e) {
        window.console.log(e);
        phraseDiv.innerHTML = e.result.text;
    };

    recognizer.recognized = function (s, e) {
        window.console.log(e);

        var groupId = document.getElementById("groupId").value;
        var userId = document.getElementById("userName").value;
        var content = e.result.text;
        if (content === 'undefined') {
            phraseDiv.innerHTML = e.result.text;//lastRecognized;
        }
        else {
            connection.invoke("SaveContents", userId, groupId, content).catch(function (err) {
                return console.error(err.toString());
            });
        }
    };

    recognizer.startContinuousRecognitionAsync();
});