
if (typeof require !== "undefined") {
    Intersection = require('./Intersection.js');
    config = require('../config.js');
}

function PhysicsEngine(worldController) {

    const that = this;
    that.worldController = worldController;
    that.LastExtrapolationDate = new Date();

    that.ExtrapolatePhysics = function() {

        const now = new Date();
        const millisecondsPassed = now - that.LastExtrapolationDate;
        that.LastExtrapolationDate = now;

        const tickPercentage = 1000 / config.serverTick / millisecondsPassed;

        that.worldController.players.forEach(player => {
            
            const speedVector = Vector2.Multiply(player.GetSpeed(), tickPercentage);
            that.UpdatePlayerPosition(player, speedVector);
        });
        
        that.worldController.bullets.forEach(bullet => {

            const speedVector = Vector2.Multiply(bullet.GetSpeed(), tickPercentage);
            bullet.SetPosition(Vector2.Add(bullet.GetPosition(), speedVector));
        });
    };

    that.CalculatePossibleMovement = function (player, speedVector) {
    
        if (speedVector.IsDegenerated())
            return [ Vector2.ZERO_VECTOR(), 0 ];
    
        // Variables used to calculate speed vector
        let offset = 0;
        const speedVectorLength = speedVector.Length();
        const speedVectorNormalized = speedVector.Normalize();
        let currentPrecision = speedVectorLength / 2;
        let intersectionObject;

        do {
            // Create moved sphere
            const speedVectorScaled = Vector2.Multiply(speedVectorNormalized, offset + currentPrecision);
            const checkPosition = Vector2.Add(player.GetPosition(), speedVectorScaled);
    
            // Check for intersection
            const validationObject = new MapCircle(checkPosition.GetX(), checkPosition.GetY(), player.GetRadius());
            intersectionObject = that.CheckAnyIntersectionWithWorld(validationObject);
    
            // Update new position and offset
            if (intersectionObject === null) // No object found, increase offset
                offset += currentPrecision;
            
            currentPrecision /= 2.0;
    
        } // Do this as long as we reach desired precision
        while (currentPrecision * 2 >= config.intersectionInterval);
    
        return [ Vector2.Multiply(speedVectorNormalized, offset), speedVectorLength - offset ];
    };
    
    that.CheckAnyIntersectionWithWorld = function (mapCircle) {

        return that.worldController.mapObjects.filter(o => Intersection.CheckIntersection(o, mapCircle))[0] || null;
    };

    that.UpdatePlayerPosition = function (player, speedVector) {

        const [movementVector, spareLength] = that.CalculatePossibleMovement(player, speedVector);
        player.SetPosition(Vector2.Add(player.GetPosition(), movementVector));
        player.SetSpeed(movementVector);

        if (spareLength > 0) {
            var spareSpeedVector = Vector2.Multiply(speedVector.Normalize(), spareLength);

            var horizontalVector = Physic.ProjectVector(spareSpeedVector, Vector2.LEFT_VECTOR());
            var verticalVector = Physic.ProjectVector(spareSpeedVector, Vector2.UP_VECTOR());

            const [parallelHorizontalMovementVector, horizontalSpareLength] =
                that.CalculatePossibleMovement(player, horizontalVector);

            const [parallelVerticalMovementVector, verticalSpareLength] =
                that.CalculatePossibleMovement(player, verticalVector);

            const parallelMovementVector = horizontalSpareLength < verticalSpareLength
                ? parallelHorizontalMovementVector
                : parallelVerticalMovementVector;

            player.SetPosition(Vector2.Add(player.GetPosition(), parallelMovementVector));
            player.SetSpeed(parallelMovementVector);
        }
    };
}

PhysicsEngine.GetSpeedFromPlayerInput = function(timePassed) {

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