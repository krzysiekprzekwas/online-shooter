let world = {

    initialize: function () {

        this.mapObjects = Array();
        this.players = Array();

        this.fpsLabel = document.getElementById("fpsLabel");
        this.pingLabel = document.getElementById("pingLabel");
        this.playerId = -1;
        this.ping = 999;
        this.receivedGameStateOnCurrentFrame = false;
        this.lastGamestate = null;
        this.lastFrameTime = new Date();
    },

    onMapStateReceived: function (mapstate) {

        for (obj of mapstate.MapObjects) {

            const mapObject = {

                width: obj.Width,
                height: obj.Height,
                x: obj.Position.X,
                y: obj.Position.Y,
                texture: this.getTextureUrl(obj.TextureId),
                type: obj.Type
            };

            // Add mesh to objects array
            this.mapObjects[obj.Id] = mapObject;
        }

        logger.info("Loaded map objects " + mapstate.MapObjects.length);
    },

    // Update function is called when gamestate was received from server
    onGameStateReceived: function (gamestate) {

        // Change variable so players wont be extrapolated on current frame
        this.receivedGameStateOnCurrentFrame = true;
        this.lastGamestate = gamestate;

        // Load players
        this.players = Array();
        for (player of gamestate.Players) {

            const playerObject = {
                id: player.Id,
                x: player.Position.X,
                y: player.Position.Y,
                diameter: player.WorldObject.Diameter
            };

            this.players.push(playerObject);
        }

    },

    getTextureUrl: function (textureId) {

        // Load texture url based on textureId sent by server
        switch (textureId) {
            case 1:
                return "textures/brick.jpg";
            case 2:
                return "textures/wall.jpg";
            case 3:
                return "textures/ground.jpg";
        }
    },

    draw: function () {

        this.drawMapObjects();
    },

    drawMapObjects: function () {

        this.mapObjects.forEach((obj, i) => {

            rect(obj.x, obj.y, obj.width, obj.height);
        });
    },
    
};