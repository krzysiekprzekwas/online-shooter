function MouseController() {

    const that = this;

    that.isMouseClicked = false;

    that.updateCurrentAngle = function (e) {

        const x = e.clientX;
        const y = e.clientY;

        if (typeof width === "undefined")
            return;

        let angle = that.getAngleFromMousePosition(x, y);
        angle = that.convertAngle(angle);
        that._angle = angle;
    };

    that.getAngleFromMousePosition = function (x, y) {
    
        const deltaX = x - (width / 2);
        const deltaY = y - (height / 2);
        
        return Math.atan2(deltaX, deltaY);
    };

    that.convertAngle = function (angle) {

        if (angle < 0)
            angle = Math.abs(angle);
        else
            angle = 2 * Math.PI - angle;

        return angle;
    };

    that.getCurrentAngle = function () {

        return that._angle;
    };

    that.OnMouseClick = function () {
        that.isMouseClicked = true;
    };

    that.OnMouseRelease = function () {
        that.isMouseClicked = false;
    };

    that.IsMouseClicked  = function () {
        return that.isMouseClicked ;
    };
    
    document.addEventListener("mousemove", that.updateCurrentAngle);

    document.addEventListener("mouseup", that.OnMouseRelease);

    document.addEventListener("mousedown", that.OnMouseClick);
}

let mouseController = new MouseController();