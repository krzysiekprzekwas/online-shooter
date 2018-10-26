const Vector2 = require('./Vector2.js');

function PhysicsEngine() {

    const that = this;

    that.ApplyPhysics = function(players) {

        players.forEach(player => {

            player.Speed = Vector2.Multiply(player.Speed, config.player_decceleration);

            const speedVector = Vector2.Add(player.Speed, PhysicsEngine.GetSpeedFromPlayerInput(player, timePassed));

            that.UpdatePlayerPosition(player, speedVector);
        });

        foreach(Player player in GameState.Instance.Players)
        {
            player.Speed *= 1 - Config.PLAYER_DECCELERATION;

            var speedVector = player.Speed + GetSpeedFromPlayerInput(player);

            UpdatePlayerPosition(player, speedVector);
        }

        GameState.Instance.Bullets.RemoveAll(b => b.Speed.Length() < Config.MIN_BULLET_SPEED);

        foreach(Bullet bullet in GameState.Instance.Bullets)
        {
            bullet.Speed *= 0.99;

            bullet.Position += bullet.Speed;
        }

        GameState.Instance.Bullets.RemoveAll(b => CheckAnyIntersectionWithWorld(new MapCircle(b.Position, b.Radius)) != null);
    }
}

PhysicsEngine.CalculatePossibleMovementVector = function (player, speedVector) {

    if (speedVector.IsDegenerated())
        return Vector2.ZERO_VECTOR();

    // Variables used to calculate speed vector
    let offset = 0;
    const speedVectorLength = speedvector.Length();
    const speedVectorNormalized = speedvector.Normalize();
    let currentPrecision = Vector2.Divide(speedVectorLength, 2);
    let intersectionObject;

    do {
        // Create moved sphere
        var checkPosition = player.Position + (speedVectorNormalized * (offset + currentPrecision));

        // Check for intersection
        var validationObject = new MapCircle(checkPosition, player.Radius);
        intersectionObject = CheckAnyIntersectionWithWorld(validationObject);

        // Update new position and offset
        if (typeof intersectionObject !== "undefined") // No object found, increase offset
            offset += currentPrecision;

        currentPrecision /= 2.0;

    } // Do this as long as we reach desired precision
    while (currentPrecision >= Config.INTERSECTION_INTERVAL);

    return speedVectorNormalized * offset;
};

PhysicsEngine.GetSpeedFromPlayerInput = function(player, timePassed) {

    let calculatedSpeedVector = new Vector2();

    // First get direction
    const keysState = KeyboardController.GetKeysState();
    if (keysState.indexOf(1) !== -1) // Up
        calculatedSpeedVector = Vector2.Add(calculatedSpeedVector, Vector2.UP_VECTOR());
    if (keysState.indexOf(2) !== -1) // Down
        calculatedSpeedVector = Vector2.Add(calculatedSpeedVector, Vector2.DOWN_VECTOR());
    if (keysState.indexOf(3) !== -1) // Left
        calculatedSpeedVector = Vector2.Add(calculatedSpeedVector, Vector2.LEFT_VECTOR());
    if (keysState.indexOf(4) !== -1) // Right
        calculatedSpeedVector = Vector2.Add(calculatedSpeedVector, Vector2.RIGHT_VECTOR());

    // Scale vector to be speed length
    if (!calculatedSpeedVector.IsDegenerated()) {
        const normalizedSpeed = calculatedSpeedVector.Normalize();
        const vectorLength = config.player_speed * timePassed;
        calculatedSpeedVector = normalizedSpeed * vectorLength;
    }

    return calculatedSpeedVector;
};

PhysicsEngine.CheckAnyIntersectionWithWorld = function (mapCircle) {
    // Check intersection with all map objects
    foreach(MapObject obj in MapState.Instance.MapObjects)
    {
        bool intersects = false;
        if (obj is MapRect)
        intersects = Intersection.CheckIntersection((MapRect)obj, mapCircle);
                else if (obj is MapCircle)
        intersects = Intersection.CheckIntersection((MapCircle)obj, mapCircle);

        if (intersects)
            return obj;
    }

    return null;
};

// Export module
if (typeof module !== 'undefined' && module.hasOwnProperty('exports')) {
    module.exports = PhysicsEngine;
}