using EApp.Core.DomainDriven.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.DomainDriven.Application
{
    public abstract class DataTransferObjectBase<TModel, TIdentity> : IDataTransferObject<TModel, TIdentity> where TModel : class, IEntity<TIdentity>
    {
        public TIdentity Id { get; set; }

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

    public abstract class DataTransferObjectBase<TModel> : DataTransferObjectBase<TModel, int>, IDataTransferObject<TModel> where TModel : class, IEntity
    {
    }

}
