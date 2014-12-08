using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Common.Serialization
{
    public interface ISerialization
    {
        byte[] Serialize<T>(T obj);

        T DeSerialize<T>(string str);

        T DeSerialize<T>(byte[] bytes);
    }
}
