using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Common.Serialization;

namespace EApp.Common.Compression
{
    public interface ICompression
    {
        byte[] Zip(object obj);
        
        byte[] Zip<T>(T obj);

        object Unzip(byte[] bytes);

        T Unzip<T>(byte[] bytes);
    }
}
