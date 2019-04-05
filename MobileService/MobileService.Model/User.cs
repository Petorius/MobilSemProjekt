using System.Runtime.Serialization;

namespace MobileService.Model
{
    [DataContract]
    public class User
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string HashPassword { get; set; }
        [DataMember]
        public string Salt { get; set; }
    }
}
