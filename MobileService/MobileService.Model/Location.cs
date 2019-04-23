using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MobileService.Model
{

    [DataContract]
    public class Location
    {
        [DataMember]
        public int LocationId { get; set; }
        [DataMember]
        public int Hits { get; set; }
        [DataMember]
        public bool IsTopLocation { get; set; }
        [DataMember]
        public string LocationName { get; set; }
        [DataMember]
        public string LocationDescription { get; set; }
        [DataMember]
        public double Latitude { get; set; }
        [DataMember]
        public double Longitude { get; set; }
        [DataMember]
        public User User { get; set; }
        [DataMember]
        public List<Rating> Ratings { get; set; }
        [DataMember]
        public List<Picture> Pictures { get; set; }
    }
}
