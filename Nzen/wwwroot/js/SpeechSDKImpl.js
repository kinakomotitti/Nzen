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
        lastRecognized += e.result.text + "\r\n";
        phraseDiv.innerHTML = e.result.text;//lastRecognized;
    };

     recognizer.startContinuousRecognitionAsync();
});