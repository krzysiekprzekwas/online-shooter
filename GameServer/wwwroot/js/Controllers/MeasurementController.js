function MeasurementController() {

    const that = this;

    that.UpdatePing = function (ping) {

        $('#pingLabel').text(`Ping: ${ping} ms`);
    };

    that.UpdateFps = function (fps) {

        $('#fpsLabel').text(`FPS: ${fps.toFixed(2)}`);
    };
    
    that.UpdateFrameDropRate = function (frameDropRate) {
        $('#frameDropLabel').text(`Framedrop: ${frameDropRate}%`);
    };

    that.UpdateDelay = function (delay) {
        $('#delayValueLabel').text(`Delay: ${delay} ms`);
    };
}

const measurementController = new MeasurementController();