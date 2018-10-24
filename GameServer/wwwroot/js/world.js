let world = {

    initialize: function () {

        this.mapObjects = Array();
        this.players = Array();
        this.bullets = Array();
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
        };

        world.bullets = Array();
        for (bullet of gamestate.bullets) {

            const bulletObject = {
                playerId: bullet.playerId,
                x: bullet.position.x,
                y: bullet.position.y,
                radius: bullet.radius,
                angle: bullet.angle
            };

            world.bullets.push(bulletObject);
        };
    },

    draw: function () {

        // Start a new drawing state
        push();

        // myPlayer should be in the center
        if (this.myPlayer !== null)
            drawingController.SetMyPlayer(this.myPlayer);
        
        
        // Draw map components
        drawingController.drawMapObjects(this.mapObjects);
        drawingController.DrawBullets(this.bullets);
        drawingController.drawPlayers(this.players, this.myPlayer);

        // Restore original state
        pop();
    },

    
};