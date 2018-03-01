window.addEventListener('DOMContentLoaded', function(){

    world.initialize();
});

window.onkeyup = function (e) { keys.keyUp(e.keyCode) }
window.onkeydown = function (e) { keys.keyDown(e.keyCode) }