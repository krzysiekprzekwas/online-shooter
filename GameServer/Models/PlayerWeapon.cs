using GameServer.Game;
using System;

namespace GameServer.Models
{
    public class PlayerWeapon
    {
        public PlayerWeapon(WeaponEnum weaponEnum)
        {
            WeaponEnum = weaponEnum;
            LastShotDate = new DateTime();
        }

        public WeaponEnum WeaponEnum { get; set; }

        public int CurrentClipAmmo { get; set; }
        public int CurrentAmmo { get; set; }

        public DateTime LastShotDate { get; set; }
        
        public Weapon GetWeapon()
        {
            return WeaponService.GetWeaponFromWeaponEnumOrNull(WeaponEnum);
        }

        public void GiveMaxAmmo()
        {
            var weapon = WeaponService.GetWeaponFromWeaponEnumOrNull(WeaponEnum);
            CurrentAmmo = weapon.MaxAmmo;
        }

        public void GiveMaxClipAmmo()
        {
            var weapon = WeaponService.GetWeaponFromWeaponEnumOrNull(WeaponEnum);
            CurrentClipAmmo = weapon.MaxClipAmmo;
        }
    }
}
