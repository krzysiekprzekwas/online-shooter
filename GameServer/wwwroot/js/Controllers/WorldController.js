function WorldController() {

    const that = this;

    that.mapObjects = Array();
    that.players = Array();
    that.bullets = Array();
    that.myPlayer = null;

    that.PlayerId = -1;

    that.OnMapStateReceived = function (mapstate) {

        for (obj of mapstate.mapObjects) {

            let mapObject;

            if (obj.type === "box") {
                mapObject = new MapRect(obj.position.x, obj.position.y, obj.width, obj.height);
                mapObject.SetTextureId(obj.texture);
            }
            else {
                console.error(`Unknown map object type ${obj.type}`);
            }

            that.mapObjects[obj.id] = mapObject;
        }

        console.log("Loaded map objects " + mapstate.mapObjects.length);
    };

    // Update function is called when gamestate was received from server
    that.OnGameStateReceived = function (gamestate) {
        
        that.players = Array();
        for (player of gamestate.players) {

            const playerObject = new Player(player.id);
            playerObject.SetPosition(new Vector2(player.position.x, player.position.y));
            playerObject.SetSpeed(new Vector2(player.speed.x, player.speed.y));
            playerObject.SetRadius(player.radius);
            playerObject.SetAngle(player.angle);
            
            if (playerObject.GetId() === that.PlayerId) {

                drawingController.SetMyPlayer(playerObject);
            }

            that.players.push(playerObject);
        }

        that.bullets = Array();
        for (bullet of gamestate.bullets) {

            const bulletObject = new Bullet(bullet.playerId);
            bulletObject.SetPosition(new Vector2(bullet.position.x, bullet.position.y));
            bulletObject.SetSpeed(new Vector2(bullet.speed.x, bullet.speed.y));
            bulletObject.SetAngle(bullet.angle);
            bulletObject.SetRadius(bullet.radius);

            that.bullets.push(bulletObject);
        }
    };

    that.Draw = function () {

        // Start a new drawing state
        push();

        drawingController.Draw(that.mapObjects, that.players, that.bullets);

        // Restore original state
        pop();
    };
}

const worldController = new WorldController();