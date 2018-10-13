function FullscreenController() {

    const that = this;

    that.requestFullscreen = function () {

        if (document.documentElement.requestFullscreen) {
            document.documentElement.requestFullscreen();
        }
        else if (document.documentElement.mozRequestFullScreen) {
            document.documentElement.mozRequestFullScreen();
        }
        else if (document.documentElement.webkitRequestFullScreen) {
            document.documentElement.webkitRequestFullScreen();
        }
        else if (document.documentElement.msRequestFullscreen) {
            document.documentElement.msRequestFullscreen();
        }

        FullscreenController.SetIsToggled(true);
        that.updateToggleFullscreenToggleImage();
    };

    that.exitFullscreen = function () {

        if (document.exitFullscreen) {
            document.exitFullscreen();
        }
        else if (document.webkitExitFullscreen) {
            document.webkitExitFullscreen();
        }
        else if (document.mozCancelFullScreen) {
            document.mozCancelFullScreen();
        }
        else if (document.msExitFullscreen) {
            document.msExitFullscreen();
        }

        FullscreenController.SetIsToggled(false);
        that.updateToggleFullscreenToggleImage();
    };

    that.updateToggleFullscreenToggleImage = function () {

        let image = "style/enterFullscreen.png";
        if (FullscreenController.IsToggled())
            image = "style/exitFullscreen.png";

        that.Element.querySelector("img").src = image;
    };

    that.toggleFullscreen = function () {

        if (FullscreenController.IsToggled())
            that.exitFullscreen();
        else
            that.requestFullscreen();
    };

    return {

        AttachToElement: function (element) {

            element.addEventListener('click', that.toggleFullscreen);
            that.Element = element;
            that.updateToggleFullscreenToggleImage();
        }
    };
}

// Prototypes
FullscreenController.SetIsToggled = function (val) {

    FullscreenController._isToggled = val;
};
FullscreenController.IsToggled = function () {

    return FullscreenController._isToggled || false;
};

// Attach controller to button
let fullscrenController = new FullscreenController();
fullscrenController.AttachToElement(document.getElementById("fullscreenToggle"));