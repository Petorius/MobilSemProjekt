using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using MobileService.Exception;
using MobileService.Model;

namespace MobileService.WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "CreateUser",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        int CreateUser(User user);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "CompareHashes",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(UserNotFoundException))]
        [FaultContract(typeof(UserOrPasswordException))]
        bool CompareHashes(User user);
        
        [OperationContract]
        [WebGet(UriTemplate = "FindSaltByUserName/{userName}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(UserNotFoundException))]
        string FindSaltByUserName(string userName);
    }
}
