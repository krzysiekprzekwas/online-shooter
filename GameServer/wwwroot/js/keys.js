let keys = {

    // KeyCode from js to backend enum (KeyEnum.cs)
    configuration: {
        80: 99,
        87: 1, // W
        83: 2, // S
        65: 3, // A
        68: 4, // D
        32: 5 // Space
    },
    
    keysPressed: Array(),

    // Action on player key pressed
    keyDown: function (keyCode) {

        // console.log(keyCode);

        if (keyCode == 80)
            config.DEBUG_STOP = !config.DEBUG_STOP;

        let index = this.keysPressed.indexOf(keyCode);
        if (index === -1) {
            this.keysPressed.push(keyCode);
        }

        //console.log(this.getKeysState());
    },

    // Action on player key up
    keyUp: function (keyCode) {

        let index = this.keysPressed.indexOf(keyCode);
        if (index > -1) {
            this.keysPressed.splice(index, 1);
        }
    },

    // Returns a list of all keys pressed
    getKeysState: function () {

        let keysState = Array();

        for (let key of this.keysPressed) {
            
            if (!(key in this.configuration))
                continue;
            
            let keyCommand = this.configuration[key];
            
            if (keysState.indexOf(keyCommand) === -1)
                keysState.push(keyCommand);
        }

        return keysState;
    }
};