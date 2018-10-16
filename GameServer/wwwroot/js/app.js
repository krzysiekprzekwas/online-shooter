window.onkeyup = function (e) { keyboardController.OnKeyUp(e.keyCode); };
window.onkeydown = function (e) { keyboardController.OnKeyDown(e.keyCode); };

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

    var name = nameService.getRandomName();

    vex.dialog.prompt({
        message: 'What is Your name little soldier?',
        placeholder: name,
        callback: function (value) {

            if (value) {
                connector.initialize(value);
            }
            else {
                connector.initialize(name);
            }
        }
    });
        
}

function draw() {
    background(61);

    world.draw();
}

function windowResized() {
    resizeCanvas(windowWidth, windowHeight);
}