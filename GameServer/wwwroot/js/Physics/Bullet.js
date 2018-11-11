function Bullet(playerId) {

    const that = this;

    that._playerId = playerId;
    that.GetPlayerId = () => that._playerId;

    that._position = new Vector2();
    that.GetPosition = () => that._position;
    that.SetPosition = (value) => that._position = value;

    that.GetX = () => that.GetPosition().GetX();
    that.GetY = () => that.GetPosition().GetY();

    that.SetX = (value) => that._position.SetX(value);
    that.SetY = (value) => that._position.SetY(value);

    that._speed = new Vector2();
    that.GetSpeed = () => that._speed;
    that.SetSpeed = (value) => that._speed = value;

    that._radius = 6;
    that.GetRadius = () => that._radius;
    that.SetRadius = (value) => that._radius = value;

    that._angle = 0;
    that.GetAngle = () => that._angle;
    that.SetAngle = (value) => that._angle = value;
}

// Export module
if (typeof module !== 'undefined' && module.hasOwnProperty('exports')) {
    module.exports = Bullet;
}