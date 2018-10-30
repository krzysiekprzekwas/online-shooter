
function MapRect(x, y, width, height) {

    const that = this;

    that._position = new Vector2(x, y);
    that._width = width;
    that._height = height;

    that.GetX = () => that._position.GetX();
    that.SetX = (value) => that._position.SetX(value);

    that.GetY = () => that._position.GetY();
    that.SetY = (value) => that._position.SetY(value);

    that.GetPosition = () => that._position;
    that.SetPosition = (value) => that._position = value;

    that.GetWidth = () => that._width;
    that.SetWidth = (value) => that._width = value;

    that.GetHeight = () => that._height;
    that.SetHeight = (value) => that._height = value;

    that.Equals = (otherRect) =>
        that.GetX() === otherRect.GetX() &&
        that.GetY() === otherRect.GetY() &&
        that.GetWidth() === otherRect.GetWidth() &&
        that.GetHeight() === otherRect.GetHeight();

    that.ToString = () => `Rect<x:${that.GetX()} y:${that.GetY()}, w:${that.GetWidth()}, h:${that.GetHeight()}>`;
}

MapRect.Clone = function(mapRect) {

    return new MapRect(mapRect.GetX(), mapRect.GetY(), mapRect.GetWidth(), mapRect.GetHeight());
};

// Export module
if (typeof module !== "undefined" && module.hasOwnProperty("exports")) {
    module.exports = MapRect;
}