const MapCircle = require('../js/MapObjects/MapCircle.js');
const MapRect = require('../js/MapObjects/MapRect.js');
const Vector2 = require('../js/Physics/Vector2.js');

describe('MapObjects', function () {

    it('can create MapCircle instance', () => {

        // Arange
        const mapCircle = new MapCircle(1, 10, 3);

        // Assert
        expect(mapCircle.GetX()).toEqual(1);
        expect(mapCircle.GetY()).toEqual(10);
        expect(mapCircle.GetRadius()).toEqual(3);
    });

    it('can modify MapCircle object', () => {

        // Arrange
        const mapCircle = new MapCircle(1, 10, 3);

        // Act
        mapCircle.SetX(2);
        mapCircle.SetY(4);
        mapCircle.SetRadius(5);

        // Assert
        expect(mapCircle.GetX()).toEqual(2);
        expect(mapCircle.GetY()).toEqual(4);
        expect(mapCircle.GetRadius()).toEqual(5);
    });

    it('can modify MapCircle position', () => {

        // Arrange
        const mapCircle = new MapCircle(1, 10, 3);

        // Act
        mapCircle.SetPosition(new Vector2(11, 12));

        // Assert
        expect(mapCircle.GetX()).toEqual(11);
        expect(mapCircle.GetY()).toEqual(12);
        expect(mapCircle.GetPosition().Equals(new Vector2(11, 12))).toBe(true);
    });

    it('can compare MapCircle objects', () => {

        // Arrange
        const mapCircleA = new MapCircle(1, 2, 3);
        const mapCircleB = new MapCircle(1, 2, 3);
        const mapCircleC = new MapCircle(1, 2, 4);
        const mapCircleD = new MapCircle(1, 3, 3);

        // Assert
        expect(mapCircleA.Equals(mapCircleB)).toBe(true);
        expect(mapCircleA.Equals(mapCircleC)).toBe(false);
        expect(mapCircleA.Equals(mapCircleD)).toBe(false);
    });

    it('can clone MapCircle objects', () => {

        // Arrange
        const mapCircle = new MapCircle(1, 10, 3);

        // Act
        const clonedMapCircle = MapCircle.Clone(mapCircle);

        mapCircle.SetX(11);
        clonedMapCircle.SetY(12);

        // Assert
        expect(mapCircle.GetX()).toEqual(11);
        expect(clonedMapCircle.GetX()).toEqual(1);

        expect(mapCircle.GetY()).toEqual(10);
        expect(clonedMapCircle.GetY()).toEqual(12);
    });

    it('can caluclate squared radius of MapCircle', () => {

        // Arrange
        const mapCircle = new MapCircle(1, 10, 3);

        // Act
        const squaredRadius = mapCircle.RadiusSquared();

        // Assert
        expect(squaredRadius).toEqual(9);
    });

    it('can create MapRect instance', () => {

        // Arange
        const mapRect = new MapRect(1, 10, 3, 5);

        // Assert
        expect(mapRect.GetX()).toEqual(1);
        expect(mapRect.GetY()).toEqual(10);
        expect(mapRect.GetWidth()).toEqual(3);
        expect(mapRect.GetHeight()).toEqual(5);
    });

    it('can modify MapRect object', () => {

        // Arrange
        const mapRect = new MapRect(1, 10, 3, 5);

        // Act
        mapRect.SetX(2);
        mapRect.SetY(4);
        mapRect.SetWidth(7);
        mapRect.SetHeight(6);

        // Assert
        expect(mapRect.GetX()).toEqual(2);
        expect(mapRect.GetY()).toEqual(4);
        expect(mapRect.GetWidth()).toEqual(7);
        expect(mapRect.GetHeight()).toEqual(6);
    });

    it('can modify MapRect position', () => {

        // Arrange
        const mapRect = new MapRect(1, 10, 3, 5);

        // Act
        mapRect.SetPosition(new Vector2(11, 12));

        // Assert
        expect(mapRect.GetX()).toEqual(11);
        expect(mapRect.GetY()).toEqual(12);
        expect(mapRect.GetPosition().Equals(new Vector2(11, 12))).toBe(true);
    });

    it('can compare MapRect objects', () => {

        // Arrange
        const mapRectA = new MapRect(1, 2, 3, 4);
        const mapRectB = new MapRect(1, 2, 3, 4);
        const mapRectC = new MapRect(1, 2, 4, 4);
        const mapRectD = new MapRect(1, 3, 3, 5);

        // Assert
        expect(mapRectA.Equals(mapRectB)).toBe(true);
        expect(mapRectA.Equals(mapRectC)).toBe(false);
        expect(mapRectA.Equals(mapRectD)).toBe(false);
    });

    it('can clone MapRect objects', () => {

        // Arrange
        const mapRect = new MapRect(1, 10, 3);

        // Act
        const clonedMapRect = MapRect.Clone(mapRect);

        mapRect.SetX(11);
        clonedMapRect.SetY(12);

        // Assert
        expect(mapRect.GetX()).toEqual(11);
        expect(clonedMapRect.GetX()).toEqual(1);

        expect(mapRect.GetY()).toEqual(10);
        expect(clonedMapRect.GetY()).toEqual(12);
    });

});
