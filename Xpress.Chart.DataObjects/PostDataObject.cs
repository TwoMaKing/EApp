using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Xpress.Chart.DataObjects
{

    [DataContract()]
    public class PostDataObject
    {
        [DataMember()]
        public string TopicName { get; set; }

        [DataMember()]
        public string AuthorName { get; set; }

        [DataMember()]
        public string Content { get; set; }

        [DataMember()]
        public DateTime CreationDateTime { get; set; }

        [DataMember()]
        public List<CommentDataObject> Comments { get; set; }
    }
}
