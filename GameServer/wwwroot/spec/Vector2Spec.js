const Vector2 = require('../js/Physics/Vector2.js');

describe('Vectors', function () {

    it('can be created with default constructor', () => {

        // Arrange
        const vectorA = new Vector2();

        // Assert
        expect(vectorA.GetX()).toEqual(0);
        expect(vectorA.GetY()).toEqual(0);
    });

    it('can be created with constructor', () => {

        // Arrange
        const vector = new Vector2(3, 4);

        // Assert
        expect(vector.GetX()).toEqual(3);
        expect(vector.GetY()).toEqual(4);
    });

    it('can be cloned', () => {

        // Arrange
        const vectorA = new Vector2(1, 2);

        // Act
        const copiedVector = Vector2.Clone(vectorA);
        vectorA.Set(3, 4);

        // Assert
        expect(vectorA.Equals(new Vector2(3, 4))).toBeTruthy();
        expect(copiedVector.Equals(new Vector2(1, 2))).toBeTruthy();
    });

    it('can be copied', () => {

        // Arrange
        const vectorA = new Vector2(1, 2);
        const vectorB = new Vector2();

        // Act
        vectorB.CopyFrom(vectorA);
        vectorA.Set(3, 4);

        // Assert
        expect(vectorA.Equals(new Vector2(3, 4))).toBeTruthy();
        expect(vectorB.Equals(new Vector2(1, 2))).toBeTruthy();
    });

    it('can be cloned with calculated length kept', () => {

        // Arrange
        const vector = new Vector2(3, 14);

        expect(vector._length).not.toBeDefined(); 
        vector.Length();
        expect(vector._length).toBeDefined(); 

        const copiedVector = Vector2.Clone(vector);
        expect(copiedVector._length).toBeDefined(); 
    });

    it('can be re-referenced', () => {

        // Arrange
        const vectorA = new Vector2(1, 2);
        let vectorB = new Vector2();

        // Act
        vectorB = vectorA;
        vectorA.Set(10, 11);

        // Assert
        expect(vectorA.Equals(new Vector2(10, 11))).toBeTruthy();
        expect(vectorB.Equals(new Vector2(10, 11))).toBeTruthy();
    });

    it('can be added', () => {

        // Arrange
        const vectorA = new Vector2(1, 2);
        const vectorB = new Vector2(1, 100);

        // Act
        const vectorC = Vector2.Add(vectorA, vectorB);

        // Assert
        expect(vectorC.Equals(new Vector2(2, 102))).toBeTruthy();
        expect(vectorA.Equals(new Vector2(1, 2))).toBeTruthy();
        expect(vectorB.Equals(new Vector2(1, 100))).toBeTruthy();
    });

    it('can be subtracted', () => {

        // Arrange
        const vectorA = new Vector2(10, 2);
        const vectorB = new Vector2(1, -100);

        // Act
        const vectorC = Vector2.Subtract(vectorA, vectorB);

        // Assert
        expect(vectorC.Equals(new Vector2(9, 102))).toBeTruthy();
        expect(vectorA.Equals(new Vector2(10, 2))).toBeTruthy();
        expect(vectorB.Equals(new Vector2(1, -100))).toBeTruthy();
    });

    it('can be multiplied', () => {

        // Arrange
        const vector = new Vector2(10, 20);
        const multiplier = 3;

        // Act
        const multipliedVector = Vector2.Multiply(vector, multiplier);

        // Assert
        expect(multipliedVector.Equals(new Vector2(30, 60))).toBeTruthy();
    });

    it('can be divided', () => {

        // Arrange
        const vector = new Vector2(2, 12);
        const divider = 2;

        // Act
        const dividedVector = Vector2.Divide(vector, divider);

        // Assert
        expect(dividedVector.Equals(new Vector2(1, 6))).toBeTruthy();
    });

    it('should throw exception when divided by 0', () => {

        // Arrange
        const vector = new Vector2(2, 12);
        const divider = 0;
        
        // Assert
        expect(() => Vector2.Divide(vector, divider)).toThrow("Division by 0");
    });

    it('provides predefined vectors', () => {

        // Arrange 
        const vector = Vector2.ZERO_VECTOR();

        // Act
        vector.Set(1, 1);

        // Assert
        expect(Vector2.ZERO_VECTOR().Equals(new Vector2(0, 0))).toBeTruthy();
        expect(vector.Equals(new Vector2(1, 1))).toBeTruthy();
    });

    it('can be stringified', () => {

        // Arrange
        const vector = new Vector2(2, 12);

        // Act
        const string = vector.ToString();

        // Assert
        expect(string).toEqual("<2 12>");
    });

    it('can verify degeneraded vector', () => {

        // Arrange
        const vector = new Vector2(0, 1e-300);

        // Act
        const isDegenerated = vector.IsDegenerated();

        // Assert
        expect(isDegenerated).toBeTruthy();
    });

    it('can normalize', () => {

        // Arrage
        const vector = new Vector2(-1, 1);

        // Act
        const normalized = Vector2.Normalize(vector);

        // Assert
        const root = Math.sqrt(2) / 2;
        const expectedVector = new Vector2(-root, root);

        expect(normalized.GetX()).toBeCloseTo(expectedVector.GetX(), 6);
        expect(normalized.GetY()).toBeCloseTo(expectedVector.GetY(), 6);
    });

    it('normalize throws error when length is 0', () => {

        // Arrange
        const vector = new Vector2();

        // Assert
        expect(() => Vector2.Normalize(vector)).toThrow();
    });

    it('can be safely normalized', () => {

        // Arrange
        const vector = new Vector2();

        // Act
        const normalized = vector.SafeNormalize();
        
        // Assert
        expect(normalized.Equals(Vector2.ZERO_VECTOR())).toBeTruthy();
    });

    it('length can be caluclated', () => {

        // Arrange
        const vector = new Vector2(3, 4);

        // Act
        const length = vector.Length();

        // Assert
        expect(length).toEqual(5);
    });

    it('square length can be calculated', () => {

        // Arrange
        const vector = new Vector2(10, 2);

        // Act
        const squaredLength = vector.LengthSquared();

        // Assert
        expect(squaredLength).toEqual(104);
    });

    it('can caluculate distance', () => {

        // Arrange
        const vectorA = new Vector2(-2, 1);
        const vectorB = new Vector2(3, 13);

        // Act
        const distance = Vector2.Distance(vectorA, vectorB);

        // Assert
        expect(distance).toEqual(13);
    });

    it('can calculate distance squared', () => {

        // Arrange
        const vectorA = new Vector2(-1, 3);
        const vectorB = new Vector2(-5, 9);

        // Act
        const squaredDistance = Vector2.DistanceSquared(vectorA, vectorB);

        // Assert
        expect(squaredDistance).toEqual(52);
    });

    it('can calculate angle between vectors', () => {

        // Arrage
        const vectorA = new Vector2(1, 1);
        const vectorB = new Vector2(1, -1);

        // Act
        const angle = Vector2.AngleBetweenVectors(vectorA, vectorB);

        // Assert
        const expectedAngle = Math.PI / 2;
        expect(angle).toBeCloseTo(expectedAngle, 4);
    });

    it('can calculate angle between vectors 2', () => {

        // Arrage
        const vectorA = new Vector2(0, 3);
        const vectorB = new Vector2(-1, 0);

        // Act
        const angle = Vector2.AngleBetweenVectors(vectorA, vectorB);

        // Assert
        const expectedAngle = Math.PI / -2;
        expect(angle).toBeCloseTo(expectedAngle, 4);
    });

    it('should throw exception when trying to find angle between ZERO_VECTOR', () => {

        // Arrange
        const vectorA = new Vector2(1, 1);

        // Assert
        expect(() => Vector2.AngleBetweenVectors(vectorA, Vector2.ZERO_VECTOR())).toThrow("One of given vectors is degenerated. Cannot calculate angle between them");
    });

    it('can calculate distance to moved vector', () => {

        // Arrange
        const vectorA = new Vector2(10, -5);
        const expectedDistance = 10;
        const vectorB = new Vector2(-1, 6);

        // Act
        const addVector = Vector2.Multiply(Vector2.Normalize(vectorB), expectedDistance);
        const vectorC = Vector2.Add(vectorA, addVector);
        const distance = Vector2.Distance(vectorA, vectorC);

        // Assert
        expect(distance).toEqual(expectedDistance);
    });

    it('can calculate dot product', () => {

        // Arrange
        const vectorA = new Vector2(-2, 1);
        const vectorB = new Vector2(-1, -1.5);

        // Act
        const dot = Vector2.Dot(vectorA, vectorB);

        // Assert
        expect(dot).toEqual(0.5);
    });

    it('can calculate angle from vector', () => {

        // Arrange
        const vector = new Vector2(-1, 1);

        // Act
        const angle = Vector2.Vector2ToRadian(vector);

        // Assert
        const expectedAngle = Math.PI * 3 / 4;
        expect(angle).toBeCloseTo(expectedAngle, 4);
    });

    it('can calculate angle from vector2', () => {

        // Arrange
        const vector = new Vector2(1, 0);

        // Act
        const angle = Vector2.Vector2ToRadian(vector);

        // Assert
        const expectedAngle = 0;
        expect(angle).toBeCloseTo(expectedAngle, 4);
    });

    it('can calculate vector from angle', () => {

        // Arange
        const angle = Math.PI / 2;

        // Act
        const vector = Vector2.RadianToVector2(angle);

        // Assert
        const expectedVector = new Vector2(0, 1);
        expect(vector.GetX()).toBeCloseTo(expectedVector.GetX(), 4);
        expect(vector.GetY()).toBeCloseTo(expectedVector.GetY(), 4);
    });

});