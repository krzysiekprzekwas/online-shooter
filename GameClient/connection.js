// Create WebSocket connection.
const socket = new WebSocket('ws://localhost:5000/ws');
let pingStart;

// Connect to server
socket.addEventListener('open', function (event) {

    const connectionString = JSON.stringify({
        Type: "connect",
        Name: "Player"
    });

    socket.send(connectionString);
});

// Listen for messages
socket.addEventListener('message', function (event) {

    let response = JSON.parse(event.data);
    responseController.processResponse(response);
});

setInterval(function() {

    if (socket.readyState !== socket.OPEN)
        return true;

    const playerStateString = JSON.stringify({
        Type: "playerstate",
        Keys: keys.getKeysState()
    });

    //pingStart = new Date();
    socket.send(playerStateString);

}, 50);

function calculatePing() {
    let timeDiff = new Date() - pingStart;
    console.log(timeDiff);
}