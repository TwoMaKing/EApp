using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace EApp.Common.Serialization
{
    public class XMLSerialization
    {
        /// <summary>
        /// 将对象序列化成XML字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string Serialize<T>(T obj)
        {
            try
            {
                string xmlString = string.Empty;
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add(string.Empty, string.Empty);
                using (MemoryStream ms = new MemoryStream())
                {
                    xmlSerializer.Serialize(ms, obj,namespaces);
                    xmlString = Encoding.UTF8.GetString(ms.ToArray());
                }
                return xmlString;
            }
            catch (Exception ex)
            {
            }
            return "";
        }

        /// <summary>
        /// 将XML字符串反序列成对象
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="xmlString">要序列化的字符串</param>
        /// <returns></returns>
        public T Deserialize<T>(string xmlString)
        {
            try
            {
                T t = default(T);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                using (Stream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
                {
                    using (XmlReader xmlReader = XmlReader.Create(xmlStream))
                    {
                        Object obj = xmlSerializer.Deserialize(xmlReader);
                        t = (T)obj;
                    }
                }
                return t;
            }
            catch (Exception ex)
            {
                
            }
            return default(T);
        }

        /// <summary>
        /// 将对象序列化成xml文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="fileName"></param>
        public void SerializeToFile<T>(T obj, string fileName)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                StreamWriter streamWriter = new StreamWriter(fileName);
                xmlSerializer.Serialize((TextWriter)streamWriter, obj);
                streamWriter.Close();
                streamWriter.Dispose();
            }
            catch (Exception ex)
            { 
            
            }
        }

        /// <summary>
        /// 从文件反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public T DeSerializeFromFile<T>(string fileName)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                FileStream fileStream = new FileStream(fileName, FileMode.Open);
                object obj = xmlSerializer.Deserialize((Stream)fileStream);
                fileStream.Close();
                fileStream.Dispose();
                return (T)obj;
            }
            catch (Exception ex)
            {
            
            }
            return default(T);
        }
    }
}
