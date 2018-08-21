const connection = {

    initialize: function () {

        // Create WebSocket connection.
        this.socket = ((location.hostname === "" || location.hostname === "localhost" || location.hostname === "127.0.0.1") && !location.href.includes('remote')
            ? new WebSocket('ws://localhost:1000/ws')
            : new WebSocket('ws://145.239.86.84:1000/ws'));

        // Connect to server
        this.socket.addEventListener('open', function (event) {

            const connectionString = JSON.stringify({
                Type: "connect",
                Name: "Player"
            });

            connection.socket.send(connectionString);
        });

        // Listen for messages
        this.socket.addEventListener('message', function (event) {

            let response = JSON.parse(event.data);
            responseController.processResponse(response);
        });

        // Set up interval (sending player state to server)
        setInterval(this.connectionInterval, 50);
    },

    connectionInterval: function () {

        if (config.DEBUG_STOP)
            return;

        if (connection.socket.readyState !== connection.socket.OPEN)
            return true;

        const playerStateString = JSON.stringify({
            Type: "playerstate",
            Keys: keys.getKeysState(),
            Angles: mouse.getCurrentAngles(),
            PingStart: new Date().getTime()
        });

        console.log(playerStateString);
        connection.socket.send(playerStateString);
    },

};