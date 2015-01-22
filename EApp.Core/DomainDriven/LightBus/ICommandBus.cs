using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.DomainDriven.Commands;
using EApp.Core.DomainDriven.Domain;
using EApp.Core.DomainDriven.UnitOfWork;

namespace EApp.Core.DomainDriven.LightBus
{
    public interface ICommandBus : IUnitOfWork, IDisposable
    {
        void Clear();

        void Publish<TCommand>(TCommand command) where TCommand : class, ICommand;

        void Publish<TCommand>(IEnumerable<TCommand> commands) where TCommand : class, ICommand;
    }
}
