const connection = {

    initialize: function () {

        // Start the connection.
        var connection = new signalR.HubConnectionBuilder()
            .withUrl('/game')
            .build();


        // Create a function that the hub can call to broadcast messages.
        connection.on('broadcastMessage', function (name, message) {
            // Html encode display name and message.
            var encodedName = name;
            var encodedMsg = message;
            // Add the message to the page.
            var liElement = document.createElement('li');
            liElement.innerHTML = '<strong>' + encodedName + '</strong>:&nbsp;&nbsp;' + encodedMsg;
            document.getElementById('discussion').appendChild(liElement);
        });

        // Transport fallback functionality is now built into start.
        connection.start()
            .then(function () {
                console.log('connection started');
                document.getElementById('sendmessage').addEventListener('click', function (event) {
                    // Call the Send method on the hub.
                    connection.invoke('send', name, messageInput.value);

                    // Clear text box and reset focus for next comment.
                    messageInput.value = '';
                    messageInput.focus();
                    event.preventDefault();
                });
            })
            .catch(error => {
                console.error(error.message);
            });

        // Create WebSocket connection.
        let address = 'ws://145.239.86.84:1000/ws';
        if ((location.hostname === "" || location.hostname === "localhost" || location.hostname === "127.0.0.1") && !location.href.includes('remote'))
            address = 'ws://localhost:1000/ws';
        
        this.socket = new WebSocket(address);
        this.messagesReceivedCount = 0;
        this.messagesSentCount = 0;

        // Listeners
        this.socket.addEventListener('open', this.onOpen);
        this.socket.addEventListener('message', this.onMessage);
        this.socket.addEventListener('close', this.onClose);
        this.socket.addEventListener('error', this.onError);

        // Set up interval (sending player state to server)
        setInterval(this.connectionInterval, 50);
    },

    onOpen: function (event) {

        const connectionString = JSON.stringify({
            Type: "connect",
            Name: "Player"
        });

        connection.socket.send(connectionString);

        connection.connectionOpenDate = new Date();
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

    onClose: function (event) {

        const passed = (new Date() - connection.connectionOpenDate);

        if (event.code == 3001) {
            logger.info(`Connection closed`);
            connection.logConnectionDetails();
        }
        else if (typeof connection.connectionOpenDate == "undefined") {

            logger.error(`Can't establish connection with server`);
            logger.info(event);

            blackScreen.Set("Can't establish server connection", `Tried to connect to ${connection.socket.url} but no response. \nProbably server is offline.`);
        }
        else {

            logger.error(`Connection unexpectedly closed`);
            connection.logConnectionDetails();
            logger.info(event);

            blackScreen.Set("Connection unexpectedly closed", "Connection error occured - see details in console");
        }
    },

    onError: function (event) {

        logger.error('WebSocket error occured:');
        logger.error(event);
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

        if (connection.socket.readyState !== connection.socket.OPEN)
            return true;

        const playerStateString = JSON.stringify({
            Type: "playerstate",
            Keys: keys.getKeysState(),
            Angles: mouse.getCurrentAngles(),
            PingStart: new Date().getTime()
        });

        connection.messagesSentCount += 1;
        connection.socket.send(playerStateString);
    },

};