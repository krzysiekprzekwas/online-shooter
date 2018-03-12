let mouse = {

    initialize: function () {

        this.canvas = document.getElementById('renderCanvas');
        
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
        //document.addEventListener('pointerlockchange', this.lockChangeAlert, false);
        //document.addEventListener('mozpointerlockchange', this.lockChangeAlert, false);
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

        console.log(e.movementX + " " + e.movementY);
    }

};

