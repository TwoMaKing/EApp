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
    public class LightPostDataObject : IDataTransferObject<Post>
    {
        public LightPostDataObject() 
        {
            this.Topic = new LightTopicDataObject();
        }

        [DataMember()]
        public int Id { get; set; }

        [DataMember()]
        public string TopicName { get; set; }

        [DataMember()]
        public string AuthorName { get; set; }

        [DataMember()]
        public string Content { get; set; }

        [DataMember()]
        public DateTime CreationDateTime { get; set; }

        [DataMember()]
        public LightTopicDataObject Topic { get; set; }

        public void MapFrom(Post domainModel)
        {
            this.Id = domainModel.Id;
            
            LightTopicDataObject topic = new LightTopicDataObject();

            topic.Id = domainModel.Topic.Id;
            topic.Name = domainModel.Topic.Name;
            this.Topic = topic;

            this.Content = domainModel.Content;
            this.CreationDateTime = domainModel.CreationDateTime;
        }

        public Post MapTo()
        {
            throw new NotImplementedException();
        }
    }
}
