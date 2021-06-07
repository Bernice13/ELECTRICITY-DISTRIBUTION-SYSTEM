namespace PortalLibrary.Models
{
    public class Meter
    {
        public string Id { get; set; }
        public string MeterNumber { get; set; }
        public MeterType Type { get; set; }
        public string ProductName { get; set; }
    }
}