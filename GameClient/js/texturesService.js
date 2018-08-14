const texturesService = {

    initialize() {

        this.loadedTextures = [];
        this.loadedTexturesCount = 0;

        // Load defined textures
        this.registerTexture(0, location.href + "textures/brick.jpg"); // DEFAULT TEXTURE

        this.registerTexture(1, location.href + "textures/brick.jpg");
        this.registerTexture(2, location.href + "textures/wall.jpg");
        this.registerTexture(3, location.href + "textures/ground.jpg");

        logger.info(`Loaded ${this.loadedTexturesCount} textures.`);
    },

    registerTexture(textureId, textureUrl) {

        // Textutre already loaded
        if (this.loadedTextures[textureId] !== undefined) {
            logger.error(`Texture #${textureId} was already loaded.`);
            return;
        }

        // Load texture
        this.loadedTextures[textureId] = loadImage(textureUrl);
        this.loadedTexturesCount += 1;
    },

    getTexture(textureId) {

        if (textureId === undefined)
            textureId = 0;

        // Textutre already loaded
        if (this.loadedTextures[textureId] === undefined) {
            logger.error(`Trying to display not loaded texture #${textureId}.`);
            return null;
        }

        return this.loadedTextures[textureId];
    },

};