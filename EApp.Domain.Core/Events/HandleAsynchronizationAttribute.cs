using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Domain.Core.Events
{
    /// <summary>
    /// Represents this is an asynchronization event/command hanlder
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited=false)]
    public class HandleAsynchronizationAttribute : Attribute
    {

    }
}
