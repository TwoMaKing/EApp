using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.DomainDriven.Commands
{
    public interface ICommandDispatcher
    {
        void Clear();
 
        void Register<TCommand>(ICommandHandler<TCommand> commandHandler) where TCommand : class, ICommand;

        void UnRegister<TCommand>(ICommandHandler<TCommand> commandHandler) where TCommand : class, ICommand;

        void Dispatch<TCommand>(TCommand command) where TCommand : class, ICommand;

        ICommandHandler<TCommand> GetCommandHandler<TCommand>() where TCommand : class, ICommand;
    }
}
