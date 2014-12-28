using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace EApp.Common.Serialization
{
    public class ObjectBinarySerializer : IObjectSerializer
    {
        public byte[] Serialize(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();

            byte[] bytes = null;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, obj);

                bytes = memoryStream.ToArray();

                memoryStream.Close();
            }

            return bytes;
        }

        public T Deserialize<T>(byte[] bytes)
        {
            if (bytes == null)
            {
                return default(T);
            }

            object obj = default(T);

            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                try
                {
                    obj = binaryFormatter.Deserialize(memoryStream);

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
