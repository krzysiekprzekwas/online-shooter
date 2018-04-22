namespace CoreGameServer.MapObjects
{
    public class MapBox : MapObject
    {
        public string Type = @"box";
        public MapBox(float x, float y, float z, float w, float h, float d, Color color = null, int texture = 0)
            : base(x, y, z, color, texture)
        {
            Width = w;
            Height = h;
            Depth = d;
            TextureId = TextureId;
        }

        public float Width { get; set; }
        public float Height { get; set; }
        public float Depth { get; set; }
    }
}
