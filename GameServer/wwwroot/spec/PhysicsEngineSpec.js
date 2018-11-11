
const PhysicsEngine = require('../js/Physics/PhysicsEngine.js');
const worldController = require('../js/Controllers/WorldController.js');
const Player = require('../js/Physics/Player.js');
const Bullet = require('../js/Physics/Bullet.js');
const config = require('../js/config.js');

describe('PhysicsEngine', function () {

    afterEach(() => {

        worldController.players = [];
        worldController.bullets = [];
        worldController.mapObjects = [];
    });

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
        const expectedSpareLength = 0;
        expect(Math.abs(spareLength - expectedSpareLength)).toBeLessThan(config.intersectionInterval);
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
        const expectedSpareLength = 13;
        expect(Math.abs(spareLength - expectedSpareLength)).toBeLessThan(config.intersectionInterval);
        expect(Math.abs(possibleMovementVector.GetX() - expectedPossibleMovement.GetX())).toBeLessThan(config.intersectionInterval);
        expect(Math.abs(possibleMovementVector.GetY() - expectedPossibleMovement.GetY())).toBeLessThan(config.intersectionInterval);
    });

    it('should update player position including parellel movement', () => {

        // Arrange
        const physicsEngine = new PhysicsEngine(worldController);
        worldController.mapObjects = [new MapRect(0, 17, 2, 2)];

        const player = new Player();
        player.SetPosition(new Vector2(0, 0));
        player.SetSpeed(new Vector2(15, 15));

        // Act
        physicsEngine.UpdatePlayerPosition(player, player.GetSpeed());

        // Assert
        const expectedXPosition = 15;
        expect(Math.abs(player.GetPosition().GetX() - expectedXPosition)).toBeLessThan(config.intersectionInterval);
    });
    
    it('should correctly extrapolate bullet physics', () => {

        // Arrange
        const physicsEngine = new PhysicsEngine(worldController);

        const bullet = new Bullet();
        bullet.SetPosition(new Vector2(0, 0));
        bullet.SetSpeed(new Vector2(20, 20));
        worldController.bullets = [bullet];

        config.bulletDecceleraionFactorPerTick = 0.5;
        config.serverTickMilliseconds = 500;

        physicsEngine.HasTickPassed = () => true;
        physicsEngine.GetMillisecondsPassed = () => 500;

        // Act
        physicsEngine.ExtrapolatePhysics();

        // Assert
        expect(bullet.GetSpeed().GetX()).toBe(10);
        expect(bullet.GetX()).toBe(20);
    });

    it('should correctly remove too slow bullets', () => {

        // Arrange
        const physicsEngine = new PhysicsEngine(worldController);

        const bullet = new Bullet();
        bullet.SetPosition(new Vector2(0, 0));
        bullet.SetSpeed(Vector2.Multiply(new Vector2(20, 20).Normalize(), config.minBulletSpeed));
        worldController.bullets = [bullet];

        physicsEngine.GetMillisecondsPassed = () => 500;
        physicsEngine.HasTickPassed = () => true;

        // Act
        physicsEngine.ExtrapolatePhysics();

        // Assert
        expect(worldController.bullets.length).toBe(0);
    });

    it('should correctly remove bullets that hit wall', () => {

        // Arrange
        const physicsEngine = new PhysicsEngine(worldController);
        worldController.mapObjects = [new MapRect(0, 2, 2, 2)];

        const bullet = new Bullet();
        bullet.SetPosition(new Vector2(0, 0));
        bullet.SetSpeed(new Vector2(0, 1));
        bullet.SetRadius(1);
        worldController.bullets = [bullet];

        physicsEngine.GetMillisecondsPassed = () => 500;
        physicsEngine.HasTickPassed = () => true;

        // Act
        physicsEngine.ExtrapolatePhysics();

        // Assert
        expect(worldController.bullets.length).toBe(0);
        expect(bullet.GetSpeed().Length()).toBeGreaterThan(config.minBulletSpeed);
    });

    it('should correctly deccelerate bullet', () => {

        // Arrange
        const physicsEngine = new PhysicsEngine(worldController);

        const bullet = new Bullet();
        bullet.SetPosition(new Vector2(0, 0));
        bullet.SetSpeed(new Vector2(32, 0));
        worldController.bullets = [bullet];

        // Ticks mock
        const serverTickMilliseconds = 200;
        const simulationServerTicksCount = 2;
        const simulationClientTicksCount = 4;

        config.serverTickMilliseconds = serverTickMilliseconds;
        config.playerDeccelerationFactorPerTick = 0.5;
        physicsEngine.GetMillisecondsPassed = () => serverTickMilliseconds / simulationClientTicksCount;

        // Act
        for(let serverTick = 1; serverTick <= simulationServerTicksCount; serverTick += 1) {
            for(let clientTick = 1; clientTick <= simulationClientTicksCount; clientTick += 1) {

                physicsEngine.HasTickPassed = () => false;
                if(clientTick == simulationClientTicksCount)
                    physicsEngine.HasTickPassed = () => true;

                physicsEngine.ExtrapolatePhysics();
            }

            if(serverTick === 1) {
                expect(bullet.GetPosition().GetX()).toBe(32);
                expect(bullet.GetSpeed().GetX()).toBe(16);
            }
        }

        // Assert
        expect(bullet.GetPosition().GetX()).toBe(48);
        expect(bullet.GetSpeed().GetX()).toBe(8);
    });

    it('should correctly extrapolate players', () => {

        // Arrange
        const physicsEngine = new PhysicsEngine(worldController);

        const player = new Player();
        player.SetPosition(new Vector2(0, 0));
        player.SetSpeed(new Vector2(10, -10));
        worldController.players = [player];

        config.serverTickMilliseconds = 1000;
        
        physicsEngine.GetMillisecondsPassed = () => 500;

        // Act
        physicsEngine.ExtrapolatePhysics();

        // Assert
        const expectedPosition = new Vector2(5, -5);
        expect(Math.abs(player.GetX() - expectedPosition.GetX())).toBeLessThan(config.intersectionInterval);
        expect(Math.abs(player.GetY() - expectedPosition.GetY())).toBeLessThan(config.intersectionInterval);
    });

    it('should not allow players to enter wall during extrapolation', () => {

        // Arrange
        const physicsEngine = new PhysicsEngine(worldController);
        worldController.mapObjects = [new MapRect(0, 15, 10, 10)];

        const player = new Player();
        player.SetPosition(new Vector2(0, -0.0000001));
        player.SetSpeed(new Vector2(0, 10));
        player.SetRadius(10);
        worldController.players = [player];

        physicsEngine.GetMillisecondsPassed = () => 500;

        // Act
        physicsEngine.ExtrapolatePhysics();

        // Assert
        const expectedPosition = new Vector2(0, 0);
        expect(Math.abs(player.GetX() - expectedPosition.GetX())).toBeLessThan(config.intersectionInterval);
        expect(Math.abs(player.GetY() - expectedPosition.GetY())).toBeLessThan(config.intersectionInterval);
    });

    it('should calculate parallel movement for players at extrapolation', () => {

        // Arrange
        const physicsEngine = new PhysicsEngine(worldController);
        worldController.mapObjects = [new MapRect(0, 15, 10, 10)];

        const player = new Player();
        player.SetPosition(new Vector2(0, -0.0000001));
        player.SetSpeed(new Vector2(10, 10));
        player.SetRadius(10);
        worldController.players = [player];

        config.serverTickMilliseconds = 1000;

        physicsEngine.GetMillisecondsPassed = () => 500;

        // Act
        physicsEngine.ExtrapolatePhysics();

        // Assert
        const expectedPosition = new Vector2(5, 0);
        expect(Math.abs(player.GetX() - expectedPosition.GetX())).toBeLessThan(config.intersectionInterval);
        expect(Math.abs(player.GetY() - expectedPosition.GetY())).toBeLessThan(config.intersectionInterval);
    });

});
