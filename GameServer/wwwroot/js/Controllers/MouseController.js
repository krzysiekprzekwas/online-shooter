function MouseController() {

    const that = this;

    that.updateCurrentAngle = function (e) {

        const x = e.clientX;
        const y = e.clientY;

        const canvas = document.querySelector('canvas');
        if (canvas === null)
            return;

        const centerX = canvas.width / 2;
        const centerY = canvas.height / 2;

        that._angle = that.convertPointToAngle(x, y, centerX, centerY);
    };

    that.convertPointToAngle = function (x, y, centerX, centerY) {

        const deltaX = x - centerX;
        const deltaY = y - centerY;

        let rad = Math.atan2(deltaX, deltaY);
        if (rad < 0)
            rad = Math.abs(rad);
        else
            rad = 2 * Math.PI - rad;

        return rad / Math.PI * 180;
    };

    that.getCurrentAngle = function () {

        return that._angle;
    };

    document.addEventListener("mousemove", that.updateCurrentAngle);
}

let mouseController = new MouseController();
