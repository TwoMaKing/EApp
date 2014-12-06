using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace EApp.Common.Serialization
{
    public class UnicodeSerialization : ISerialization
    {
        public byte[] Serialize<T>(T obj)
        {
            byte[] result = default(byte[]);
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    BinaryFormatter binFormatter = new BinaryFormatter();
                    binFormatter.Serialize(ms, obj);
                    result = ms.ToArray();
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }


        public T DeSerialize<T>(byte[] bytes)
        {
            T result = default(T);
            try
            {
                if (bytes == null)
                    return default(T);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    BinaryFormatter binFormatter = new BinaryFormatter();
                    result = (T)binFormatter.Deserialize(ms);
                }
            }
            catch (Exception ex)
            { 
            }
            return result;
        }



        public T DeSerialize<T>(string str)
        {
            throw new NotImplementedException();
        }
    }
}
