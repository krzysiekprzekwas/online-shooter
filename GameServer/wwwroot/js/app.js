window.onkeyup = function (e) { keyboardController.OnKeyUp(e.keyCode); };
window.onkeydown = function (e) { keyboardController.OnKeyDown(e.keyCode); };
p5.disableFriendlyErrors = true;

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

    $.get(
        "https://uinames.com/api/",
        null,
        function (data) {

            vex.dialog.prompt({
                message: 'What is Your name little soldier?',
                placeholder: data.name,
                callback: function (value) {

                    if (value) {
                        // Connection last - we may receive response faster than other class initalization
                        connector.initialize(value);
                    }
                    else {
                        connector.initialize(data.name);
                    }
                }
            });

        }
    );
}

function draw() {
    background(61);

    world.draw();

    // Draw FPS (rounded to 2 decimal places) at the bottom left of the screen
    var fps = frameRate();
    fill(255);
    stroke(0);

    $('#fpsLabel').text("FPS: " + fps.toFixed(2));
}

function windowResized() {
    resizeCanvas(windowWidth, windowHeight);
}