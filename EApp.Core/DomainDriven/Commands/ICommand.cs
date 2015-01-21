using EApp.Core.DomainDriven.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.DomainDriven.Commands
{
    /// <summary>
    /// Used for CQRS. The implemented classes is commands  
    /// </summary>
    public interface ICommand : IEntity
    {

    }
}
