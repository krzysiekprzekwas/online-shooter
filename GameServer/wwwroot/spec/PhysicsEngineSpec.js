
const PhysicsEngine = require('../js/Physics/PhysicsEngine.js');
const worldController = require('../js/Controllers/WorldController.js');
const Player = require('../js/Physics/Player.js');
const config = require('../js/config.js');

describe('PhysicsEngine', function () {

    it('should correctly check intersection with map objects', () => {

        // Arrange
        const physicsEngine = new PhysicsEngine(worldController);
        worldController.mapObjects = [new MapRect(1, 1, 3, 3)];

        // Act
        const intersectionObject = physicsEngine.CheckAnyIntersectionWithWorld(new MapCircle(1, 1, 2));

        // Assert
        expect(intersectionObject).not.toBe(null);
    });

    it('should not detect intersection with world when no map objects defined', () => {

        // Arrange
        const physicsEngine = new PhysicsEngine(worldController);
        worldController.mapObjects = [];

        // Act
        const intersectionObject = physicsEngine.CheckAnyIntersectionWithWorld(new MapCircle(1, 1, 2));

        // Assert
        expect(intersectionObject).toBe(null);
    });

    it('should not detect intersection with world when some map objects are defined but they dont intersects', () => {

        // Arrange
        const physicsEngine = new PhysicsEngine(worldController);
        worldController.mapObjects = [new MapRect(10, 1, 3, 3), new MapRect(4, -3, 2, 3), new MapRect(-1, -2, 1, 1)];

        // Act
        const intersectionObject = physicsEngine.CheckAnyIntersectionWithWorld(new MapCircle(1, 1, 2));

        // Assert
        expect(intersectionObject).toBe(null);
    });

    it('should calculate possible movement vector when no obstacles in front', () => {

        // Arrange
        const physicsEngine = new PhysicsEngine(worldController);
        worldController.mapObjects = [new MapRect(0, -20, 2, 2)];

        const player = new Player();
        player.SetPosition(new Vector2(0, 0));

        const speedVector = new Vector2(0, 16);

        // Act
        const [possibleMovementVector, spareLength] = physicsEngine.CalculatePossibleMovement(player, speedVector);

        // Assert
        const expectedPossibleMovement = new Vector2(0, 16);
        expect(Math.abs(possibleMovementVector.GetX() - expectedPossibleMovement.GetX())).toBeLessThan(config.intersectionInterval);
        expect(Math.abs(possibleMovementVector.GetY() - expectedPossibleMovement.GetY())).toBeLessThan(config.intersectionInterval);
    });

    it('should calculate possible movement vector when one obstacle in front', () => {

        // Arrange
        const physicsEngine = new PhysicsEngine(worldController);
        worldController.mapObjects = [new MapRect(0, 20, 2, 2)];

        const player = new Player();
        player.SetPosition(new Vector2(0, 0));

        const speedVector = new Vector2(0, 16);

        // Act
        const [possibleMovementVector, spareLength] = physicsEngine.CalculatePossibleMovement(player, speedVector);

        // Assert
        const expectedPossibleMovement = new Vector2(0, 3);
        expect(Math.abs(possibleMovementVector.GetX() - expectedPossibleMovement.GetX())).toBeLessThan(config.intersectionInterval);
        expect(Math.abs(possibleMovementVector.GetY() - expectedPossibleMovement.GetY())).toBeLessThan(config.intersectionInterval);
    });
    
    
});
