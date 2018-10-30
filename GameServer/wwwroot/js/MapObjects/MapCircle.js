
function MapCircle(x, y, radius) {

    const that = this;

    that._position = new Vector2(x, y);
    that._radius = radius;

    that.GetX = () => that._position.GetX();
    that.SetX = (value) => that._position.SetX(value);

    that.GetY = () => that._position.GetY();
    that.SetY = (value) => that._position.SetY(value);

    that.GetPosition = () => that._position;
    that.SetPosition = (value) => that._position = value;

    that.GetRadius = () => that._radius;
    that.SetRadius = (value) => that._radius = value;

    that.RadiusSquared = () => Math.pow(that._radius, 2);

    that.Equals = (otherCircle) =>
        that.GetX() === otherCircle.GetX() &&
        that.GetY() === otherCircle.GetY() &&
        that.GetRadius() === otherCircle.GetRadius();

    that.ToString = () => `Circle<x:${that.GetX()} y:${that.GetY()}, r:${that.GetRadius()}>`;
}

MapCircle.Clone = function(mapCircle) {

    return new MapCircle(mapCircle.GetX(), mapCircle.GetY(), mapCircle.GetRadius());
};

// Export module
if (typeof module !== "undefined" && module.hasOwnProperty("exports")) {
    module.exports = MapCircle;
}
