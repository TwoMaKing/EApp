using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace EApp.Common.Serialization
{
    public class XMLSerialization : ISerialization
    {
        public byte[] Serialize<T>(T obj)
        {
            byte[] result = default(byte[]);
            try
            {
                if (obj == null)
                    return result;
                result = default(byte[]);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                XmlSerializerNamespaces nameSpaces = new XmlSerializerNamespaces();
                nameSpaces.Add(string.Empty, string.Empty);
                using (MemoryStream ms = new MemoryStream())
                {
                    xmlSerializer.Serialize((Stream)ms, obj, nameSpaces);
                    result = ms.GetBuffer();
                }
                return result;
            }
            catch (Exception ex)
            {
            }
            return default(byte[]);
        }

        public T DeSerialize<T>(byte[] bytes)
        {
            T result = default(T);
            try
            {
                if (bytes == null)
                    return result;
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    result = (T)xmlSerializer.Deserialize((Stream)ms);
                }
                return result;
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public T DeSerialize<T>(string xmlString)
        {
            T result = default(T);
            try
            {
                if (string.IsNullOrEmpty(xmlString))
                    return result;
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                using (Stream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
                {
                    result = (T)xmlSerializer.Deserialize(xmlStream);
                }
                return result;
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
