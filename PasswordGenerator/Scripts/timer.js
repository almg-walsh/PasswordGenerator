var downloadButton = document.getElementById("download");
var counter = 30;
var newElement = document.createElement("p");
newElement.innerHTML = "Your password will only be available for 30 seconds.";
var id;

downloadButton.parentNode.replaceChild(newElement, downloadButton);

id = setInterval(function () {
    counter--;
    if (counter < 0) {
        newElement.parentNode.replaceChild(downloadButton, newElement);
        clearInterval(id);
    } else {
        newElement.innerHTML = "Your password will expire in " + counter.toString() + " seconds.";
    }
}, 1000);
