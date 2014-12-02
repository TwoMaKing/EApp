using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core;
using log4net;

namespace EApp.Common.Log.Log4Net
{
    public class Log4NetLoggerFactory : ILoggerFactory
    {
        public ILogger CreateLogger()
        {
            return new Log4NetLogger();
        }
    }
}
