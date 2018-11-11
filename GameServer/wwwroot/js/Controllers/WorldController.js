if (typeof require !== "undefined") {
    PhysicsEngine = require('../Physics/PhysicsEngine.js');
}

function WorldController() {

    const that = this;

    that.mapObjects = Array();
    that.players = Array();
    that.bullets = Array();
    that.myPlayer = null;

    that.PlayerId = -1;
    that.physicsEngine = new PhysicsEngine(that);

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

        that.physicsEngine.LastTickDate = new Date();

        that.players = Array();
        for (player of gamestate.players) {

            const playerObject = new Player(player.id);
            playerObject.SetPosition(new Vector2(player.position.x, player.position.y));
            playerObject.SetSpeed(new Vector2(player.speed.x, player.speed.y));
            playerObject.SetRadius(player.radius);
            playerObject.SetAngle(player.angle);
            playerObject.SetHealth(player.health);
            playerObject.SetMaxHealth(player.maxHealth);
            playerObject.SetAlive(player.isAlive);
            
            if (playerObject.GetId() === that.PlayerId) {
                if (!playerObject.IsAlive()) {
                    $('#killScreen').addClass('overlay');
                    $('#killScreen').removeClass('hidden');

                    $('#resurectionTime').html(Math.round(player.milisecondsToResurect / 1000) + " seconds");
                } else {
                    $('#killScreen').removeClass('overlay');
                    $('#killScreen').addClass('hidden');
                    drawingController.SetMyPlayer(playerObject);
                    $('#healthLabel').html(`${player.health}/${player.maxHealth}`);
                }
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

        if (config.extrapolation)
            that.physicsEngine.ExtrapolatePhysics();

        drawingController.Draw(that.mapObjects, that.players, that.bullets);

        // Restore original state
        pop();
    };
}

const worldController = new WorldController();

// Export module
if (typeof module !== 'undefined' && module.hasOwnProperty('exports')) {
    module.exports = worldController;
}