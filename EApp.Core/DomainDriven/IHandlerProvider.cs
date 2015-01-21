using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.DomainDriven
{
    public interface IHandlerProvider
    {
        IEnumerable GetHandlers();
    }
}
