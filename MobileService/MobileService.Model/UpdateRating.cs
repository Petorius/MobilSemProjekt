using System.Runtime.Serialization;

namespace MobileService.Model
{
    [DataContract]
    public class UpdateRating
    {
        [DataMember]
        public User User { get; set; }
        [DataMember]
        public Rating Rating { get; set; }
    }
}