using GameServer.Models;
using System;

namespace GameServer
{
    public interface IConfig
    {
        int BufferSize { get; set; }
        int ClientStateIntervalMilliseconds { get; set; }
        double PlayerRadius { get; set; }
        double IntersectionInterval { get; set; }
        double MinBulletSpeed { get; set; }
        int MaxPlayerHealth { get; set; }
        bool Extrapolation { get; set; }
        int ServerTickMilliseconds { get; set; }
        double ServerTicksPerSecond { get; set; }
        double PlayerDeccelerationPerTick { get; set; }
        double PlayerSpeedPerTick { get; set; }
        double BulletDecceleraionPerTick { get; set; }
        int MilisecondsToResurect { get; set; }
    }

    public class Config : IConfig
    {
        public Config()
        {
            BufferSize = 4 * 1024;
            ClientStateIntervalMilliseconds = 50;
            PlayerRadius = 16;
            IntersectionInterval = 0.01;
            MinBulletSpeed = 0.1;
            MaxPlayerHealth = 100;
            Extrapolation = true;

            ServerTickMilliseconds = 250;
            ServerTicksPerSecond = 1000.0 / ServerTickMilliseconds;

            _playerDeccelerationPerSecond = 0.001;
            PlayerDeccelerationPerTick = Math.Pow(_playerDeccelerationPerSecond, 1.0 / ServerTicksPerSecond);

            _playerSpeedPerSecond = 50;
            PlayerSpeedPerTick = _playerSpeedPerSecond / ServerTicksPerSecond;

            _bulletDeccelerationPerSecond = 0.1;
            BulletDecceleraionPerTick = Math.Pow(_bulletDeccelerationPerSecond, 1.0 / ServerTicksPerSecond);
        }

        private double _playerDeccelerationPerSecond { get; set; }
        private double _playerSpeedPerSecond { get; set; }
        private double _bulletDeccelerationPerSecond { get; set; }

        public int MilisecondsToResurect { get; set; }
        public int BufferSize { get; set; }
        public int ClientStateIntervalMilliseconds { get; set; }
        public double PlayerRadius { get; set; }
        public double IntersectionInterval { get; set; }
        public double MinBulletSpeed { get; set; }
        public int MaxPlayerHealth { get; set; }
        public bool Extrapolation { get; set; }

        public int ServerTickMilliseconds { get; set; }
        public double ServerTicksPerSecond { get; set; }
        public double PlayerDeccelerationPerTick { get; set; }
        public double PlayerSpeedPerTick { get; set; }
        public double BulletDecceleraionPerTick { get; set; }
    }
}
