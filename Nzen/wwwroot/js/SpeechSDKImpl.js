// status fields and start button in UI
var phraseDiv;
var startRecognizeOnceAsyncButton;

// subscription key and region key for speech services.
var subscriptionKey, regionKey;
var authorizationToken;
var SpeechSDK;
var recognizer;
var lastRecognized = "";

document.addEventListener("DOMContentLoaded", function () {

    subscriptionKey = "859b3e1bc5514b60819d09a4db722436";//document.getElementById("subscriptionKey");
    regionKey = "eastus";//document.getElementById("regionKey");

    phraseDiv = document.getElementById("phraseDiv");
    phraseDiv.innerHTML = "";

    var audioConfig = SpeechSDK.AudioConfig.fromDefaultMicrophoneInput();
    var speechConfig;

    if (authorizationToken) {

        speechConfig = SpeechSDK.SpeechConfig.fromAuthorizationToken(authorizationToken, regionKey.value);

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
        lastRecognized += e.result.text + "\r\n";
        phraseDiv.innerHTML = e.result.text;//lastRecognized;
    };

    recognizer.startContinuousRecognitionAsync();
});