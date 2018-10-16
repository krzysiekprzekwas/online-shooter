const nameService = {

    getRandomName() {

        var name;

        jQuery.ajax({
            url: 'https://uinames.com/api/?region=england',
            async: false,
            success: function (data) {
                name = data.name;
            },
            error: function () {
                name = "Player" + Math.floor((Math.random() * 10000) + 1);
            }
        });

        return name;
    },

};