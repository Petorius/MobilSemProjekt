using System.Runtime.Serialization;
using System.ServiceModel;

namespace MobilSemProjekt.MVVM.Exception
{
    [DataContract]
    public class EmptyInputException : System.Exception
    {
        [DataMember]
        public string ReturnMessage { get; set; }

        public EmptyInputException()
        {
            ReturnMessage = $"Der er ikke indtastet et brugernavn eller kodeord.";
        }

        public override string ToString()
        {
            return ReturnMessage;
        }
    }
}
