using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Xpress.Chart.DataObjects;
using Xpress.Chart.Domain.Models;

namespace Xpress.Chart.ServiceContracts
{
    [ServiceContract()]
    public interface IPostService
    {
        [OperationContract()]
        IEnumerable<PostDataObject> GetPosts(int pageNo, int pageSize);

        [OperationContract()]
        PostDataObject PublishPost(int topicId, int authorId, string content);
    }
}
