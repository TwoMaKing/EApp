using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.Application;
using EApp.Core.Configuration;

namespace EApp.Domain.Core.DirectBus
{
    public class MessageDispatchercs : IMessageDispatcher
    {
        private Dictionary<Type, List<object>> handlerDictionary = new Dictionary<Type, List<object>>();

        public void Dispatch<T>(T message)
        {
            throw new NotImplementedException();
        }

        public void Register<T>(IHandler<T> handler)
        {
            Type messageType = typeof(T);

            if (!this.handlerDictionary.ContainsKey(messageType))
            {
                this.handlerDictionary.Add(messageType, new List<object>());
            }

            var handlerList = this.handlerDictionary[messageType];

            if (handlerList == null)
            {
                handlerList = new List<object>();
            }

            if (!handlerList.Contains(handler))
            {
                handlerList.Add(handler);
            }
        }

        public void UnRegister<T>(IHandler<T> handler)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

    }
}
