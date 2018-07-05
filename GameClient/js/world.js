let world = {

    initialize: function () {

        this.mapObjects = Array();
        this.players = Array();
        this.myPlayer = null;

        this.fpsLabel = document.getElementById("fpsLabel");
        this.pingLabel = document.getElementById("pingLabel");
        this.playerId = -1;
        this.ping = 999;
        this.receivedGameStateOnCurrentFrame = false;
        this.lastGamestate = null;
        this.lastFrameTime = new Date();

        this.printCoordinates = false;
    },

    onMapStateReceived: function (mapstate) {

        for (obj of mapstate.MapObjects) {

            const mapObject = {

                width: obj.Width,
                height: obj.Depth,
                x: obj.Position.X,
                y: obj.Position.Z,
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

            if (playerObject.id == this.playerId)
                this.myPlayer = playerObject;

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

        // Start a new drawing state
        push();

        // Centralize map
        const center = {
            x: (this.myPlayer !== null) ? this.myPlayer.x : 0,
            y: (this.myPlayer !== null) ? this.myPlayer.y : 0
        };

        translate((windowWidth / 2), (windowHeight / 2));

        // Draw map components
        this.drawMapObjects(center);
        this.drawPlayers(center);

        // Restore original state
        pop();
    },

    drawMapObjects: function (center) {

        noFill();
        this.mapObjects.forEach((obj, i) => {

            strokeWeight(2);
            stroke(243, 156, 18);
            noFill();
            rect(obj.x - center.x, obj.y - center.y, obj.width, obj.height);

            if (world.printCoordinates) {

                strokeWeight(0);
                textSize(12);
                fill(236, 240, 241)
                text(`(${obj.x}, ${obj.y} [${obj.width}x${obj.height}])`, obj.x - center.x, obj.y - center.y);
            }
        });
    },

    drawPlayers: function (center) {


        this.players.forEach((player, i) => {

            strokeWeight(4);
            fill(255, 190, 118);
            if (this.myPlayer !== null && player.id == this.myPlayer.id)
                stroke(52, 152, 219);
            else
                stroke(0);

            ellipse(player.x - center.x, player.y - center.y, player.diameter, player.diameter);

            if (world.printCoordinates) {

                strokeWeight(0);
                textSize(12);
                fill(236, 240, 241)
                text(`(${Math.round(player.x)}, ${Math.round(player.y)} [${player.diameter}])`, player.x - center.x, player.y - center.y);
            }
        });
    },

    
};