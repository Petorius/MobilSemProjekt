using System.Runtime.Serialization;

namespace MobilSemProjekt.MVVM.Exception
{
    [DataContract]
    public class NoLocationsInDatabaseException
    {
        [DataMember]
        public string ReturnMessage { get; set; }

        public NoLocationsInDatabaseException()
        {
            ReturnMessage = $"Der er ingen lokationer i databasen.";
        }

        public override string ToString()
        {
            return ReturnMessage;
        }
    }
}
