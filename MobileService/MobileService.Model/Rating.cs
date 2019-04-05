using System.Runtime.Serialization;

namespace MobileService.Model
{
    [DataContract]
    public class Rating
    {
        [DataMember]
        public int RatingId { get; set; }
        [DataMember]
        public double Rate { get; set; }
        [DataMember]
        public string Comment { get; set; }
        [DataMember]
        public User User { get; set; }
    }
}