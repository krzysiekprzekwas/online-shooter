function ConnectionController() {

    const that = this;

    that.connection = new signalR.HubConnectionBuilder()
        .withUrl('/game')
        .build();

    that.FrameDropRate = 0;

    that.Initialize = function (name) {

        that.connection.onclose(function(e) {
            vex.dialog.alert({
                message: 'Connection with server lost!',
                callback: function() {
                    location.reload();
                }
            });
        });

        that.connection.on('newPlayerConnected',
            function(name) {

                notificationController.PlayerJoinedNotification(name);

            });

        that.connection.on('playerKilled',
            function(killerAndvictim) {
                notificationController.PlayerKilledNotification(killerAndvictim[0], killerAndvictim[1]);
            });

        that.connection.on('playerDisconnected',
            function(name) {

                notificationController.PlayerLeftNotification(name);

            });

        that.connection.on('updateLatency',
            function(sendTime) {
                var d = new Date();
                var n = d.getTime();
                measurementController.UpdatePing(n - sendTime);
            });

        // Create a function that the hub can call to broadcast messages.
        that.connection.on('updateGameState',
            function(gameState) {
                if (Math.random() * 100 > that.FrameDropRate) {
                    worldController.OnGameStateReceived(gameState);
                };
            });

        that.connection.on('connectConfirmation',
            function(response) {

                config = { ...config, ...response.config };

                worldController.OnMapStateReceived(response.mapState);
                weaponService.onWeaponsReceived(response.weapons);

                console.log(`Loaded server configuration (${Object.keys(response.config).length} variables)`);

                worldController.PlayerId = response.playerId;
            });

        var slider = document.getElementById("frameDropRange");
        that.FrameDropRate = slider.value;

        slider.oninput = function() {
            that.FrameDropRate = this.value;
            measurementController.UpdateFrameDropRate(that.FrameDropRate);
        };

        that.connection.start()
            .then(function() {
                console.log('connection started');
                that.OnOpen(name);
                // Set up interval (sending player state to server)
                setInterval(that.ConnectionInterval, 50);
            });
    };

    that.OnOpen = function(name) {

        that.connection.invoke('onOpen', name);
    };

    that.ConnectionInterval = function() {

        const playerStateString = JSON.stringify({
            Type: "playerstate",
            Keys: keyboardController.GetKeysState(),
            Angle: mouseController.getCurrentAngle(),
            MouseClicked: mouseController.IsMouseClicked(),
            PingStart: new Date().getTime()
        });

        var d = new Date();

        if (Math.random() * 100 > that.FrameDropRate) {
            that.connection.invoke('clientStateUpdate', playerStateString);
            that.connection.invoke('measureLatency', d.getTime());
        };
    };
}

const connectionController = new ConnectionController();