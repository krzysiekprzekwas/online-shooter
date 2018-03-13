let mouse = {

    MOUSE_MOVE_FACTOR: 3000,

    initialize: function () {

        this.canvas = document.getElementById('renderCanvas');
        this.x = 0;
        this.y = 0;
        
        // Option to leave lock
        document.exitPointerLock = document.exitPointerLock ||
            document.mozExitPointerLock;

        // On click canvas lock pointer
        this.canvas.requestPointerLock = this.canvas.requestPointerLock ||
            this.canvas.mozRequestPointerLock;
        this.canvas.onclick = function () {
            this.requestPointerLock();
        };

        // On pointer lock status changed
        document.addEventListener('pointerlockchange', mouse.lockChangeAlert, false);
        document.addEventListener('mozpointerlockchange', mouse.lockChangeAlert, false);
    },

    lockChangeAlert: function () {
        if (document.pointerLockElement === mouse.canvas ||
            document.mozPointerLockElement === mouse.canvas) {

            document.addEventListener("mousemove", mouse.updatePosition, false);
        } else {

            document.removeEventListener("mousemove", mouse.updatePosition, false);
        }
    },

    updatePosition: function (e) {

        mouse.x += e.movementX;
        mouse.y += e.movementY;

        world.camera.rotation.y = mouse.x / mouse.MOUSE_MOVE_FACTOR * settings.sensitivity;
        world.camera.rotation.x = mouse.y / mouse.MOUSE_MOVE_FACTOR * settings.sensitivity;
    },




};

