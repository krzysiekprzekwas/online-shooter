function MeasurementController() {

    const that = this;

    that.UpdatePing = function (ping) {

        $('#pingLabel').text(ping + " - Ping");
    };

    that.UpdateFps = function (fps) {

        $('#fpsLabel').text(fps.toFixed(2) + " - FPS");
    };
}

const measurementController = new MeasurementController();