using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Xpress.Chat.DataObjects;
using Xpress.Chat.Domain.Models;

namespace Xpress.Chat.ServiceContracts
{
    [ServiceContract()]
    public interface IQueryService
    {
        [OperationContract()]
        [WebInvoke(UriTemplate = "Posts/All", Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<PostDataObject> GetPosts();

        [OperationContract()]
        [WebInvoke(UriTemplate = "Posts/Query", Method = "Post", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<PostDataObject> GetPostsByQueryRequest(QueryRequest request);
    }
}
