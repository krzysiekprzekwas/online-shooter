let responseController = {

    processResponse: function (response) {


        //console.log(response);
        if (response.Type === "mapstate")
            world.loadMapObjects(response.MapState);
        else if (response.Type === "gamestate")
            world.updatePlayers(response.GameState);
        else if (response.Type === "connected")
            this.connectedResponse(response);
        else if (response.Type === "received")
            this.receivedResponse(response);
        else
            logger.warn("Unknown message " + event.data);
    },

    connectedResponse: function (response) {

        logger.info("Player connected");

        world.playerId = response.PlayerId;
    },

    receivedResponse: function (response) {

        let timeDiff = new Date() - pingStart;

        world.ping = Math.floor(timeDiff / 2);
    }

};