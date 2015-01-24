using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Xpress.Chat.DataObjects;
using Xpress.Chat.Domain.Models;

namespace Xpress.Chat.ServiceContracts
{
    [ServiceContract()]
    public interface IPostService
    {
        [OperationContract()]
        void PublishPost(PostDataObject post);

        [OperationContract()]
        [WebGet(UriTemplate = "Posts/All", ResponseFormat=WebMessageFormat.Json)]
        IEnumerable<PostDataObject> GetPosts();
    }
}
