
const Vector2 = require('../js/Physics/Vector2.js');
const Physic = require('../js/Physics/Physic.js');

describe('Intersection', function () {

    it('should correctly rotate vector', () => {

        // Arrage
        const vector = new Vector2(10, 0);
        const angle = Math.PI / 4;

        // Act
        const rotatedVector = Physic.RotateVector(vector, angle);

        // Assert
        const expectedVector = Vector2.Multiply(Vector2.Normalize(new Vector2(1, 1)), vector.Length());

        expect(rotatedVector.GetX()).toBeCloseTo(expectedVector.GetX(), 6);
        expect(rotatedVector.GetY()).toBeCloseTo(expectedVector.GetY(), 6);
    });

    it('should correctly rotate vector by negative angle', () => {

        // Arrage
        const vector = new Vector2(0, -3);
        const angle = Math.PI / -2;

        // Act
        const rotatedVector = Physic.RotateVector(vector, angle);

        // Assert
        const expectedVector = new Vector2(-3, 0);

        expect(rotatedVector.GetX()).toBeCloseTo(expectedVector.GetX(), 6);
        expect(rotatedVector.GetY()).toBeCloseTo(expectedVector.GetY(), 6);
    });

    it('should correctly project vector onto another1', () => {

        // Arrage
        const vector = new Vector2(2, 1);
        const projectionVector = new Vector2(1, 0);

        // Act
        const projectedVector = Physic.ProjectVector(vector, projectionVector);

        // Assert
        const expectedVector = new Vector2(2, 0);
        expect(projectedVector.Equals(expectedVector)).toBe(true);
    });

    it('should correctly project vector onto another2', () => {

        // Arrage
        const vector = new Vector2(-2, -1);
        const projectionVector = new Vector2(-1, 1);

        // Act
        const projectedVector = Physic.ProjectVector(vector, projectionVector);

        // Assert
        const expectedVector = new Vector2(-0.5, 0.5);
        expect(projectedVector.GetX()).toBeCloseTo(expectedVector.GetX(), 6);
        expect(projectedVector.GetY()).toBeCloseTo(expectedVector.GetY(), 6);
    });

    it('should correctly project vector onto another3', () => {

        // Arrage
        const vector = new Vector2(2, 1);
        const projectionVector = new Vector2(-1, 0);

        // Act
        const projectedVector = Physic.ProjectVector(vector, projectionVector);

        // Assert
        const expectedVector = new Vector2(2, 0);
        expect(projectedVector.Equals(expectedVector)).toBe(true);
    });

    it('should correctly project vector onto another4', () => {

        // Arrage
        const vector = new Vector2(0.55242717280199, 0.55242717280199);
        const projectionVector = Vector2.UP_VECTOR();

        // Act
        const projectedVector = Physic.ProjectVector(vector, projectionVector);

        // Assert
        const expectedVector = new Vector2(0, 0.55242717280199);
        expect(projectedVector.Equals(expectedVector)).toBe(true);
    });

    it('should correctly calculate parallel vector to normal1', () => {

        // Arrage
        const vector = new Vector2(2, 1);
        const normalVector = new Vector2(0, -1);

        // Act
        const parallelVector = Physic.GetParallelVectorToNormal(vector, normalVector);

        // Assert
        const expectedVector = new Vector2(2, 0);
        expect(parallelVector.GetX()).toBeCloseTo(expectedVector.GetX(), 6);
        expect(parallelVector.GetY()).toBeCloseTo(expectedVector.GetY(), 6);
    });

    it('should correctly calculate parallel vector to normal2', () => {

        // Arrage
        const vector = new Vector2(-2, -1);
        const normalVector = new Vector2(1, 1);

        // Act
        const parallelVector = Physic.GetParallelVectorToNormal(vector, normalVector);

        // Assert
        const expectedVector = new Vector2(-0.5, 0.5);
        expect(parallelVector.GetX()).toBeCloseTo(expectedVector.GetX(), 6);
        expect(parallelVector.GetY()).toBeCloseTo(expectedVector.GetY(), 6);
    });

    it('should correctly calculate parallel vector to normal3', () => {

        // Arrage
        const vector = new Vector2(-1, -1);
        const normalVector = new Vector2(1, 0);

        // Act
        const parallelVector = Physic.GetParallelVectorToNormal(vector, normalVector);

        // Assert
        const expectedVector = new Vector2(0, -1);
        expect(parallelVector.GetX()).toBeCloseTo(expectedVector.GetX(), 6);
        expect(parallelVector.GetY()).toBeCloseTo(expectedVector.GetY(), 6);
    });

    it('should correctly calculate parallel vector to normal4', () => {

        // Arrage
        const vector = new Vector2(0, 3);
        const normalVector = new Vector2(-1, -1);

        // Act
        const parallelVector = Physic.GetParallelVectorToNormal(vector, normalVector);

        // Assert
        const expectedVector = Vector2.Multiply(Vector2.Normalize(new Vector2(-1, 1)), ((vector.Length() * Math.sqrt(2)) / 2));
        expect(parallelVector.GetX()).toBeCloseTo(expectedVector.GetX(), 6);
        expect(parallelVector.GetY()).toBeCloseTo(expectedVector.GetY(), 6);
    });

    it('should correctly calculate prallel vector to intersection normal1', () => {

        // Arrage
        const movementVector = new Vector2(0, 2);
        const intersectionDistance = 1;
        const intersectionNormal = new Vector2(-1, -1);

        // Act
        const parallelVector = Physic.GetLeftParallelVectorToIntersectionNormal(movementVector, intersectionDistance, intersectionNormal);

        // Assert
        const expectedVector = Vector2.Multiply(Vector2.Normalize(new Vector2(-1, 1)), (((movementVector.Length() - intersectionDistance) * Math.sqrt(2)) / 2));
        expect(parallelVector.GetX()).toBeCloseTo(expectedVector.GetX(), 6);
        expect(parallelVector.GetY()).toBeCloseTo(expectedVector.GetY(), 6);
    });

    it('should correctly calculate prallel vector to intersection normal2', () => {

        // Arrage
        const movementVector = new Vector2(-3, 3);
        const intersectionDistance = Math.sqrt(2);
        const intersectionNormal = new Vector2(-1, 0);

        // Act
        const parallelVector = Physic.GetLeftParallelVectorToIntersectionNormal(movementVector, intersectionDistance, intersectionNormal);

        // Assert
        const expectedVector = Vector2.Multiply(Vector2.Normalize(new Vector2(0, 1)), (((movementVector.Length() - intersectionDistance) * Math.sqrt(2)) / 2));
        expect(parallelVector.GetX()).toBeCloseTo(expectedVector.GetX(), 6);
        expect(parallelVector.GetY()).toBeCloseTo(expectedVector.GetY(), 6);
    });
});
