window.onkeyup = function (e) { keys.keyUp(e.keyCode); };
window.onkeydown = function (e) { keys.keyDown(e.keyCode); };

function preload() {

    texturesService.initialize();
}

function setup() {
    createCanvas(window.innerWidth, window.innerHeight, WEBGL);
    stroke(0);

    ellipseMode(CENTER);
    rectMode(CENTER);

    mouse.initialize();
    world.initialize();
    
    vex.defaultOptions.className = 'vex-theme-top';

    vex.dialog.prompt({
        message: 'What is Your name little soldier?',
        placeholder: 'Player',
        callback: function (value) {

            if (value !== false) {
                // Connection last - we may receive response faster than other class initalization
                connector.initialize(value);
            }
            else {

                console.log("No scenario for missing name");
            }
        }
    });

    
}

function draw() {
    background(61);

    if (blackScreen.ShouldBeDisplayed())
        blackScreen.Display();
    else
        world.draw();
}

function windowResized() {
    resizeCanvas(windowWidth, windowHeight);
}