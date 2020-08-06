document.addEventListener('DOMContentLoaded', function () {
    StartTimer();
    DetectError();
}, false);


function StartTimer() {
    setTimeout(DisplayAntiVirusWarning, 60000);
}


function DisplayAntiVirusWarning() {
    var antiVirusDiv = document.getElementById("LoadingIsBlocked");
    antiVirusDiv.style.visibility = "visible";

    var antiVirusDiv = document.getElementById("loading");
    antiVirusDiv.style.visibility = "hidden";
}

function HideAntiVirusWarning() {
    var antiVirusDiv = document.getElementById("LoadingIsBlocked");
    antiVirusDiv.style.visibility = "hidden";
}

function DetectError() {
    var req = new XMLHttpRequest();
    req.open('HEAD', document.URL, true);
    req.send();
    if (req.status === 403) {
        DisplayAntiVirusWarning();
    }
}