using System.Numerics;
using GameServer.States;

namespace GameServer.MapObjects
{
    public class MapObject
    {
        public MapObject(float x, float y, float z, Color color, int textureId)
        {
            Id = MapState.Instance.AssingMapObjectId();

            Position = new Vector3(x, y, z);

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
    }
}
