using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Xpress.Chat.DataObjects;
using Xpress.Chat.Domain.Models;

namespace Xpress.Chat.ServiceContracts
{
    [ServiceContract()]
    public interface IPostCommandService
    {
        [OperationContract()]
        void PublishPost(PostDataObject post);
    }

}
