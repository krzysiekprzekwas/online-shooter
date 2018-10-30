function WorldController() {

    const that = this;

    that.mapObjects = Array();
    that.players = Array();
    that.bullets = Array();
    that.myPlayer = null;

    that._playerId = -1;
    that.SetPlayerId = (id) => that._playerId = id;
    that.GetPlayerId = () => that._playerId;

    that.OnMapStateReceived = function (mapstate) {

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
            that.mapObjects[obj.id] = mapObject;
        }

        console.log("Loaded map objects " + mapstate.mapObjects.length);
    };

    // Update function is called when gamestate was received from server
    that.OnGameStateReceived = function (gamestate) {

        // Load players
        that.players = Array();
        for (player of gamestate.players) {

            const playerObject = {
                id: player.id,
                x: player.position.x,
                y: player.position.y,
                radius: player.radius,
                angle: player.angle
            };

            if (playerObject.id === that.GetPlayerId())
                that.myPlayer = playerObject;

            that.players.push(playerObject);
        }

        that.bullets = Array();
        for (bullet of gamestate.bullets) {

            const bulletObject = {
                playerId: bullet.playerId,
                x: bullet.position.x,
                y: bullet.position.y,
                radius: bullet.radius,
                angle: bullet.angle
            };

            that.bullets.push(bulletObject);
        }
    };

    that.Draw = function () {

        // Start a new drawing state
        push();

        // myPlayer should be in the center
        if (that.myPlayer !== null)
            drawingController.SetMyPlayer(that.myPlayer);

        // Draw map components
        drawingController.drawMapObjects(that.mapObjects);
        drawingController.DrawBullets(that.bullets);
        drawingController.drawPlayers(that.players, that.myPlayer);

        // Restore original state
        pop();
    };
}

const worldController = new WorldController();