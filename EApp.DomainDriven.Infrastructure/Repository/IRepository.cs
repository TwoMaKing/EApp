using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.DomainDriven.Infrastructure.Domain;
using EApp.DomainDriven.Infrastructure.UnitOfWork;

namespace EApp.DomainDriven.Infrastructure.Repository
{

    /// <summary>
    /// Repository main interface
    /// </summary>
    public interface IRepository<TAggregateRoot> where TAggregateRoot : class, IAggregateRoot
    {
        /// <summary>
        /// Unit Of Work模式, 工作单元
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        ///  新增一个item到Repository
        /// </summary>
        void Add(TAggregateRoot item);

        /// <summary>
        /// 新增 items 到Repository
        /// </summary>
        void Add(IEnumerable<TAggregateRoot> items);

        /// <summary>
        /// 更新 item 到Repository
        /// </summary>
        void Update(TAggregateRoot item);

        /// <summary>
        /// 更新 items 到Repository
        /// </summary>
        void Update(IEnumerable<TAggregateRoot> items);

        /// <summary>
        /// 从 Repository 删除 item
        /// </summary>
        void Delete(TAggregateRoot item);

        /// <summary>
        /// 从 Repository 删除 items
        /// </summary>
        void Delete(IEnumerable<TAggregateRoot> items);

        /// <summary>
        /// Execute the specified command SQL text
        /// </summary>
        int ExecuteNonQuery(string commandSqlText);

        /// <summary>
        /// Get specific entity by id or key
        /// </summary>
        TAggregateRoot FindByKey<TKey>(TKey idOrKey);

        /// <summary>
        /// Get specific entity by query (Expression)
        /// </summary>
        IEnumerable<TAggregateRoot> FindAll(Func<TAggregateRoot, bool> expression);

    }
}
