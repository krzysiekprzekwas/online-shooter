function WeaponService() {

    const that = this;

    that.loadedWeapons;

    that.onWeaponsReceived = function (weapons) {

        that.loadedWeapons = weapons;
    };

    that.GetWeaponFromEnum = function (weaponEnum) {

        return that.loadedWeapons[weaponEnum];
    };
};

const weaponService = new WeaponService();