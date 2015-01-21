using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.DomainDriven.Bus
{
    public interface IMessageDispatcher
    {
        void Dispatch<T>(T message);

        void Register<T>(IHandler<T> handler);

        void UnRegister<T>(IHandler<T> handler);

        void Clear();
    }
}
