﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EApp.Core
{
    public interface ILoggerFactory
    {
        ILogger CreateLogger();
    }

}