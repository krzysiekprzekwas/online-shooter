window.addEventListener('DOMContentLoaded', function(){

    world.initialize();
});

var keys = {};
window.onkeyup = function(e) { delete keys[e.keyCode]; }
window.onkeydown = function(e) { keys[e.keyCode] = true; }