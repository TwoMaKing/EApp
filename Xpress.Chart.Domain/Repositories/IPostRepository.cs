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
using Xpress.Chart.Domain;
using Xpress.Chart.Domain.Models;

namespace Xpress.Chart.Domain.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        IEnumerable<Post> GetPostsPublishedByUser(User user);
    }
}
