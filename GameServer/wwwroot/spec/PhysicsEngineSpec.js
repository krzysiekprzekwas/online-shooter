
const Vector2 = require('../js/Physics/Vector2.js');
const Physic = require('../js/Physics/Physic.js');

describe('PhysicsEngine', function () {

    it('should correctly rotate vector', () => {

        // Arrange
        var player = new Player();
        player.Keys.Add(KeyEnum.Up);
        player.Keys.Add(KeyEnum.Left);

        // Act
        var nromalizedSpeedVector = PhysicsEngine.GetSpeedFromPlayerInput(player).Normalize();

        // Assert
        var expectedSpeedVectorDirection = Vector2.Normalize(new Vector2(-1, -1));
        Assert.AreEqual(expectedSpeedVectorDirection.X, nromalizedSpeedVector.X, 1e-6);
        Assert.AreEqual(expectedSpeedVectorDirection.Y, nromalizedSpeedVector.Y, 1e-6);

    });

});
