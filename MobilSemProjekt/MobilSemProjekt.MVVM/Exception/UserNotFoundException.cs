using System.Runtime.Serialization;

namespace MobilSemProjekt.MVVM.Exception
{
    [DataContract]
    public class UserNotFoundException
    {
        [DataMember]
        public string UserName { get; set; }
        
        public UserNotFoundException(string userName)
        {
            UserName = userName;
        }
        
        public override string ToString()
        {
            return $"Brugeren {UserName} blev ikke fundet.";
        }
    }
}
