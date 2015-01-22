using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Xpress.Chat.DataObjects
{
    [DataContract()]
    public class QueryRequest
    {
        [DataMember()]
        public int PageNo { get; set; }

        [DataMember()]
        public int PageSize { get; set; }
    }
}
