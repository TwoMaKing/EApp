using EApp.Core.DomainDriven.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Xpress.Chat.Domain.Models;

namespace Xpress.Chat.DataObjects
{

     [DataContract()]
    public class TopicDataObject : DataTransferObjectBase<Topic>
    {
        [DataMember()]
        public string Name { get; set; }

        [DataMember()]
        public string Summary { get; set; }

        [DataMember()]
        public DateTime ExpiredDate { get; set; }

        protected override void DoMapFrom(Topic domainModel)
        {
            this.Id = domainModel.Id;
            this.Name = domainModel.Name;
            this.Summary = domainModel.Summary;
            this.ExpiredDate = domainModel.ExpiredDate;
        }

        protected override Topic DoMapTo()
        {
            Topic topic = new Topic();

            topic.Id = this.Id;
            topic.Name = this.Name;
            topic.Summary = this.Summary;
            topic.ExpiredDate = this.ExpiredDate;

            return topic;
        }
    }
}
