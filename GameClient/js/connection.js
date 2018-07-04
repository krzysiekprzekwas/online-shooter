// Create WebSocket connection.
const socket = ((location.hostname === "" || location.hostname === "localhost" || location.hostname === "127.0.0.1") && !location.href.includes('remote')
    ? new WebSocket('ws://localhost:1000/ws')
    : new WebSocket('ws://145.239.86.84:1000/ws'));
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

setInterval(function () {


    if (config.DEBUG_STOP)
        return;

    if (socket.readyState !== socket.OPEN)
        return true;

    const playerStateString = JSON.stringify({
        Type: "playerstate",
        Keys: keys.getKeysState(),
        Angles: mouse.getCurrentAngles(),
        PingStart: new Date().getTime()
    });

    
    socket.send(playerStateString);

}, 50);