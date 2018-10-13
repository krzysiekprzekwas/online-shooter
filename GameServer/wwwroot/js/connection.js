const connector = {

    notificationStack: { "dir1": "down", "dir2": "right", "push": "top" },

    initialize: function (name) {

        // Start the connection.
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl('/game')
            .build();

        this.connection.onclose = function () {
            console.log('connecition closed');
        };

        this.connection.on('newPlayerConnected', function (name) {
            new PNotify({
                title: 'Player joined',
                text: 'Player ' + name + ' connected',
                addclass: "stack-bottomleft",
                stack: connector.notificationStack,
                nonblock: {
                    nonblock: true
                }
            });
        });

        this.connection.on('playerDisconnected', function (name) {
            new PNotify({
                title: 'Player left',
                text: 'Player ' + name + ' disconnected',
                addclass: "stack-bottomleft",
                stack: connector.notificationStack,
                nonblock: {
                    nonblock: true
                }
            });
        });

        // Create a function that the hub can call to broadcast messages.
        this.connection.on('updateGameState', function (gameState) {
            world.onGameStateReceived(gameState);
        });

        this.connection.on('connectConfirmation', function (response) {

            for (setting in response.config) {

                const value = response.config[setting.toUpperCase()];
                config[setting] = value;
            }


            world.onMapStateReceived(response.mapState);

            console.log(`Loaded server configuration (${Object.keys(response.config).length} variables)`);

            world.playerId = response.playerId;
        });

        this.connection.start()
            .then(function () {
                console.log('connection started');
                connector.onOpen(name);
                // Set up interval (sending player state to server)
                setInterval(connector.connectionInterval, 50);
            });
    },

    onOpen: function (name) {

        connector.connection.invoke('onOpen', name);
    },

    onMessage: function (event) {

        let response = JSON.parse(event.data);
        responseController.processResponse(response);

        return false;
    },

    connectionInterval: function () {

        const playerStateString = JSON.stringify({
            Type: "playerstate",
            Keys: keys.getKeysState(),
            Angle: mouse.getCurrentAngles().X,
            PingStart: new Date().getTime()
        });

        connector.connection.invoke('clientStateUpdate', playerStateString);
    }
};