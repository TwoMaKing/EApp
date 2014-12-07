using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace EApp.Common.Serialization
{
    public class DataContractSerialization : ISerialization
    {
        public byte[] Serialize<T>(T obj)
        {
            if (obj == null)
                return default(byte[]);
            MemoryStream ms = new MemoryStream();
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            serializer.WriteObject(ms, obj);
            byte[] result = ms.ToArray();
            ms.Close();
            ms.Dispose();
            return result;
        }

        public T DeSerialize<T>(string str)
        {
            throw new NotImplementedException();
        }

        public T DeSerialize<T>(byte[] bytes)
        {
            if (bytes == default(byte[]))
                return default(T);
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(bytes);
            T result = (T)serializer.ReadObject(ms);
            return result;
        }
    }
}
