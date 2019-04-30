using System.Runtime.Serialization;

namespace MobileService.Exception
{
    [DataContract]
    public class DbConnectionException
    {   
        public override string ToString()
        {
            return $"Der er ikke forbindelse til databasen";
        }
    }
}
