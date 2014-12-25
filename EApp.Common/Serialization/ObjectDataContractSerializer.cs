using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace EApp.Common.Serialization
{
    public class ObjectDataContractSerializer : IObjectSerializer
    {
        public byte[] Serialize(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            DataContractSerializer dataContractSerializer = new DataContractSerializer(obj.GetType());

            byte[] bytes;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                dataContractSerializer.WriteObject(memoryStream, obj);

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

            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T));

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                try
                {
                    obj = dataContractSerializer.ReadObject(memoryStream);

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
