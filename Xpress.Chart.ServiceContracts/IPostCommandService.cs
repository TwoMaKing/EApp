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
    public interface IPostCommandService
    {
        [OperationContract()]
        void PublishPost(PostDataObject post);
    }

}
