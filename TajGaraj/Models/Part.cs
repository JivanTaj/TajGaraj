namespace TajGaraj.Models
{
    public class Part
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Warranty { get; set; }
        public string Vendor { get; set; }
        public string condition { get; set; }
        public bool installed { get; set; }
        public string Location { get; set; }
    }
}
