using GameServer.Models;
using GameServer.States;

namespace GameServer.MapObjects
{
    public abstract class MapObject
    {
        public MapObject(double x, double y, double z, Color color)
        {
            Id = MapState.Instance.AssingMapObjectId();

            Position = new Vector3d(x, y, z);

            Color = color;
            if(color == null)
            {
                Color = new Color(1, 1, 1);
            }
        }

        public Vector3d Position { get; set; }
        public int Id { get; set; }

        public Color Color { get; set; }
    }
}
