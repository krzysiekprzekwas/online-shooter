let world = {

    initialize: function() {

        this.canvas = document.getElementById('renderCanvas');
        this.engine = new BABYLON.Engine(this.canvas, true);
        this.mapObjects = Array();
        this.playerObjects = Array();
        this.fpsLabel = document.getElementById("fpsLabel");

        this.createScene();
        
        this.engine.runRenderLoop(function(){
            world.scene.render();
            world.fpsLabel.innerHTML = world.engine.getFps().toFixed();
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
                        depth: obj.Depth,
                    }, this.scene);   
    
                    break;
    
                default:
                    console.error("[ERROR] Unknown object type " + obj.Type + ".");
            }
    
            mesh.position.x += obj.X;
            mesh.position.y += obj.Y;
            mesh.position.z += obj.Z;

            this.mapObjects[obj.Id] = mesh;
        }

        logger.info("Loaded map objects " + mapstate.MapObjects.length);
    },

    updatePlayers: function(gamestate) {

        for(player of gamestate.Players) 
        {
            if(typeof this.playerObjects[player.Id] == 'undefined')
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
        var camera = new BABYLON.FreeCamera('camera1', new BABYLON.Vector3(0, 5,-10), this.scene);
        camera.setTarget(BABYLON.Vector3.Zero());
        //camera.attachControl(this.canvas, false);

        // Create light
        var light = new BABYLON.HemisphericLight('light1', new BABYLON.Vector3(0,1,0), this.scene);
    }
};