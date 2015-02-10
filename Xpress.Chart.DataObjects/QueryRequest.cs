using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using EApp.Core.Query;

namespace Xpress.Chat.DataObjects
{
    [DataContract()]
    [Serializable()]
    public class PostQueryRequest
    {
        private int pageNo = 1;

        private int pageSize = int.MaxValue;

        private CreationDateTimeParam creationDateTimeParam = new CreationDateTimeParam();

        private MiscParams miscParams = new MiscParams();

        [DataMember()]
        public int TopicId { get; set; }

        [DataMember()]
        public string Content { get; set; }

        [DataMember()]
        public CreationDateTimeParam CreationDateTimeParam 
        {
            get 
            {
                return creationDateTimeParam;
            }
            set 
            {
                this.creationDateTimeParam = value;
            }
        }

        [DataMember()]
        public MiscParams MiscParams 
        {
            get
            {
                return this.miscParams;
            }
            set
            {
                this.miscParams = value;
            }
        }

        [DataMember()]
        public int PageNo 
        {
            get 
            {
                return this.pageNo;
            }
            set 
            {
                this.pageNo = value;
            }
        }

        [DataMember()]
        public int PageSize 
        {
            get 
            {
                return this.pageSize;
            }
            set 
            {
                this.pageSize = value;
            } 
        }
    }

    [DataContract()]
    public class CreationDateTimeParam
    {
        [DataMember()]
        public Operator CreationDateTimeOperator { get; set; }

        [DataMember()]
        public DateTime CreationDateTime { get; set; }
    }

    [DataContract()]
    public class MiscParams
    {
        [DataMember()]
        public int TopicId { get; set; }

        [DataMember()]
        public string Content { get; set; }
    }

}
