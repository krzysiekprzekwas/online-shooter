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

        this.createScene();
        
        this.engine.runRenderLoop(function(){
            world.scene.render();
            world.fpsLabel.innerHTML = world.engine.getFps().toFixed();
            world.pingLabel.innerHTML = world.ping;
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
            mat.emissiveColor = new BABYLON.Color3(obj.Color.Red, obj.Color.Green, obj.Color.Blue);
            
            mesh.position.x += obj.X;
            mesh.position.y += obj.Y;
            mesh.position.z += obj.Z;
            mesh.material = mat;

            this.mapObjects[obj.Id] = mesh;
        }

        logger.info("Loaded map objects " + mapstate.MapObjects.length);
    },

    updatePlayers: function(gamestate) {

        for(player of gamestate.Players) 
        {
            if (player.Id === world.playerId) {

                this.camera.position.x = player.X;
                this.camera.position.y = player.Y;
                this.camera.position.z = player.Z;
                continue;
            }

            if(typeof this.playerObjects[player.Id] === 'undefined')
            {
                let playerObject = BABYLON.Mesh.CreateSphere('sphere1', 16, 2, this.scene);
                this.playerObjects[player.Id] = playerObject;
            }

            this.playerObjects[player.Id].position.x = player.X;
            this.playerObjects[player.Id].position.y = player.Y;
            this.playerObjects[player.Id].position.z = player.Z;
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