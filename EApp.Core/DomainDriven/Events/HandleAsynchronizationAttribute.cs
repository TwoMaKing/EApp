using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.DomainDriven.Events
{
    [AttributeUsage(AttributeTargets.Class, Inherited=false)]
    public class HandleAsynchronizationAttribute : Attribute
    {

    }
}
