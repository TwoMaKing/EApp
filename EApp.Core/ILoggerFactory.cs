using System;

namespace EApp.Core
{
    public interface ILoggerFactory
    {
        ILogger CreateLogger();
    }

}
