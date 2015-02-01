using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Domain.Core.Application
{
    /// <summary>
    /// 数据传输对象接口，用来将Domain Model 映射成 DTO, 或者把DTO 映射成 Domain Model.
    /// </summary>
    public interface IDataTransferObject<TModel, TIdentityKey> where TModel : class, IEntity<TIdentityKey>
    {
        void MapFrom(TModel domainModel);

        TModel MapTo();
    }

    public interface IDataTransferObject<TModel> : IDataTransferObject<TModel, int> where TModel : class, IEntity<int>, IEntity
    { 
        
    }

}
