function DrawingController() {

    const that = this;

    that.myPlayer;

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

        players.except(function (x) { return that.myPlayer !== null && x.id === that.myPlayer.id; }).forEach((player, i) => {
            
            push();

            translate(player.x - that.myPlayer.x, player.y - that.myPlayer.y);
            rotate(player.angle);

            strokeWeight(0);
            rect(0, 20, 16, 30);

            strokeWeight(4);
            ellipse(0, 0, player.radius * 2, player.radius * 2);

            pop();
        });

        // Draw myPlayer

        fill(255, 190, 118);

        stroke(52, 152, 219);

        push();
        
        rotate(that.myPlayer.angle);

        strokeWeight(0);
        rect(0, 20, 16, 30);

        strokeWeight(4);
        ellipse(0, 0, that.myPlayer.radius * 2, that.myPlayer.radius * 2);

        pop();
    };
};

const drawingController = new DrawingController();