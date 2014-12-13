using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Infrastructure.Domain;

namespace Xpress.Chart.Domain.Models
{
    public class Topic : EntityBase, IAggregateRoot
    {
        private int id;

        private string name = string.Empty;

        public Topic(int id, string name) 
        {
            this.id = id;

            this.name = name;
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

        public object Id
        {
            get
            {
                return this.id;
            }
        }
    }
}
