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

        logger.info("Loaded map objects " + mapstate.mapObjects.length);
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
        const center = {
            x: (this.myPlayer !== null) ? this.myPlayer.x : 0,
            y: (this.myPlayer !== null) ? this.myPlayer.y : 0
        };

        if (typeof center.x === "undefined" || typeof center.y === "undefined") {
            console.error("Unknown center of map (probably player position is undefined)");
        }

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
            fill(255, 255, 255);

            texture(texturesService.getTexture(obj.textureId));
            rect(obj.x - center.x, obj.y - center.y, obj.width, obj.height);

            if (world.printCoordinates) {

                strokeWeight(0);
                textSize(12);
                fill(236, 240, 241);
                text(`(${obj.x}, ${obj.y} [${obj.width}x${obj.height}])`, obj.x - center.x, obj.y - center.y);
            }
        });
    },

    drawPlayers: function (center) {
        
        this.players.forEach((player, i) => {

            fill(255, 190, 118);
            if (this.myPlayer !== null && player.id === this.myPlayer.id)
                stroke(52, 152, 219);
            else
                stroke(0);

            push();

            translate(player.x - center.x, player.y - center.y);
            rotate(player.angle);

            strokeWeight(0);
            rect(0, 20, 16, 30);

            strokeWeight(4);
            ellipse(0, 0, player.radius * 2, player.radius * 2);

            pop();

            if (world.printCoordinates) {

                strokeWeight(0);
                textSize(12);
                fill(236, 240, 241);
                text(`(${Math.round(player.x)}, ${Math.round(player.y)} [${player.radius}])`, player.x - center.x, player.y - center.y);
            }
        });
    },

    
};