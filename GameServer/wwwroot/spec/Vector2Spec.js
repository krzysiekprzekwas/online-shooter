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

    });

    it('normalize throws error when length is 0', () => {

    });

    it('can be safely normalized', () => {

    });

    it('length can be caluclated', () => {

    });

    it('square length can be calculated', () => {

    });

    it('can caluculate distance', () => {

    });

    it('can calculate distance squared', () => {

    });

    it('can calculate angle between vectors', () => {

    });

    it('can calculate dot product', () => {

    });
});