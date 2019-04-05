using System.Runtime.Serialization;

namespace MobileService.Model
{
    [DataContract]
    public class Picture
    {
        [DataMember]
        public int PictureId { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public string PictureName { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
