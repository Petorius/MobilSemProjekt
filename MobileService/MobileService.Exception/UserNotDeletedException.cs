using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MobileService.Exception
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
