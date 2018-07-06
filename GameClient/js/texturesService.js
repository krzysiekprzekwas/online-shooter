const texturesService = {

    initialize() {

        this.loadedTextures = [];
        this.loadedTexturesCount = 0;

        // Load defined textures
        this.registerTexture(1, "textures/brick.jpg");
        this.registerTexture(2, "textures/wall.jpg");
        this.registerTexture(3, "textures/ground.jpg");

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

        // Textutre already loaded
        if (this.loadedTextures[textureId] !== undefined) {
            logger.error(`Trying to display not loaded texture #${textureId}.`);
            return null;
        }

        return this.loadedTextures[textureId];
    },

};