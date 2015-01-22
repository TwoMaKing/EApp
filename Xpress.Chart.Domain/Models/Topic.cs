using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.DomainDriven.Domain;

namespace Xpress.Chat.Domain.Models
{
    public class Topic : EntityBase, IAggregateRoot
    {
        public Topic() : base() { }

        public string Name
        {
            get;
            set;
        }

        public string Summary { get; set; }

        public DateTime ExpiredDate { get; set; }

        public static Topic Create(string name, string summary, DateTime expiredDate) 
        {
            Topic topic = new Topic();

            topic.Name = name;
            topic.Summary = summary;
            topic.ExpiredDate = expiredDate;

            return topic;
        } 
    }
}
