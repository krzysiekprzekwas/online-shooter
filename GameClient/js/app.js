window.addEventListener('DOMContentLoaded', function(){

    //mouse.initialize();
    world.initialize();
});

window.onkeyup = function (e) { keys.keyUp(e.keyCode); };
window.onkeydown = function (e) { keys.keyDown(e.keyCode); };

function setup() {
    createCanvas(displayWidth, displayHeight);
    strokeWeight(10)
    stroke(0);
}

function draw() {
    world.draw();
}

function windowResized() {
    resizeCanvas(windowWidth, windowHeight);
}