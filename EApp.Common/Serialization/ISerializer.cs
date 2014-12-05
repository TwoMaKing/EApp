using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Common.Serialization
{
    public interface ISerializer
    {
        string Serialize(object obj);

        object DeSerialize(Type returnType,string str);
    }
}
