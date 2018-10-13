function KeyboardController() {

    const that = this;

    that.KeysPressed = Array();

    that.OnKeyDown = function (keyCode) {

        const index = that.KeysPressed.indexOf(keyCode);
        if (index === -1) {
            that.KeysPressed.push(keyCode);
        }
    };

    that.OnKeyUp = function (keyCode) {

        const index = that.KeysPressed.indexOf(keyCode);
        if (index > -1) {
            that.KeysPressed.splice(index, 1);
        }
    };

    // Returns a list of all keys pressed
    that.GetKeysState = function () {

        const keysState = Array();

        for (const key of that.KeysPressed) {

            if (!(key in KeyboardController.Configuration))
                continue;

            const keyCommand = KeyboardController.Configuration[key];
            if (keysState.indexOf(keyCommand) === -1)
                keysState.push(keyCommand);
        }

        return keysState;
    };
}

// KeyCode from js to backend enum (KeyEnum.cs)
KeyboardController.Configuration = {
    80: 99,
    87: 1, // W
    83: 2, // S
    65: 3, // A
    68: 4, // D
    32: 5 // Space
};

const keyboardController = new KeyboardController();