using System.Runtime.Serialization;

namespace MobilSemProjekt.MVVM.Exception
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
