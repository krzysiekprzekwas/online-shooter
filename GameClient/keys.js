let keys = {

    configuration: {

        87: "w",
        83: "s",
        68: "d",
        65: "a"
    },

    keysPressed: Array(),

    // Action on player key pressed
    keyDown: function (keyCode) {

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

        //console.log(this.getKeysState());
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