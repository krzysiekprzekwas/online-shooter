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
        
        // world.camera.rotation.y is horizontal rotation
        world.camera.rotation.y += e.movementX / mouse.MOUSE_MOVE_FACTOR * settings.sensitivity;
        // world.camera.rotation.x is vertical rotation
        world.camera.rotation.x += e.movementY / mouse.MOUSE_MOVE_FACTOR * settings.sensitivity;
        

        // Normalize x rotation from 0 to 2 * PI
        //let d = Math.floor(world.camera.rotation.x / (2 * Math.PI));
        //world.camera.rotation.x -= d * Math.PI;

        // Normalize x rotation from 0 to PI
        let d = Math.floor(world.camera.rotation.y / Math.PI);
        world.camera.rotation.y -= d * Math.PI;

    },




};

