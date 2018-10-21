function TexturesService() {

    const that = this;

    that.initialize = function ()  {

        that.loadedTextures = [];
        that.loadedTexturesCount = 0;

        // Load defined textures
        that.registerTexture(0, "textures/brick.jpg"); // DEFAULT TEXTURE

        that.registerTexture(1, "textures/brick.jpg");
        that.registerTexture(2, "textures/wall.jpg");
        that.registerTexture(3, "textures/ground.jpg");

        console.log(`Loaded ${this.loadedTexturesCount} textures.`);
    };

    that.registerTexture = function(textureId, textureUrl) {

        // Textutre already loaded
        if (that.loadedTextures[textureId] !== undefined) {
            console.error(`Texture #${textureId} was already loaded.`);
            return;
        }

        // Load texture
        const loadedImage = loadImage(textureUrl);
        that.loadedTextures[textureId] = loadedImage;
        that.loadedTexturesCount += 1;
    };

    that.getTexture = function(textureId) {

        if (textureId === undefined)
            textureId = 0;

        // Textutre already loaded
        if (that.loadedTextures[textureId] === undefined) {
            console.error(`Trying to display not loaded texture #${textureId}.`);
            return null;
        }

        return that.loadedTextures[textureId];
    };
};

const texturesService = new TexturesService();