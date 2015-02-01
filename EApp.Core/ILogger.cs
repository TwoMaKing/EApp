using System;

namespace EApp.Core
{
    public interface ILogger
    {
        bool ErrorEnabled { get; }

        bool FatalEnabled { get; }
        
        bool InfoEnabled { get; }
        
        bool WarnEnabled { get; }

        void Error(object message);
        void Error(object message, Exception exception);
        void ErrorFormat(string format, params object[] args);

        void Fatal(object message);
        void Fatal(object message, Exception exception);
        void FatalFormat(string format, params object[] args);

        void Info(object message);
        void Info(object message, Exception exception);
        void InfoFormat(string format, params object[] args);

        void Warn(object message);
        void Warn(object message, Exception exception);
        void WarnFormat(string format, params object[] args);

    }
}
