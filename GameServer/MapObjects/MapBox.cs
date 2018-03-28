namespace GameServer.MapObjects
{
    public class MapBox : MapObject
    {
        public string Type = @"box";
        public MapBox(float x, float y, float z, float w, float h, float d, Color color = null)
            : base(x, y, z, color)
        {
            Width = w;
            Height = h;
            Depth = d;
        }

        public float Width { get; set; }
        public float Height { get; set; }
        public float Depth { get; set; }
    }
}
