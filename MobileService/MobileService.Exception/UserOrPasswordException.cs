using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace MobileService.Exception
{
    [DataContract]
    public class UserOrPasswordException
    {
        [DataMember]
        public string Message { get; set; }

        public UserOrPasswordException()
        {
            Message = $"Brugernavnet eller koden er forkert.";
        }
    }
}
