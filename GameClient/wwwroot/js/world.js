let world = {

    initialize: function() {

        this.canvas = document.getElementById('renderCanvas');
        this.engine = new BABYLON.Engine(this.canvas, true);
        this.mapObjects = Array();
        this.playerObjects = Array();
        this.fpsLabel = document.getElementById("fpsLabel");
        this.pingLabel = document.getElementById("pingLabel");
        this.playerId = -1;
        this.ping = 999;
        this.receivedGameStateOnCurrentFrame = false;
        this.lastGamestate = null;
        this.lastFrameTime = new Date();

        this.createScene();
        
        this.engine.runRenderLoop(function () {

            // If extrapolation is turned on and world gamestate is obsolote
            if (config.EXTRAPOLATION && !world.receivedGameStateOnCurrentFrame)
                world.extrapolatePlayers();

            world.receivedGameStateOnCurrentFrame = false;
            world.scene.render();
            world.fpsLabel.innerHTML = world.engine.getFps().toFixed();
            world.pingLabel.innerHTML = world.ping;

            // Remember last frame generation time so we will be able to extrapolate
            world.lastFrameTime = new Date();
        });

        // the canvas/window resize event handler
        window.addEventListener('resize', function(){
            world.engine.resize();
        });
    },

    loadMapObjects: function(mapstate) {

        for(obj of mapstate.MapObjects)
        {
            var mesh;
    
            switch(obj.Type)
            {
                case 'box':
                mesh = BABYLON.MeshBuilder.CreateBox("box", {
                        height: obj.Height,
                        width: obj.Width,
                        depth: obj.Depth
                    }, this.scene);   
    
                    break;
    
                default:
                    console.error("[ERROR] Unknown object type " + obj.Type + ".");
            }

            let mat = new BABYLON.StandardMaterial("mat", this.scene);


            try {

                switch (obj.TextureId) {
                    case 1:
                        mat.diffuseTexture = new BABYLON.Texture("textures/brick.jpg",this.scene);
                        break;
                    case 2:
                            mat.diffuseTexture  = new BABYLON.Texture("textures/wall.jpg", this.scene);
                        break;
                    case 3:
                        mat.diffuseTexture = new BABYLON.Texture("textures/ground.jpg", this.scene);
                        break;
                    default:
                        mat.emissiveColor = new BABYLON.Color3(obj.Color.Red, obj.Color.Green, obj.Color.Blue);
                    }

            }
            catch (err) {
                console.log("Trouble getting texture. Loaded backup color.")
                mat.emissiveColor = new BABYLON.Color3(obj.Color.Red, obj.Color.Green, obj.Color.Blue);
            }


            mesh.position.x += obj.Position.X;
            mesh.position.y += obj.Position.Y;
            mesh.position.z += obj.Position.Z;
            mesh.material = mat;

            this.mapObjects[obj.Id] = mesh;
        }

        logger.info("Loaded map objects " + mapstate.MapObjects.length);
    },

    // Update function is called when gamestate was received from server
    updatePlayers: function(gamestate) {

        // Change variable so players wont be extrapolated on current frame
        this.receivedGameStateOnCurrentFrame = true;
        this.lastGamestate = gamestate;

        for(player of gamestate.Players) 
        {
            // If current player is you, just update camera position
            if (player.Id === world.playerId) {

                this.camera.position.x = player.Position.X;
                this.camera.position.y = player.Position.Y;
                this.camera.position.z = player.Position.Z;
                continue;
            }

            // Else create player or update players position
            if(typeof this.playerObjects[player.Id] === 'undefined')
            {
                let playerObject = BABYLON.Mesh.CreateSphere('sphere1', 16, 0.5, this.scene);
                this.playerObjects[player.Id] = playerObject;
            }

            this.playerObjects[player.Id].position.x = player.Position.X;
            this.playerObjects[player.Id].position.y = player.Position.Y;
            this.playerObjects[player.Id].position.z = player.Position.Z;
        }

    },

    extrapolatePlayers: function () {

        if (this.lastGamestate === null)
            return;

        let timeDiff = new Date() - this.lastFrameTime;
        let speed = timeDiff / 1000 * config.PLAYER_SPEED;

        for (player of this.lastGamestate.Players)
        {
            // If current player is you, just update camera position
            if (player.Id === world.playerId) {

                this.camera.position.x += player.Speed.X * speed;
                this.camera.position.y += player.Speed.Y * speed;
                this.camera.position.z += player.Speed.Z * speed;
                continue;
            }

            this.playerObjects[player.Id].position.x += player.Speed.X * speed;
            this.playerObjects[player.Id].position.y += player.Speed.Y * speed;
            this.playerObjects[player.Id].position.z += player.Speed.Z * speed;
        }
    },

    createScene: function() {

        this.scene = new BABYLON.Scene(this.engine);

        // Create camera
        this.camera = new BABYLON.FreeCamera('camera1', new BABYLON.Vector3(0, 5, -10), this.scene);
        this.camera.setTarget(BABYLON.Vector3.Zero());
        //this.camera.rotation.x = -Math.PI / 2;
        //this.camera.rotation.y = 0;

        // Create light
        var light = new BABYLON.HemisphericLight('light1', new BABYLON.Vector3(0,1,0), this.scene);
    }
};