using GameServer.Models;
using System.Collections.Generic;
using System.Linq;

namespace GameServer.Game
{
    public static class WeaponService
    {
        public static Dictionary<int, Weapon> Weapons = new Dictionary<int, Weapon>
        {
            { (int)WeaponEnum.SingleShotTheGreatBarrel, new Weapon(
                name: "Single shot the great barrel",
                maxClipAmmo: 5,
                maxAmmo: 100,
                reloadTime: 5000,
                shootTime: 500,
                bulletSpeed: 50,
                bulletDamage: 20,
                bulletSize: 8,
                barrelWidth: 16,
                barrelHeight: 40
                ) },
        };

        public static WeaponEnum GetDefaultWeaponEnum()
        {
            return (WeaponEnum)Weapons.Keys.First();
        }

        public static Weapon GetWeaponFromWeaponEnumOrNull(WeaponEnum weaponEnum)
        {
            return Weapons.GetValueOrDefault((int)weaponEnum);
        }

    }
}
