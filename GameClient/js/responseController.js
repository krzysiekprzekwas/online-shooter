let responseController = {

    processResponse: function (response) {

        if (config.DEBUG_STOP)
            return;

        //console.log(response);
        if (response.Type === "mapstate")
            world.onMapStateReceived(response.MapState);
        else if (response.Type === "gamestate")
            world.onGameStateReceived(response.GameState);
        else if (response.Type === "connected")
            this.connectedResponse(response);
        else if (response.Type === "received")
            this.receivedResponse(response);
        else
            logger.warn("Unknown message " + event.data);
    },

    connectedResponse: function (response) {

        logger.info(`Player connected #${response.PlayerId}`);
        
        for (setting in response.Config) {

            const value = response.Config[setting];
            config[setting] = value;
        }
        logger.info(`Loaded server configuration (${Object.keys(response.Config).length} variables)`);

        world.playerId = response.PlayerId;
    },

    receivedResponse: function (response) {
        
        let timeDiff = new Date() - response.PingStart;

        world.ping = Math.floor(timeDiff);
    }

};