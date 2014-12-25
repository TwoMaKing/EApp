using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.DomainDriven
{
    public interface IHandler<T>
    {
        void Handle(T message);
    }
}
