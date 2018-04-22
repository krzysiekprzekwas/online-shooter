namespace GameServer.MapObjects
{
    public class MapSphere : MapObject
    {

        public MapSphere(float x, float y, float z, float diameter, Color color = null, int texture = 0)
            : base(x, y, z, color, texture)
        {
            Diameter = diameter;
        }


        public float Diameter { get; set; }
    }
}
