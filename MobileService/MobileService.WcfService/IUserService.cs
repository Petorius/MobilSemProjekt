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
        [WebGet(UriTemplate = "CompareHashes/{userName}/{userHash}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(UserNotFoundException))]
        [FaultContract(typeof(UserOrPasswordException))]
        bool CompareHashes(string userName, string userHash);
        
        [OperationContract]
        [WebGet(UriTemplate = "FindSaltByUserName/{userName}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(UserNotFoundException))]
        string FindSaltByUserName(string userName);
    }
}
