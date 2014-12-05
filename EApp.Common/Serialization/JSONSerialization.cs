using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace EApp.Common.Serialization
{
    public class JSONSerialization
    {
        /// <summary>
        /// JSON序列化
        /// </summary>
        public static string JsonSerializer<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// JSON反序列化
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
        {
            return (T)JsonConvert.DeserializeObject(jsonString);
        }
    }
}
