const blackScreen = {

    Set(title, description, skipTime) {

        this.title = title || "Title";
        this.description = description || "Description";
    },

    Remove() {

        this.title = null;
        this.description = null;
    },

    ShouldBeDisplayed() {

        return this.title != null;
    },

    Display() {

        const width = 0.6 * window.innerWidth;
        const height = 0.4 * window.innerHeight;
        const titleHeight = 80;

        noStroke();
        fill(120, 120, 120);
        rect(0, 0, width, height);

        fill(150, 150, 150);
        rect(0, (-height / 2) + (titleHeight / 2), width, titleHeight);
        
        let titleTexture = createGraphics(width, height);
        titleTexture.fill(231, 76, 60);
        titleTexture.textAlign(CENTER);
        titleTexture.textSize(30);
        titleTexture.text(this.title, width / 2, (titleHeight / 2) + 10);
        texture(titleTexture);
        rect(0, 0, width, height);

        let descriptionTexture = createGraphics(width, height);
        titleTexture.fill(192, 57, 43);
        titleTexture.textAlign(LEFT);
        titleTexture.textSize(16);
        titleTexture.text(this.description, 15, (height / 2) - 50);
        texture(titleTexture);
        rect(0, 0, width, height);
    },
};