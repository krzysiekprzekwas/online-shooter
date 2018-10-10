class Vector2 {

    // Properties
    set X(val) {
        this._x = val;

        this._length = undefined;
        this._lengthSquared = undefined;
    }
    get X() {
        return this._x;
    }

    set Y(val) {
        this._y = val;

        this._length = undefined;
        this._lengthSquared = undefined;
    }
    get Y() {
        return this._y;
    }

    // Constrctors
    constructor(x = 0, y = 0) {
        
        this.X = x;
        this.Y = y;
    }

    Copy(vector) {

        this.X = vector.X;
        this.Y = vecotr.Y;
        this._length = vector._length;
        this._lengthSquared = vector._lengthSquared;
    }

    static Clone(vector) {

        const copiedVector = new Vector2(vector.X, vector.Y);

        copiedVector._length = vector._length;
        copiedVector._lengthSquared = vector._lengthSquared;

        return copiedVector;
    }

    // Vector operations
    LengthSquared() {

        if (typeof this._lengthSquared === "undefined")
            this._lengthSquared = Math.pow(this.X, 2) + Math.pow(this.Y, 2);

        return this._lengthSquared;
    }

    Length() {

        if (typeof this._length === "undefined")
            this._length = Math.sqrt(this.LengthSquared());

        return this._length;
    }

    Normalize() {

        if (this.IsDegenerated)
            throw "Degenerated vector given. Cannot normalzie zero vector.";

        const vector = Vector2.Copy(this);
        vector.Divide(this.Length());
        return vector;
    }

    SafeNormalize() {

        try {
            this.Normalize();
        }
        catch (err) {
            return new Vector2();
        }
    }

    IsDegenerated() {

        return this.Equals(Vector2.ZERO_VECTOR);
    }

    static Dot(vectorA, vectorB) {

        return (vectorA.X * vectorB.X) + (vectorA.Y * vectorB.Y);
    }

    static DistanceSquared(vectorA, vectorB) {

        return Math.pow(vectorA.X - vectorB.X, 2) + Math.pow(vectorA.Y - vectorB.Y, 2);
    }

    static Distance(vectorA, vectorB) {

        return Math.sqrt(Vector2.DistanceSquared(vectorA, vectorB));
    }

    static Normalize(vector) {

        return vector.Normalize();
    }

    static AngleBetweenVectors(vectorA, vectorB) {

        if (vectorA.IsDegenerated() || vectorB.IsDegenerated())
            throw "One of given vectors is degenerated. Cannot calculate angle between them";

        const sin = vectorB.X * vectorA.Y - vectorA.X * vectorB.Y;
        const cos = vectorB.X * vectorA.X + vectorB.Y * vectorA.Y;

        return Math.atan2(sin, cos);
    }

    // Mathematical operations
    static Add(vectorA, vectorB) {

        return new Vector2(vectorA.X + vectorB.X, vectorA.Y + vectorB.Y);
    }

    static Subtract(vectorA, vectorB) {

        return new Vector2(vectorA.X - vectorB.X, vectorA.Y - vectorB.Y);
    }

    static Multiply(vector, multiplier) {

        return new Vector2(vector.X * multiplier, vector.Y * multiplier);
    }

    static Divide(vector, divider) {

        if (divider === 0)
            throw "Division by 0";

        return Vector2.Multiply(vector, 1.0 / divider);
    }

    // Helpers
    Equals(otherVector) {

        if (!otherVector)
            return false;

        return this.X === otherVector.X && this.Y === otherVector.Y;
    }

    ToString() {

        return `<${this.X} ${this.Y}>`;
    }
    
    static get LEFT_VECTOR() { return new Vector(-1, 0); }
    static get RIGHT_VECTOR() { return new Vector(1, 0); }
    static get UP_VECTOR() { return new Vector(0, 1); }
    static get DOWN_VECTOR() { return new Vector(0, -1); }
    static get ZERO_VECTOR() { return new Vector(0, 0); }
}
