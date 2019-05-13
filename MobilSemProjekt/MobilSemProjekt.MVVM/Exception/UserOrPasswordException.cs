using System.Runtime.Serialization;

namespace MobilSemProjekt.MVVM.Exception
{
    [DataContract]
    public class UserOrPasswordException : System.Exception
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
