function MeasurementController() {

    const that = this;

    that.UpdatePing = function (ping) {

        $('#pingLabel').text(ping + " - Ping");
    };

    that.UpdateFps = function (fps) {

        $('#fpsLabel').text(fps.toFixed(2) + " - FPS");
    };
    
    that.UpdateFrameDropRate = function (frameDropRate) {
        $('#frameDropLabel').text(frameDropRate + "% - Framedrop");
    };

    that.UpdateDelay = function (delay) {
        $('#delayValueLabel').text(delay + " ms - Delay");
    };
}

const measurementController = new MeasurementController();