using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace EApp.Domain.Core.Application
{
    [Serializable()]
    [DataContract()]
    [XmlRoot()]
    public abstract class DataTransferObjectBase<TModel, TIdentity> : IDataTransferObject<TModel, TIdentity> where TModel : class, IEntity<TIdentity>
    {
        public void MapFrom(TModel domainModel)
        {
            this.DoMapFrom(domainModel);
        }

        public TModel MapTo()
        {
            return this.DoMapTo();
        }

        protected abstract void DoMapFrom(TModel domainModel);

        protected abstract TModel DoMapTo();
    }

    [Serializable()]
    [DataContract()]
    [XmlRoot()]
    public abstract class DataTransferObjectBase<TModel> : DataTransferObjectBase<TModel, int>, IDataTransferObject<TModel> where TModel : class, IEntity
    {

    }

}
