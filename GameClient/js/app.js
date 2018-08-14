window.onkeyup = function (e) { keys.keyUp(e.keyCode); };
window.onkeydown = function (e) { keys.keyDown(e.keyCode); };

function setup() {
    createCanvas(displayWidth, displayHeight, WEBGL);
    stroke(0);

    ellipseMode(CENTER);
    rectMode(CENTER);

    mouse.initialize();
    world.initialize();
    texturesService.initialize();

    // Connection last - we may receive response faster than other class initalization
    connection.initialize();
}

function draw() {
    background(61);
    world.draw();
}

function windowResized() {
    resizeCanvas(windowWidth, windowHeight);
}