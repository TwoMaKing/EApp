using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Core.DomainDriven.Domain;
using EApp.Core.DomainDriven.Repository;
using EApp.Core.DomainDriven.UnitOfWork;
using EApp.Core.QuerySepcifications;
using Xpress.Chat.Domain;
using Xpress.Chat.Domain.Models;

namespace Xpress.Chat.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {

    }
}
