// Create WebSocket connection.
const socket = new WebSocket('ws://localhost:5000/ws');
let pingStart;

// Connect to server
socket.addEventListener('open', function (event) {

    const connectionString = JSON.stringify({
        Type: "connect",
        Name: "Player",
    });

    socket.send(connectionString);
});

// Listen for messages
socket.addEventListener('message', function (event) {

    let response = JSON.parse(event.data);

    if(response.Type == "mapstate")
        world.loadMapObjects(response.MapState);
    else if(response.Type == "gamestate")
        world.updatePlayers(response.GameState);
    else if(response.Type == "connect")
        logger.info("Player connected");
    // else if(response.Type == "playerstate")
    //     calculatePing();
    else
        logger.warn("Unknown message " + event.data)
});

setInterval(function() {

    const playerStateString = JSON.stringify({
        Type: "playerstate",
        Keys: keys
    });

    pingStart = new Date();
    socket.send(playerStateString);
}, 50);

function calculatePing() {
    let timeDiff = new Date() - pingStart;
    console.log(timeDiff);
}