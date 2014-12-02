using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Appender;
using log4net.Core;

namespace EApp.Common.Log
{
    public interface ILoggerFactory
    {
        ILogger CreateLogger();
    }

}
