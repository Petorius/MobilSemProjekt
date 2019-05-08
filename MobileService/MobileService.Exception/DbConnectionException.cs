using System.Runtime.Serialization;

namespace MobileService.Exception
{
    [DataContract]
    public class Exception
    {   
        public override string ToString()
        {
            return $"Der er ikke forbindelse til databasen";
        }
    }
}
