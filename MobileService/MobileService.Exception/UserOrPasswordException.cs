using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace MobileService.Exception
{
    [DataContract]
    public class UserOrPasswordException
    {
        [DataMember]
        public string ReturnMessage { get; set; }

        public UserOrPasswordException()
        {
            ReturnMessage = $"Brugernavnet eller koden er forkert.";
        }

        public override string ToString()
        {
            return ReturnMessage;
        }
    }
}
