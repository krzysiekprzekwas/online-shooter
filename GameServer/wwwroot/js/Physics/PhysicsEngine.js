const Vector2 = require('./Vector2.js');
const Intersection = require('./Intersection.js');
const MapCircle = require('../MapObjects/MapCircle.js');
const MapRect = require('../MapObjects/MapRect.js');

function PhysicsEngine() {

    const that = this;

    that.players = new Array();
    that.bullets = new Array();
    that.mapObjects = new Array();

    that.ApplyPhysics = function(players) {

        that.players.forEach(player => {

            player.SetSpeed(Vector2.Multiply(player.Speed, config.playerDecceleration));

            const speedVector = Vector2.Add(player.Speed, that.GetSpeedFromPlayerInput(player, timePassed));

            that.UpdatePlayerPosition(player, speedVector);
        });

        that.bullets.filter(b => b.GetSpeed().Length() < config.minBulletspeed);

        that.bullets.forEach(bullet => {

            bullet.SetSpeed(Vector2.Multiply(bullet.GetSpeed(), 0.99));
            bullet.SetPosition(Vector2.Add(bullet.GetPosition(), bullet.GetSpeed()));
        });

        that.bullets.filter(b => that.CheckAnyIntersectionWithWorld(new MapCircle(b.GetPosition(), b.GetRadius()) !== null));
    };

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
            const speedVectorScaled = Vector2.Multiply(speedVectorNormalized, offset + currentPrecision);
            const checkPosition = Vector2.Add(player.Position, speedVectorScaled);
    
            // Check for intersection
            const validationObject = new MapCircle(checkPosition, player.Radius);
            intersectionObject = that.CheckAnyIntersectionWithWorld(validationObject);
    
            // Update new position and offset
            if (intersectionObject !== null) // No object found, increase offset
                offset += currentPrecision;
    
            currentPrecision /= 2.0;
    
        } // Do this as long as we reach desired precision
        while (currentPrecision >= config.intersectionInterval);
    
        return Vector2.Multiply(speedVectorNormalized, offset);
    };
    
    that.CheckAnyIntersectionWithWorld = function (mapCircle) {

        // Check intersection with all map objects
        that.MapObjects.forEach(obj => {

            if (Intersection.CheckIntersection(obj, mapCircle))
                return obj;
        });

        return null;
    };
}


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

// Export module
if (typeof module !== 'undefined' && module.hasOwnProperty('exports')) {
    module.exports = PhysicsEngine;
}