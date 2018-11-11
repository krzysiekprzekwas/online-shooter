namespace GameServer.Models
{
    public class Bullet
    {
        public Bullet()
        { }

        public Bullet(Bullet otherBullet)
            : this()
        {
            Position = new Vector2(otherBullet.Position);
            Radius = otherBullet.Radius;
            Speed = new Vector2(otherBullet.Speed);
            PlayerId = otherBullet.PlayerId;
            Id = otherBullet.Id;
        }

        public Vector2 Position { get; set; }
        public double Radius { get; set; }
        public Vector2 Speed { get; set; }
        public int PlayerId { get; set; }
        public int Id { get; set; }
    }
}
