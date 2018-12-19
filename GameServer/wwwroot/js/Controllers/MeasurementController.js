function MeasurementController() {

    const that = this;
    
    that.ToggleExtrapolation = function () {

        if ($('#extrapolationRange')[0].value == 0) {
            config.extrapolation = false;
            $('#extrapolationValueLabel').text("Extrapolation - OFF");

        } else {
            config.extrapolation = true;
            $('#extrapolationValueLabel').text("Extrapolation - ON");

        }
    };

    $('#extrapolationRange').bind("input", that.ToggleExtrapolation);

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