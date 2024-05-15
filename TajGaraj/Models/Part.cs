namespace TajGaraj.Models
{
    public class Part
    {
        public int ID { get; set; } // auto incrementing ID
        public string Name { get; set; } // name of the part 
        public string Description { get; set; } // description of the part
        public bool Warranty { get; set; } // warranty status 
        public string Vendor { get; set; } // vendor of the part
        public string condition { get; set; } // condition of the part from preset 
        public bool installed { get; set; } // installation status
        public string Location { get; set; } // location of the part
    }
}
