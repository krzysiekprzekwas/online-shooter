const Intersection = require('../js/Physics/Intersection.js');
const MapCircle = require('../js/MapObjects/MapCircle.js');
const MapRect = require('../js/MapObjects/MapRect.js');

describe('Intersection', function () {

    it('should detect intersection between two circles when one is inside', () => {

        // Arrage
        const c1 = new MapCircle(0, 0, 1);
        const c2 = new MapCircle(1, 1, 0.5);

        // Act
        const intersects = Intersection.CheckIntersection(c1, c2);

        // Assert
        expect(intersects).toBe(true);
    });

    it('should detect intersection between two circles when they touch each other', () => {
        
        // Arrage
        const c1 = new MapCircle(0, 0, 1);
        const c2 = new MapCircle(0, 2, 1);

        // Act
        const intersects = Intersection.CheckIntersection(c1, c2);

        // Assert
        expect(intersects).toBe(true);
    });

    it('should not detect intersection between two circles when they dont intersect', () => {
        
        // Arrage
        const c1 = new MapCircle(0, 0, 1);
        const c2 = new MapCircle(0, 2.1, 1);

        // Act
        const intersects = Intersection.CheckIntersection(c1, c2);

        // Assert
        expect(intersects).toBe(false);
    });

    it('should detect intersection between two circles when one is centered inside another', () => {
        
        // Arrage
        const c1 = new MapCircle(0, 0, 1);
        const c2 = new MapCircle(0, 0, 0.5);

        // Act
        const intersects = Intersection.CheckIntersection(c1, c2);

        // Assert
        expect(intersects).toBe(true);
    });

    it('should detect intersection between two rects when they intersects', () => {
        
        // Arrange
        const r1 = new MapRect(0, 0, 2, 2);
        const r2 = new MapRect(1.5, 1.5, 1, 1);

        // Act
        const intersects = Intersection.CheckIntersection(r1, r2);

        // Assert
        expect(intersects).toBe(true);
    });

    it('should detect intersection between two rects when they intersects 2', () => {
        
        // Arrange
        const r1 = new MapRect(0, 0, 10, 10);
        const r2 = new MapRect(6, 0, 5, 1);

        // Act
        const intersects = Intersection.CheckIntersection(r1, r2);

        // Assert
        expect(intersects).toBe(true);
    });

    it('should not detect intersection between two rects when they dont intersects', () => {
        
        // Arrange
        const r1 = new MapRect(0, 0, 2, 2);
        const r2 = new MapRect(0, 3, 2, 2);

        // Act
        const intersects = Intersection.CheckIntersection(r1, r2);

        // Assert
        expect(intersects).toBe(false);
    });

    it('should detect intersection between two rects when they touch each other', () => {
        
        // Arrange
        const r1 = new MapRect(0, 0, 2, 2);
        const r2 = new MapRect(2, 2, 2, 2);

        // Act
        const intersects = Intersection.CheckIntersection(r1, r2);

        // Assert
        expect(intersects).toBe(true);
    });

    it('should detect intersection between two rects when one is inside other', () => {
        
        // Arrange
        const r1 = new MapRect(0, 0, 2, 2);
        const r2 = new MapRect(0, 0, 0.1, 0.1);

        // Act
        const intersects = Intersection.CheckIntersection(r1, r2);

        // Assert
        expect(intersects).toBe(true);
    });

    it('should detect intersection between rect and circle when they intersect', () => {
        
        // Arrange
        const c = new MapCircle(0, 0, 1);
        const r = new MapRect(1, 0, 1, 1);

        // Act
        const intersects = Intersection.CheckIntersection(c, r);

        // Assert
        expect(intersects).toBe(true);
    });

    it('should not detect intersection between rect and circle when they dont intersect', () => {
        
        // Arrange
        const c = new MapCircle(0, 0, 1);
        const r = new MapRect(2, 0, 1, 1);

        // Act
        const intersects = Intersection.CheckIntersection(c, r);

        // Assert
        expect(intersects).toBe(false);
    });

    it('should detect intersection between rect and circle when rect is inside circle', () => {
        
        // Arrange
        const c = new MapCircle(0, 0, 5);
        const r = new MapRect(0, 0, 1, 1);

        // Act
        const intersects = Intersection.CheckIntersection(c, r);

        // Assert
        expect(intersects).toBe(true);
    });

    it('should detect intersection between rect and circle when circle is inside rect', () => {
        
        // Arrange
        const c = new MapCircle(0, 0, 0.5);
        const r = new MapRect(0, 0, 5, 5);

        // Act
        const intersects = Intersection.CheckIntersection(r, c);

        // Assert
        expect(intersects).toBe(true);
    });

    it('should detect intersection between rect and circle when they touch each other', () => {
        
        // Arrange
        const c = new MapCircle(0, 0, 1);
        const r = new MapRect(2, 0, 2, 2);

        // Act
        const intersects = Intersection.CheckIntersection(r, c);

        // Assert
        expect(intersects).toBe(true);
    });

    it('should not detect intersection between rect and circle when they almost touch each other', () => {
        
        // Arrange
        const c = new MapCircle(0, 0, 1);
        const r = new MapRect(2.01, 0, 2, 2);

        // Act
        const intersects = Intersection.CheckIntersection(r, c);

        // Assert
        expect(intersects).toBe(false);
    });
});
