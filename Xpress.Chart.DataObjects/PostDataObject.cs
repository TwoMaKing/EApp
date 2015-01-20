using EApp.Core.DomainDriven.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Xpress.Chart.Domain.Models;

namespace Xpress.Chart.DataObjects
{

    [DataContract()]
    public class PostDataObject : DataTransferObjectBase<Post>
    {
        //[DataMember()]
        //public int TopicId { get; set; }

        //[DataMember()]
        //public int AuthorId { get; set; }

        //[DataMember()]
        //public string TopicName { get; set; }

        //[DataMember()]
        //public string AuthorName { get; set; }
        
        [DataMember()]
        public TopicDataObject Topic { get; set; }

        [DataMember()]
        public UserDataObject Author { get; set; }

        [DataMember()]
        public string Content { get; set; }

        [DataMember()]
        public DateTime CreationDateTime { get; set; }

        [DataMember()]
        public List<CommentDataObject> Comments { get; set; }

        protected override void DoMapFrom(Post domainModel)
        {
            this.Id = domainModel.Id;
            
            TopicDataObject topic = new TopicDataObject();
            topic.Id = domainModel.Topic.Id;
            topic.Name = domainModel.Topic.Name;
            this.Topic = topic;

            UserDataObject author = new UserDataObject();
            author.Id = domainModel.Author.Id;
            author.Name = domainModel.Author.Name;
            author.NickName = domainModel.Author.NickName;
            this.Author = author;

            this.Content = domainModel.Content;
            this.CreationDateTime = domainModel.CreationDateTime;

            //this.AuthorId = domainModel.Author.Id;
            //this.AuthorName = domainModel.Author.Name;
            //this.TopicId = domainModel.Topic.Id;
            //this.TopicName = domainModel.Topic.Name;
        }

        protected override Post DoMapTo()
        {
            Post post = new Post();
            post.Id = this.Id;
            
            Topic topic = new Topic();
            topic.Id = this.Topic.Id;
            post.Topic = topic;

            User author = new User();
            author.Id = this.Author.Id;
            post.Author = author;

            post.Content = this.Content;
           
            return post;
        }
    }
}
