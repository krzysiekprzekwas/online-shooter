window.onkeyup = function (e) { keyboardController.OnKeyUp(e.keyCode); };
window.onkeydown = function (e) { keyboardController.OnKeyDown(e.keyCode); };
p5.disableFriendlyErrors = true;

Array.prototype.except = function (val) {
    return this.filter(function (x) { return x !== val; });
}; 

function preload() {

    texturesService.initialize();
}

function setup() {
    createCanvas(window.innerWidth, window.innerHeight, WEBGL);
    stroke(0);

    ellipseMode(CENTER);
    rectMode(CENTER);
        
    vex.defaultOptions.className = 'vex-theme-top';

    var name = nameService.getRandomName();

    vex.dialog.prompt({
        message: 'What is Your name little soldier?',
        placeholder: name,
        callback: function (value) {

            if (value) {
                connectionController.Initialize(value);
            }
            else {
                connectionController.Initialize(name);
            }
        }
    });
        
}

function draw() {
    drawingController.SetBackground(61);

    worldController.Draw();
    
    var fps = frameRate();
    measurementController.UpdateFps(fps);
}

function windowResized() {
    resizeCanvas(windowWidth, windowHeight);
}
