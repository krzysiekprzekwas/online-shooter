namespace GameServer.Models
{
    public class SpawnPoint
    {
        public SpawnPoint(Vector2 positon)
        {
            Position = positon;
        }

        public SpawnPoint(double x, double y)
            : this(new Vector2(x, y))
        {
        }

        public Vector2 Position { get; set; }
    }
}
