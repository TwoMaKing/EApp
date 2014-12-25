using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.DomainDriven.UnitOfWork
{
    /// <summary>
    /// Unit Of Work模式，即工作单元，它是一种数据访问模式。
    /// 它是用来维护一个由已经被业务修改(如增加、删除和更新等)的业务对象组成的列表。
    /// 它负责协调这些业务对象的持久化工作及并发问题。那它是怎么来维护的一系列业务对象组成的列表持久化工作的呢？
    /// 通过事务。Unit Of Work模式会记录所有对象模型修改过的信息，在提交的时候，一次性修改，并把结果同步到数据库。 
    /// 这个过程通常被封装在事务中。所以在DAL中采用Unit Of Work模式好处就在于能够确保数据的完整性，
    /// 如果在持有一系列业务对象（同属于一个事务）的过程中出现问题，就可以将所有的修改回滚，以确保数据始终处于有效状态，不会出现脏数据。
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// 当前的Unit Of Work事务是否已被提交。
        /// </summary>
        bool Committed { get; }

        /// <summary>
        /// 提交当前的Unit Of Work事务。
        /// </summary>
        void Commit();

        /// <summary>
        /// 回滚当前的Unit Of Work事务。
        /// </summary>
        void Rollback();
    }
}
