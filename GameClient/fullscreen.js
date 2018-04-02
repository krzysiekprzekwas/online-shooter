let fullscreen = {

    toggled: false,

    requestFullscreen: function () {

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

        this.toggled = true;
    },

    exitFullscreen: function () {

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

        this.toggled = false;
    },

    toggleFullscreen: function () {
        
        if (fullscreen.toggled)
            fullscreen.exitFullscreen();
        else
            fullscreen.requestFullscreen();
    }
};

document.getElementById('fullscreenToggle').addEventListener('click', fullscreen.toggleFullscreen);