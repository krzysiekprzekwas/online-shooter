window.onkeyup = function (e) { keyboardController.OnKeyUp(e.keyCode); };
window.onkeydown = function (e) { keyboardController.OnKeyDown(e.keyCode); };

var myStack = {
    dir1: 'up',
    dir2: 'left',
    firstpos1: 25,
    firstpos2: 25,
    spacing1: 36,
    spacing2: 36,
    push: 'top'
};

function preload() {

    texturesService.initialize();
}

function setup() {
    createCanvas(window.innerWidth, window.innerHeight, WEBGL);
    stroke(0);

    ellipseMode(CENTER);
    rectMode(CENTER);

    PNotify.prototype.options.delay = 3000;

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

    if (blackScreen.ShouldBeDisplayed())
        blackScreen.Display();
    else
        world.draw();
}

function windowResized() {
    resizeCanvas(windowWidth, windowHeight);
}