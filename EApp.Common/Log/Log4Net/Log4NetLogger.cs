using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core;
using log4net;


namespace EApp.Common.Log.Log4Net
{
    public class Log4NetLogger : ILogger
    {
        private ILog currentLogHandler = LogManager.GetLogger("");

        public Log4NetLogger() 
        { 
            
        }
 
        public bool ErrorEnabled
        {
            get 
            {
                return this.currentLogHandler.IsErrorEnabled;
            }
        }

        public bool FatalEnabled
        {
            get 
            {
                return this.currentLogHandler.IsFatalEnabled;
            }
        }

        public bool InfoEnabled
        {
            get 
            {
                return this.currentLogHandler.IsInfoEnabled;
            }
        }

        public bool WarnEnabled
        {
            get 
            {
                return this.currentLogHandler.IsWarnEnabled; 
            }
        }

        public void Error(object message)
        {
            this.currentLogHandler.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            this.currentLogHandler.Error(message, exception);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            this.currentLogHandler.ErrorFormat(format, args);
        }

        public void Fatal(object message)
        {
            this.currentLogHandler.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            this.currentLogHandler.Fatal(message, exception);
        }

        public void FatalFormat(string format, params object[] args)
        {
            this.currentLogHandler.FatalFormat(format, args);
        }

        public void Info(object message)
        {
            this.currentLogHandler.Info(message);
        }

        public void Info(object message, Exception exception)
        {
            this.currentLogHandler.Info(message, exception);
        }

        public void InfoFormat(string format, params object[] args)
        {
            this.currentLogHandler.InfoFormat(format, args);
        }

        public void Warn(object message)
        {
            this.currentLogHandler.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            this.currentLogHandler.Warn(message, exception);
        }

        public void WarnFormat(string format, params object[] args)
        {
            this.currentLogHandler.WarnFormat(format, args);
        }
    }
}
