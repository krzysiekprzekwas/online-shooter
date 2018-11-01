function NotificationController() {

    const that = this;

    that.stack = {
        dir1: 'up',
        dir2: 'left',
        firstpos1: 25,
        firstpos2: 25,
        spacing1: 36,
        spacing2: 36,
        push: 'top'
    };

    PNotify.prototype.options.delay = 3000;

    that.PlayerJoinedNotification = function (name) {
        new PNotify({
            text: 'Player ' + name + ' connected',
            addclass: "stack-bottomleft",
            stack: that.stack
        });
    };

    that.PlayerLeftNotification = function (name) {
        new PNotify({
            text: 'Player ' + name + ' left',
            addclass: "stack-bottomleft",
            stack: that.stack
        });
    };

    that.PlayerKilledNotification = function(killer, victim) {
        new PNotify({
            text: victim + " killed by " + killer,
            addclass: "stack-bottomleft",
            stack: that.stack
        });
    };
}

// KeyCode from js to backend enum (KeyEnum.cs)
NotificationController.Configuration = {
    80: 99,
    87: 1, // W
    83: 2, // S
    65: 3, // A
    68: 4, // D
    32: 5 // Space
};

const notificationController = new NotificationController();