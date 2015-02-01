using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core;
using EApp.Domain.Core.Commands;

namespace EApp.Domain.Core.Bus
{
    public interface ICommandBus : IUnitOfWork, IDisposable
    {
        void Clear();

        void Publish<TCommand>(TCommand command) where TCommand : class, ICommand;

        void Publish<TCommand>(IEnumerable<TCommand> commands) where TCommand : class, ICommand;
    }
}
