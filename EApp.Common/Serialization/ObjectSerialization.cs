using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace EApp.Common.Serialization
{
    public class ObjectSerialization
    {
        Stream stream;
        BinaryFormatter binFormatter;

        public ObjectSerialization(string fileName)
        {
            stream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            binFormatter = new BinaryFormatter();
        }

        public void Serialize<T>(T obj)
        {
            binFormatter.Serialize(stream, obj);
        }


        public T DeSerialize<T>()
        {
            return (T)binFormatter.Deserialize(stream);
        }
    }
}
