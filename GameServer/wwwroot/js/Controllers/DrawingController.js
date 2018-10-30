function DrawingController() {

    const that = this;

    that.SetBackground = function (value) {
        background(value);
    };

    that.SetMyPlayer = function (player) {
        that.myPlayer = player;
    };

    that.Draw = function (mapObjects, players, bullets) {

        if (typeof that.myPlayer === "undefined")
            return;

        that.DrawMapObjects(mapObjects);
        that.DrawBullets(bullets);
        that.DrawPlayers(players);
    };
    
    that.DrawMapObjects = function (mapObjects) {

        noFill();
        mapObjects.forEach(obj => {

            strokeWeight(2);
            stroke(243, 156, 18);
            fill(255, 255, 255);

            texture(texturesService.getTexture(obj.textureId));
            rect(obj.GetX() - that.myPlayer.GetX(), obj.GetY() - that.myPlayer.GetY(), obj.GetWidth(), obj.GetHeight());
        });
    };

    that.DrawBullets = function (bullets) {

        fill(204, 102, 0);
        bullets.forEach(bullet => {
            that.DrawBullet(bullet.GetX(), bullet.GetY(), bullet.GetRadius());
        });
    };

    that.DrawPlayers = function (players) {

        // Draw enemies
        fill(255, 190, 118);
        stroke(0);

        players.filter(x => x.GetId() !== that.myPlayer.GetId()).forEach(player => {
            
            that.DrawPlayer(player.GetX(), player.GetY(), player.GetAngle(), player.GetRadius());
        });

        // Draw myPlayer

        stroke(52, 152, 219);

        that.DrawPlayer(that.myPlayer.GetX(), that.myPlayer.GetY(), mouseController.getCurrentAngle(), that.myPlayer.GetRadius());
    };

    that.DrawPlayer = function (x, y, angle, radius) {
        push();

        translate(x - that.myPlayer.GetX(), y - that.myPlayer.GetY());

        rotate(angle);

        strokeWeight(0);
        rect(0, 20, 16, 30);

        strokeWeight(4);
        ellipse(0, 0, radius * 2, radius * 2);

        pop();
    };

    that.DrawBullet = function (x, y, radius) {

        push();

        translate(x - that.myPlayer.GetX(), y - that.myPlayer.GetY());
        
        strokeWeight(4);
        ellipse(0, 0, radius * 2, radius * 2);

        pop();
    };
};

const drawingController = new DrawingController();