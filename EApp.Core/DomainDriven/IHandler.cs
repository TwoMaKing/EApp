using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.DomainDriven
{
    /// <summary>
    /// Handle message. e.g. used for event handler (domain handler) or command handler (CQRS)
    /// </summary>
    public interface IHandler<T>
    {
        void Handle(T message);
    }
}
