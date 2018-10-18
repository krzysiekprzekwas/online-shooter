namespace GameServer.Models
{
    public class Weapon
    {
        public Weapon(
            string name,
            int maxClipAmmo,
            int maxAmmo,
            int reloadTime,
            int shootTime,
            double bulletSpeed,
            int bulletDamage,
            int bulletSize,
            int barrelWidth,
            int barrelHeight)
        {
            Name = name;
            MaxClipAmmo = maxClipAmmo;
            MaxAmmo = maxAmmo;
            ReloadTime = reloadTime;
            ShootTime = shootTime;
            BulletSpeed = bulletSpeed;
            BulletDamage = bulletDamage;
            BulletSize = bulletSize;
            BarrelWidth = barrelWidth;
            BarrelHeight = barrelHeight;
        }

        public string Name { get; set; }

        public int MaxClipAmmo { get; set; }
        public int MaxAmmo { get; set; }

        public int ReloadTime { get; set; }
        public int ShootTime { get; set; }

        public double BulletSpeed { get; set; }
        public int BulletDamage { get; set; }
        public int BulletSize { get; set; }

        public int BarrelWidth { get; set; }
        public int BarrelHeight { get; set; }
    }
}
