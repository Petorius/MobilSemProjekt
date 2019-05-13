using System.Runtime.Serialization;

namespace MobilSemProjekt.MVVM.Exception
{
    [DataContract]
    public class LocationNotDeletedException
    {
        [DataMember]
        public string ReturnMessage { get; set; }

        public LocationNotDeletedException()
        {
            ReturnMessage = $"Lokationen blev ikke slettet i databasen.";
        }

        public override string ToString()
        {
            return ReturnMessage;
        }
    }
}
