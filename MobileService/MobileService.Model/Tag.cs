using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MobileService.Model
{
    [DataContract]
    public class Tag
    {
        [DataMember]
        public int TagId { get; set; }
        [DataMember]
        public string TagName { get; set; }
        [DataMember]
        public List<Location> Locations { get; set; }
    }
}
