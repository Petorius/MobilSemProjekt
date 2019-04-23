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
        [WebGet(UriTemplate = "FindByUserName/{userName}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(UserNotDeletedException))]
        User FindByUserName(string userName);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "CompareHashes",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(UserNotDeletedException))]
        [FaultContract(typeof(UserOrPasswordException))]
        bool CompareHashes(User user);
        
        [OperationContract]
        [WebGet(UriTemplate = "FindSaltByUserName/{userName}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(UserNotDeletedException))]
        string FindSaltByUserName(string userName);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "Update",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        bool UpdateUser(List<User> users);
    }
}
