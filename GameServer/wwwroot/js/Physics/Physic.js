const Vector2 = require('./Vector2.js');

function Physic() {

};

Physic.GetLeftParallelVectorToIntersectionNormal = function(movementVector, intersectionDistance, intersectionNormal) {

    const realMovementDirectionVector = Vector2.Multiply(Vector2.Normalize(movementVector), intersectionDistance);
    const leftMovementDirectionVector = Vector2.Subtract(movementVector, realMovementDirectionVector);

    return Physic.GetParallelVectorToNormal(leftMovementDirectionVector, intersectionNormal);
};

Physic.GetParallelVectorToNormal = function (vector, normalVector) {
    return Physic.ProjectVector(vector, Physic.RotateVector(normalVector, Math.PI / 2.0));
};

Physic.ProjectVector = function (vector, projectionVector) {

    const normalizedProjectionVector = projectionVector.Normalize();
    const dotProduct = Vector2.Dot(vector, projectionVector);

    return Vector2.Multiply(normalizedProjectionVector, dotProduct / projectionVector.Length());
};

Physic.RotateVector = function (vector, angle) {

    const cosAngle = Math.cos(angle);
    const sinAngle = Math.sin(angle);

    const x = cosAngle * vector.GetX() - sinAngle * vector.GetY();
    const y = sinAngle * vector.GetX() + cosAngle * vector.GetY();
    return new Vector2(x, y);
};

// Export module
if (typeof module !== 'undefined' && module.hasOwnProperty('exports')) {
    module.exports = Physic;
}
