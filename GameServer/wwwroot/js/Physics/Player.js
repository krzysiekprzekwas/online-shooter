function Player(id = 0) {

    const that = this;

    that._id = id;
    that.GetId = () => this._id;

    that._name = "Unnamed player";
    that.GetName = () => that._name;
    that.SetName = (value) => that._name = value;

    that._angle = 0;
    that.GetAngle = () => that._angle;
    that.SetAngle = (value) => that._angle = value;

    that._position = new Vector2();
    that.GetPosition = () => that._position;
    that.SetPosition = (value) => that._position = value;

    that.GetX = () => that.GetPosition().GetX();
    that.GetY = () => that.GetPosition().GetY();

    that._speed = new Vector2();
    that.GetSpeed = () => that._speed;
    that.SetSpeed = (value) => that._speed = value;

    that._isMouseClicked = false;
    that.IsMouseClicked = () => that._isMouseClicked;
    that.SetIsMouseClicked = (value) => that._isMouseClicked = value;

    that._radius = 16;
    that.GetRadius = () => that._radius;
    that.SetRadius = (value) => that._radius = value;

    that.ToString = () => `Player<Id:${that.GetId()} Name:${that.GetName()}, Pos:${that.GetPosition().ToString()} R:${that.GetRadius()}>`;
}
