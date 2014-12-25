using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EApp.Common.Serialization
{
    public class ObjectXmlSerializer : IObjectSerializer
    {
        public byte[] Serialize(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            
            byte[] bytes = null;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.Serialize(memoryStream, obj);

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
            
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                try
                {
                    obj = serializer.Deserialize(memoryStream);

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
