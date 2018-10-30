using GameServer.Models;

namespace GameServer
{
    public interface IConfig
    {
        int BufferSize { get; set; }
        int ServerTick { get; set; }
        double PlayerSpeed { get; set; }
        double Gravity { get; set; }
        double JumpPower { get; set; }
        double PlayerDecceleration { get; set; }
        double PlayerRadius { get; set; }
        double IntersectionInterval { get; set; }
        double MinBulletSpeed { get; set; }
        double BulletDecceleraion { get; set; }
    }

    public class Config : IConfig
    {
        public Config()
        {
            BufferSize = 4 * 1024;
            ServerTick = 64;
            PlayerSpeed = 50;
            Gravity = 10;
            JumpPower = 2;
            PlayerDecceleration = 0.8;
            PlayerRadius = 16;
            IntersectionInterval = 0.01;
            MinBulletSpeed = 0.01;
            BulletDecceleraion = 0.99;
        }

        public int BufferSize { get; set; }
        public int ServerTick { get; set; }
        public double PlayerSpeed { get; set; }
        public double Gravity { get; set; }
        public double JumpPower { get; set; }
        public double PlayerDecceleration { get; set; }
        public double PlayerRadius { get; set; }
        public double IntersectionInterval { get; set; }
        public double MinBulletSpeed { get; set; }
        public double BulletDecceleraion { get; set; }
    }
}
