using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Infrastructure.Domain;

namespace Xpress.Chart.Domain.Models
{
    public class Topic : EntityBase, IAggregateRoot
    {
        private string name = string.Empty;

        public Topic(int id, string name) 
        {
            this.name = name;
            this.Id = id;
        }

        public string Name 
        {
            get 
            {
                return this.name;
            } 
        }

        public string Summary { get; set; }

        public DateTime ExpiredDate { get; set; }

    }
}
