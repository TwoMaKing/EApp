using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.DomainDriven.Commands
{
     /// <summary>
     /// Command handler to handle command
     /// </summary>
     /// <typeparam name="TCommand"></typeparam>
    public interface ICommandHandler<TCommand> : IHandler<TCommand> where TCommand : class, ICommand
    {
        //void Handle(ICommand command);
    }
}
