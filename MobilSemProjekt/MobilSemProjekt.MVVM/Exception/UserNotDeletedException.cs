using System.Runtime.Serialization;

namespace MobilSemProjekt.MVVM.Exception
{
    [DataContract]
    public class UserNotDeletedException
    {
        [DataMember]
        public string UserName { get; set; }
        
        public UserNotDeletedException(string userName)
        {
            UserName = userName;
        }
        
        public override string ToString()
        {
            return $"Brugeren {UserName} blev ikke slettet.";
        }
    }
}
