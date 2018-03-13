let responseController = {

    processResponse: function (response) {

        if (response.Type === "mapstate")
            world.loadMapObjects(response.MapState);
        else if (response.Type === "gamestate")
            world.updatePlayers(response.GameState);
        else if (response.Type === "connected")
            this.connectedResponse(response);
        // else if(response.Type == "playerstate")
        //     calculatePing();
        else
            logger.warn("Unknown message " + event.data);
    },

    connectedResponse: function (response) {
        
        logger.info("Player connected");

        world.playerId = response.PlayerId;
    }

}