const connector = {

    initialize: function () {

        // Start the connection.
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl('/game')
            .build();


        // Create a function that the hub can call to broadcast messages.
        this.connection.on('message', function (name, message) {
            // Html encode display name and message.
            var encodedName = name;
            var encodedMsg = message;
            // ALog message
            console.log(encodedName + " " + encodedMsg);
        });

        // Transport fallback functionality is now built into start.
        this.connection.start()
            .then(function () {
                console.log('connection started');
                connector.onOpen();
            });

        this.messagesReceivedCount = 0;
        this.messagesSentCount = 0;

        // Set up interval (sending player state to server)
        setInterval(this.connectionInterval, 50);
    },

    onOpen: function (event) {

        const connectionString = JSON.stringify({
            Type: "connect",
            Name: "Player"
        });
        
        logger.info('Connection established');
    },

    onMessage: function (event) {

        connection.messagesReceivedCount += 1;
        
        //logger.info(`Received message #${connection.messagesReceivedCount}`);
        //logger.info(event);

        let response = JSON.parse(event.data);
        responseController.processResponse(response);

        return false;
    },

    logConnectionDetails() {

        const passed = (new Date() - connection.connectionOpenDate);
        logger.info(`Connection open time: ${passed}ms`);
        logger.info(`Received messages: ${connection.messagesReceivedCount}`);
        logger.info(`Sent messages: ${connection.messagesSentCount}`);
    },

    connectionInterval: function () {

        if (config.DEBUG_STOP)
            return;

        const playerStateString = JSON.stringify({
            Type: "playerstate",
            Keys: keys.getKeysState(),
            Angles: mouse.getCurrentAngles(),
            PingStart: new Date().getTime()
        });

        connector.messagesSentCount += 1;
        connector.connection.invoke('send', "tester", connector.messagesSentCount);
    }
};