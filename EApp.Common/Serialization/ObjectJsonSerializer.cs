using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;

namespace EApp.Common.Serialization
{
    public class ObjectJsonSerializer : IObjectSerializer
    {
        public byte[] Serialize(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(obj.GetType());

            byte[] bytes;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                jsonSerializer.WriteObject(memoryStream, obj);

                bytes = memoryStream.ToArray();

                memoryStream.Close();
            }

            return bytes;
        }

        public T Deserialize<T>(byte[] bytes)
        {
            object obj = default(T);

            if (bytes == null)
            {
                return (T)obj;
            }

            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                try
                {
                    obj = jsonSerializer.ReadObject(memoryStream);

                    return (T)obj;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    memoryStream.Close();
                }
            }
        }
    }
}
