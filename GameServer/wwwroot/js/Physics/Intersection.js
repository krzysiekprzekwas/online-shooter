
if (typeof require !== "undefined") {
    Vector2 = require('./Vector2.js');
    MapCircle = require('../MapObjects/MapCircle.js');
    MapRect = require('../MapObjects/MapRect.js');
}


function Intersection() {

    const that = this;

}

Intersection.CheckIntersection = function(mapObjectA, mapObjectB) {

    if (mapObjectA.constructor.name === "MapCircle" && mapObjectB.constructor.name === "MapCircle")
        return Intersection.CheckCircleCircleIntersection(mapObjectA, mapObjectB);
    else if (mapObjectA.constructor.name === "MapRect" && mapObjectB.constructor.name === "MapCircle")
        return Intersection.CheckRectCircleIntersection(mapObjectA, mapObjectB);
    else if (mapObjectA.constructor.name === "MapCircle" && mapObjectB.constructor.name === "MapRect")
        return Intersection.CheckRectCircleIntersection(mapObjectB, mapObjectA);
    else if (mapObjectA.constructor.name === "MapRect" && mapObjectB.constructor.name === "MapRect")
        return Intersection.CheckRectRectIntersection(mapObjectA, mapObjectB);
    
    throw `Cannot check intersection for ${mapObjectA.constructor.name} and ${mapObjectB.constructor.name}`;
};

Intersection.CheckCircleCircleIntersection = function(mapCircleA, mapCircleB) {
    
    const distanceSquared = Vector2.DistanceSquared(mapCircleA.GetPosition(), mapCircleB.GetPosition());
    return distanceSquared <= Math.pow(mapCircleA.GetRadius() + mapCircleB.GetRadius(), 2);
};

Intersection.CheckRectCircleIntersection = function(mapRect, mapCircle) {

    const x = Math.abs(mapCircle.GetX() - mapRect.GetX());
    const y = Math.abs(mapCircle.GetY() - mapRect.GetY());
    
    if (x > mapRect.GetWidth() / 2 + mapCircle.GetRadius() ||
        y > mapRect.GetHeight() / 2 + mapCircle.GetRadius())
    {
        return false;
    }
    
    if (x <= mapRect.GetWidth() / 2 || y <= mapRect.GetHeight() / 2)
    {
        return true;
    }
    
    const cornerDistance_sq = Math.pow(x - mapRect.GetWidth() / 2, 2) + Math.pow(y - mapRect.GetHeight() / 2, 2);
    return cornerDistance_sq <= mapCircle.RadiusSquared();
};

Intersection.CheckRectRectIntersection = function(mapRectA, mapRectB) {

    const r1x1 = mapRectA.GetX() - (mapRectA.GetWidth() / 2);
    const r1x2 = mapRectA.GetX() + (mapRectA.GetWidth() / 2);
    const r1y1 = mapRectA.GetY() - (mapRectA.GetHeight() / 2);
    const r1y2 = mapRectA.GetY() + (mapRectA.GetHeight() / 2);

    const r2x1 = mapRectB.GetX() - (mapRectB.GetWidth() / 2);
    const r2x2 = mapRectB.GetX() + (mapRectB.GetWidth() / 2);
    const r2y1 = mapRectB.GetY() - (mapRectB.GetHeight() / 2);
    const r2y2 = mapRectB.GetY() + (mapRectB.GetHeight() / 2);

    return (r2x2 >= r1x1 && r2x1 <= r1x2) && (r2y2 >= r1y1 && r2y1 <= r1y2);
};

// Export module
if (typeof module !== "undefined" && module.hasOwnProperty("exports")) {
    module.exports = Intersection;
}