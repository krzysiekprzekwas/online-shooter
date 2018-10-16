const texturesService = {

    initialize() {

        this.loadedTextures = [];
        this.loadedTexturesCount = 0;

        // Load defined textures
        this.registerTexture(0, "textures/brick.jpg"); // DEFAULT TEXTURE

        this.registerTexture(1, "textures/brick.jpg");
        this.registerTexture(2, "textures/wall.jpg");
        this.registerTexture(3, "textures/ground.jpg");

        console.log(`Loaded ${this.loadedTexturesCount} textures.`);
    },

    registerTexture(textureId, textureUrl) {

        // Textutre already loaded
        if (this.loadedTextures[textureId] !== undefined) {
            console.error(`Texture #${textureId} was already loaded.`);
            return;
        }

        // Load texture
        const loadedImage = loadImage(textureUrl);
        this.loadedTextures[textureId] = loadedImage;
        this.loadedTexturesCount += 1;
    },

    getTexture(textureId) {

        if (textureId === undefined)
            textureId = 0;

        // Textutre already loaded
        if (this.loadedTextures[textureId] === undefined) {
            console.error(`Trying to display not loaded texture #${textureId}.`);
            return null;
        }

        return this.loadedTextures[textureId];
    },

};