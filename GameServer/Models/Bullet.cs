namespace GameServer.Models
{
    public class Bullet
    {
        public Vector2 Position { get; set; }
        public double Radius { get; set; }
        public Vector2 Speed { get; set; }
        public int PlayerId { get; set; }
        public int Id { get; set; }
    }
}
