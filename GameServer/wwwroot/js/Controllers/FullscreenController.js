class FullscreenController {

    static set IsToggled(val) {
        FullscreenController._isToggled = val;
    }
    static get IsToggled() {
        return FullscreenController._isToggled || false;
    }

    constructor(element) {

        element.addEventListener('click', this.ToggleFullscreen);
        this.Element = element;
    }
    
    RequestFullscreen() {

        this.UpdateImageTo("style/exitFullscreen.png");
        
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

        FullscreenController.IsToggled = true;
    }

    ExitFullscreen() {

        this.UpdateImageTo("style/enterFullscreen.png");

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

        FullscreenController.IsToggled = false;
    }

    UpdateImageTo(source) {

        this.Element.querySelector("img").get().src = source;
    }

    ToggleFullscreen() {

        if (FullscreenController.IsToggled)
            this.ExitFullscreen();
        else
            this.RequestFullscreen();
    }
}

const fullscreenButtonController = new FullscreenController(document.getElementById("fullscreenToggle"));