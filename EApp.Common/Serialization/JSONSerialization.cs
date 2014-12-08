using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace EApp.Common.Serialization
{
    public class JSONSerialization : ISerialization
    {
        public byte[] Serialize<T>(T obj)
        {
            if (obj == null)
                return default(byte[]);
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
        }

        public T DeSerialize<T>(string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
                return default(T);
            return (T)JsonConvert.DeserializeObject(jsonString);
        }


        public T DeSerialize<T>(byte[] bytes)
        {
            if (bytes == default(byte[]))
                return default(T);
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(bytes));
        }
    }
}
