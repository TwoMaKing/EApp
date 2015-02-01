using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Domain.Core;

namespace EApp.Domain.Core.Commands
{
    /// <summary>
    /// Used for CQRS. The implemented classes is commands  
    /// </summary>
    public interface ICommand : IEntity<int>, IEntity
    {

    }
}
