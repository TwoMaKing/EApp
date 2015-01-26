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
    public class LightTopicDataObject : IDataTransferObject<Topic>
    {
        [DataMember()]
        public int Id { get; set; }

        [DataMember()]
        public string Name { get; set; }

        public void MapFrom(Topic domainModel)
        {
            throw new NotImplementedException();
        }

        public Topic MapTo()
        {
            throw new NotImplementedException();
        }
    }
}
