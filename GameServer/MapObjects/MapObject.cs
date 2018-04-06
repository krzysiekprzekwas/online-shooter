using GameServer.Models;
using GameServer.States;
using System.Collections;
using System.Numerics;

namespace GameServer.MapObjects
{
    public abstract class MapObject
    {
        public MapObject(float x, float y, float z, MapObject parent, Color color, int textureId)
        {
            Id = MapState.Instance.AssingMapObjectId();

            Position = new Vector3(x, y, z);
            Parent = parent;

            TextureId = textureId;

            Color = color;
            if (color == null)
            {
                Color = new Color(1, 1, 1);
            }
        }

        public Vector3 Position { get; set; }
        public int Id { get; set; }

        public Color Color { get; set; }
        public int TextureId { get; set; }

        public MapObject Parent { get; set; }
    }

    public class MapObjectDistanceToPositionComparer : IComparer
    {
        private Vector3 _position;
        public MapObjectDistanceToPositionComparer(Vector3 position)
        {
            _position = position;
        }

        public int Compare(object x, object y)
        {
            MapObject a = x as MapObject;
            MapObject b = y as MapObject;

            float da = (_position - a.Position).LengthSquared();
            float db = (_position - b.Position).LengthSquared();

            if (da > db)
                return 1;
            else if (da == db)
                return 0;

            return -1;
        }
    }
}
