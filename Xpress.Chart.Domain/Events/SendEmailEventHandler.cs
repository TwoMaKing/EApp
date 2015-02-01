using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Domain.Core.Events;

namespace Xpress.Chat.Domain.Events
{
    [HandleAsynchronization()]
    public class SendEmailEventHandler : IEventHandler<OrderConfirmEvent>
    {
        public void Handle(OrderConfirmEvent t)
        {
            //Send Email
        }
    }
}
