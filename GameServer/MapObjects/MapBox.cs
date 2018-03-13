namespace GameServer.MapObjects
{
    public class MapBox : MapObject
    {
        public string Type = @"box";
        public MapBox(double x, double y, double z, double w, double h, double d, Color color = null)
            : base(x, y, z, color)
        {
            Width = w;
            Height = h;
            Depth = d;
        }

        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }
    }
}
