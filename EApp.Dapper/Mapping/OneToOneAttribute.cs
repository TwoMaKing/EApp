using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Dapper.Mapping
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class OneToOneAttribute : AssociationAttribute
    {

    }
}
