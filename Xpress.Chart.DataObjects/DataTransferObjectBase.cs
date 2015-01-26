using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using EApp.Core.DomainDriven.Application;
using EApp.Core.DomainDriven.Domain;

namespace Xpress.Chat.DataObjects
{
    [DataContract()]
    public class DataTransferObjectBase<TModel> //: IDataTransferObject<TModel> where TModel : class, IEntity
    {
        public DataTransferObjectBase() { }

        public void MapFrom(TModel domainModel)
        {
            this.DoMapFrom(domainModel);
        }

        public TModel MapTo()
        {
            return this.DoMapTo();
        }

        protected virtual void DoMapFrom(TModel domainModel) { }

        protected virtual TModel DoMapTo() { return default(TModel); }
    }
}
