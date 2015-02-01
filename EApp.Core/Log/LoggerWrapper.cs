using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core;
using log4net;

namespace EApp.Core.Log
{
    /// <summary>
    /// The wrapper used for Decorator Pattern
    /// </summary>
    public class LoggerWrapper : ILogger
    {
        private ILogger logger;

        public LoggerWrapper(ILogger logger)
        {
            this.logger = logger;
        }

        #region ILog Members

        public bool ErrorEnabled
        {
            get
            {
                return this.logger.ErrorEnabled;
            }
        }

        public bool FatalEnabled
        {
            get
            {
                return this.logger.FatalEnabled;
            }
        }

        public bool InfoEnabled
        {
            get
            {
                return this.logger.InfoEnabled;
            }
        }

        public bool WarnEnabled
        {
            get
            {
                return this.logger.WarnEnabled;
            }
        }

        public virtual void Error(object message, Exception exception)
        {
            this.logger.Error(message, exception);
        }

        public virtual void Error(object message)
        {
            this.logger.Error(message);
        }

        public virtual void ErrorFormat(string format, params object[] args)
        {
            this.logger.ErrorFormat(format, args);
        }

        public virtual void Fatal(object message, Exception exception)
        {
            this.logger.Fatal(message, exception);
        }

        public void Fatal(object message)
        {
            this.logger.Fatal(message);
        }

        public virtual void FatalFormat(string format, params object[] args)
        {
            this.logger.FatalFormat(format, args);
        }

        public virtual void Info(object message, Exception exception)
        {
            this.logger.Info(message, exception);
        }

        public virtual void Info(object message)
        {
            this.logger.Info(message);
        }

        public virtual void InfoFormat(string format, params object[] args)
        {
            this.logger.InfoFormat(format, args);
        }

        public virtual void Warn(object message, Exception exception)
        {
            this.logger.Warn(message, exception);
        }

        public virtual void Warn(object message)
        {
            this.logger.Warn(message);
        }

        public virtual void WarnFormat(string format, params object[] args)
        {
            this.logger.WarnFormat(format, args);
        }

        #endregion
    }
}
