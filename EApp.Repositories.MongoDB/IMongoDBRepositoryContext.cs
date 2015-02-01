using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Domain.Core.Repositories;
using MongoDB;

namespace EApp.Repositories.MongoDB
{
    public interface IMongoDBRepositoryContext : IRepositoryContext
    {
        IMongoDatabase MongoDatabase { get; }
    }
}
