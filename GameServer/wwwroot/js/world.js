let world = {

    initialize: function () {

        this.mapObjects = Array();
        this.players = Array();
        this.myPlayer = null;
        
        this.playerId = -1;
        this.receivedGameStateOnCurrentFrame = false;
        this.lastGamestate = null;
        this.images = [];

        this.printCoordinates = false;
    },

    onMapStateReceived: function (mapstate) {

        for (obj of mapstate.mapObjects) {

            const mapObject = {

                width: obj.width,
                height: obj.height,
                x: obj.position.x,
                y: obj.position.y,
                textureId: obj.texture,
                type: obj.type
            };

            // Add mesh to objects array
            world.mapObjects[obj.id] = mapObject;
        }

        console.log("Loaded map objects " + mapstate.mapObjects.length);
    },

    // Update function is called when gamestate was received from server
    onGameStateReceived: function (gamestate) {

        // Change variable so players wont be extrapolated on current frame
        world.receivedGameStateOnCurrentFrame = true;
        world.lastGamestate = gamestate;

        // Load players
        world.players = Array();
        for (player of gamestate.players) {

            const playerObject = {
                id: player.id,
                x: player.position.x,
                y: player.position.y,
                radius: player.radius,
                angle: player.angle
            };

            if (playerObject.id === world.playerId)
                world.myPlayer = playerObject;

            world.players.push(playerObject);
        }
    },

    draw: function () {

        // Start a new drawing state
        push();

        // myPlayer should be in the center
        drawingController.SetMyPlayer(this.myPlayer !== null ? this.myPlayer : { x: 0, y: 0 });
        
        // Draw map components
        drawingController.drawMapObjects(this.mapObjects);
        drawingController.drawPlayers(this.players, this.myPlayer);

        // Restore original state
        pop();
    },

    
};