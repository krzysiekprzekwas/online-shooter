using GameServer.States;

namespace GameServer.MapObjects
{
    public abstract class MapObject
    {
        public MapObject(double x, double y, double z, Color color)
        {
            Id = MapState.Instance.AssingMapObjectId();

            X = x;
            Y = y;
            Z = z;

            Color = color;
            if(color == null)
            {
                Color = new Color(1, 1, 1);
            }
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public int Id { get; set; }

        public Color Color { get; set; }
    }
}
