using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace EApp.Core.Configuration
{
    /// <summary>
    /// Represents that the implemented classes are configuration sources for EApp framework.
    /// </summary>
    public interface IConfigSource
    {
        EAppConfigurationSection Config { get; }
    }
}
