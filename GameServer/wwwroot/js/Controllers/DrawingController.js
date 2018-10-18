function DrawingController() {

    const that = this;

    that.myPlayer = {
        id: -1,
        radius: 16
    };

    that.SetBackground = function (value) {
        background(value);
    };

    that.SetMyPlayer = function (player) {
        that.myPlayer = player;
    };
    
    that.drawMapObjects = function (mapObjects) {

        noFill();
        mapObjects.forEach((obj, i) => {

            strokeWeight(2);
            stroke(243, 156, 18);
            fill(255, 255, 255);

            texture(texturesService.getTexture(obj.textureId));
            rect(obj.x - that.myPlayer.x, obj.y - that.myPlayer.y, obj.width, obj.height);
        });
    };

    that.drawPlayers = function (players) {

        // Draw enemies

        fill(255, 190, 118);

        stroke(0);

        players.filter(x => x.id !== that.myPlayer.id).forEach((player, i) => {
            
            that.DrawPlayer(player.x, player.y, player.angle, player.radius);
        });

        // Draw myPlayer

        stroke(52, 152, 219);

        that.DrawPlayer(that.myPlayer.x, that.myPlayer.y, mouseController.getCurrentAngle(), that.myPlayer.radius);
    };

    that.DrawPlayer = function (x, y, angle, radius) {
        push();

        translate(x - that.myPlayer.x, y - that.myPlayer.y);

        rotate(angle);

        strokeWeight(0);
        rect(0, 20, 16, 30);

        strokeWeight(4);
        ellipse(0, 0, radius * 2, radius * 2);

        pop();
    };
};

const drawingController = new DrawingController();