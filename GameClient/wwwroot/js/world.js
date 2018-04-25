let world = {

    initialize: function () {

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
        window.addEventListener('resize', function () {
            world.engine.resize();
        });
    },

    loadMapObjects: function (mapstate) {

        for (obj of mapstate.MapObjects) {
            var mesh;

            // Create mesh in 3d world
            switch (obj.Type) {
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

            let material = null;
            let textureUrl = '';
            let textureSize = 50;

            // Load texture url based on textureId sent by server
            switch (obj.TextureId) {
                case 1:
                    textureUrl = "http://145.239.86.84/textures/brick.jpg";
                    break;
                case 2:
                    textureUrl = "http://145.239.86.84/textures/wall.jpg";
                    break;
                case 3:
                    textureUrl = "http://145.239.86.84/textures/ground.jpg";
                    break;
            }

            // Texture not defined
            if (textureUrl == '') {

                material = new BABYLON.StandardMaterial("mat", this.scene);
                material.emissiveColor = new BABYLON.Color3(obj.Color.Red, obj.Color.Green, obj.Color.Blue);
            }
            // Load texture
            else {

                material = new BABYLON.MultiMaterial("multi", this.scene);

                // Define a materials
                let frontMaterial = new BABYLON.StandardMaterial("material0", this.scene);
                frontMaterial.diffuseTexture = new BABYLON.Texture(textureUrl, this.scene);

                let backMaterial = frontMaterial.clone();
                let leftMaterial = frontMaterial.clone();
                let rightMaterial = frontMaterial.clone();
                let topMaterial = frontMaterial.clone();
                let bottomMaterial = frontMaterial.clone();

                // Rotate materials on side walls
                frontMaterial.diffuseTexture.wAng = Math.PI;
                leftMaterial.diffuseTexture.wAng = Math.PI / 2;
                rightMaterial.diffuseTexture.wAng = Math.PI / 2;
                topMaterial.diffuseTexture.wAng = Math.PI / 2;

                // Scale materials
                topMaterial.diffuseTexture.uScale = obj.Width / textureSize;
                topMaterial.diffuseTexture.vScale = obj.Depth / textureSize;
                bottomMaterial.diffuseTexture.uScale = obj.Width / textureSize;
                bottomMaterial.diffuseTexture.vScale = obj.Depth / textureSize;

                leftMaterial.diffuseTexture.uScale = obj.Height / textureSize;
                leftMaterial.diffuseTexture.vScale = obj.Depth / textureSize;
                rightMaterial.diffuseTexture.uScale = obj.Height / textureSize;
                rightMaterial.diffuseTexture.vScale = obj.Depth / textureSize;

                frontMaterial.diffuseTexture.uScale = obj.Width / textureSize;
                frontMaterial.diffuseTexture.vScale = obj.Height / textureSize;
                backMaterial.diffuseTexture.uScale = obj.Width / textureSize;
                backMaterial.diffuseTexture.vScale = obj.Height / textureSize;
                
                // Add all materials to MultiMaterial object
                material.subMaterials.push(frontMaterial);
                material.subMaterials.push(backMaterial);
                material.subMaterials.push(leftMaterial);
                material.subMaterials.push(rightMaterial);
                material.subMaterials.push(topMaterial);
                material.subMaterials.push(bottomMaterial);

                // Apply materials
                mesh.subMeshes = [];
                var verticesCount = mesh.getTotalVertices();
                mesh.subMeshes.push(new BABYLON.SubMesh(0, 0, verticesCount, 0, 6, mesh));
                mesh.subMeshes.push(new BABYLON.SubMesh(1, 1, verticesCount, 6, 6, mesh));
                mesh.subMeshes.push(new BABYLON.SubMesh(2, 2, verticesCount, 12, 6, mesh));
                mesh.subMeshes.push(new BABYLON.SubMesh(3, 3, verticesCount, 18, 6, mesh));
                mesh.subMeshes.push(new BABYLON.SubMesh(4, 4, verticesCount, 24, 6, mesh));
                mesh.subMeshes.push(new BABYLON.SubMesh(5, 5, verticesCount, 30, 6, mesh));
            }

            // Set mesh material
            mesh.material = material;

            // Set mesh world position
            mesh.position.x += obj.Position.X;
            mesh.position.y += obj.Position.Y;
            mesh.position.z += obj.Position.Z;

            // Add mesh to objects array
            this.mapObjects[obj.Id] = mesh;
        }

        logger.info("Loaded map objects " + mapstate.MapObjects.length);
    },

    // Update function is called when gamestate was received from server
    updatePlayers: function (gamestate) {

        // Change variable so players wont be extrapolated on current frame
        this.receivedGameStateOnCurrentFrame = true;
        this.lastGamestate = gamestate;

        for (player of gamestate.Players) {
            // If current player is you, just update camera position
            if (player.Id === world.playerId) {

                this.camera.position.x = player.Position.X;
                this.camera.position.y = player.Position.Y;
                this.camera.position.z = player.Position.Z;
                continue;
            }

            // Else create player or update players position
            if (typeof this.playerObjects[player.Id] === 'undefined') {
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

        for (player of this.lastGamestate.Players) {
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

    createScene: function () {

        this.scene = new BABYLON.Scene(this.engine);

        // Create camera
        this.camera = new BABYLON.FreeCamera('camera1', new BABYLON.Vector3(0, 5, -10), this.scene);
        this.camera.setTarget(BABYLON.Vector3.Zero());
        //this.camera.rotation.x = -Math.PI / 2;
        //this.camera.rotation.y = 0;

        // Create light
        var light = new BABYLON.HemisphericLight('light1', new BABYLON.Vector3(0, 1, 0), this.scene);
        light.diffuse = new BABYLON.Color3(0.8, 0.8, 0.8);
        light.specular = new BABYLON.Color3(0.1, 0.1, 0.1);
        light.groundColor = new BABYLON.Color3(0, 0, 0);
    }
};