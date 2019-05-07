using System.Collections.Generic;

namespace MobilSemProjekt.MVVM.Model
{
    public class Location
    {
        public int LocationId { get; set; }
        public int Hits { get; set; }
        public bool IsTopLocation { get; set; }
        public string LocationName { get; set; }
        public string LocationDescription { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public User User { get; set; }
        public List<Rating> Ratings { get; set; }
        public List<Picture> Pictures { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
