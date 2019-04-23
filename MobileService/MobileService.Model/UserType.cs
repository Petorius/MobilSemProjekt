using System.Runtime.Serialization;

namespace MobileService.Model
{
    [DataContract]
    public class UserType
    {
        [DataMember]
        public int UserTypeId { get; set; }
        [DataMember]
        public string TypeName { get; set; }
    }
}
