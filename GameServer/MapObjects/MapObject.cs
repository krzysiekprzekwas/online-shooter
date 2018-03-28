using GameServer.Models;
using GameServer.States;
using System.Numerics;

namespace GameServer.MapObjects
{
    public abstract class MapObject
    {
        public MapObject(float x, float y, float z, Color color)
        {
            Id = MapState.Instance.AssingMapObjectId();

            Position = new Vector3(x, y, z);

            Color = color;
            if(color == null)
            {
                Color = new Color(1, 1, 1);
            }
        }

        public Vector3 Position { get; set; }
        public int Id { get; set; }

        public Color Color { get; set; }
    }
}
