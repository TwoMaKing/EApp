using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.DomainDriven.Commands
{
    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : class, ICommand
    {
        public abstract void Handle(TCommand message);

    }
}
