using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core;
using EApp.Core.Application;
using EApp.Core.Exceptions;

namespace EApp.Common.Serialization
{
    public class ObjectSerializerFactory
    {
        private static Dictionary<string, IObjectSerializer> objectSerializerDictionary = new Dictionary<string, IObjectSerializer>();

        private static readonly object lockObject = new object();

        public static IObjectSerializer GetObjectSerializer() 
        {
            string defaultObjectSerializerName = EAppRuntime.Instance.CurrentApp.ConfigSource.Config.SerializationFormats.Default;

            if (string.IsNullOrEmpty(defaultObjectSerializerName))
            {
                throw new ConfigException("The default Object Serializer provider has not been defined in the ConfigSource.");
            }

            return GetObjectSerializer(defaultObjectSerializerName);
        }

        public static IObjectSerializer GetObjectSerializer(string serializerName) 
        {
            string objectSerializerTypeName = EAppRuntime.Instance.CurrentApp.ConfigSource.Config.SerializationFormats[serializerName].Type;

            if (objectSerializerDictionary.ContainsKey(objectSerializerTypeName))
            {
                return (IObjectSerializer)objectSerializerDictionary[objectSerializerTypeName]; 
            }

            if (string.IsNullOrEmpty(objectSerializerTypeName))
            {
                throw new ConfigException("The Object Serializer provider has not been defined in the ConfigSource.");
            }

            Type objectSerializerType = Type.GetType(objectSerializerTypeName);

            if (objectSerializerType == null)
            {
                throw new InfrastructureException("The ObjectSerializer defined by type {0} doesn't exist.", objectSerializerTypeName);
            }

            if (!typeof(IObjectSerializer).IsAssignableFrom(objectSerializerType))
            {
                throw new ConfigException("Type '{0}' is not a Object Serializer.", objectSerializerType);
            }

            IObjectSerializer objectSerializer;

            lock (lockObject)
            {
                if (!objectSerializerDictionary.ContainsKey(objectSerializerTypeName))
                {
                    objectSerializer = (IObjectSerializer)EAppRuntime.Instance.CurrentApp.ObjectContainer.Resolve(objectSerializerType,
                                                                                                                  objectSerializerTypeName);

                    objectSerializerDictionary.Add(objectSerializerTypeName, objectSerializer);
                }
                else
                {
                    objectSerializer = (IObjectSerializer)objectSerializerDictionary[objectSerializerTypeName]; 
                }
            }

            return objectSerializer;
        }

        #region Reserved codes

        //private IObjectSerializer objectSerializer;

        //private static ObjectSerializerFactory defaultObjectSerializerFactory;

        //static ObjectSerializerFactory() 
        //{
        //    string defaultSerializationFormat = EAppRuntime.Instance.CurrentApp.ConfigSource.Config.SerializationFormats.Default;

        //    string objectSerializerTypeName = EAppRuntime.Instance.CurrentApp.ConfigSource.Config.SerializationFormats[defaultSerializationFormat].Type;

        //    if (string.IsNullOrEmpty(objectSerializerTypeName))
        //    {
 
        //    }

        //    Type objectSerializerType = Type.GetType(objectSerializerTypeName);

        //    if (objectSerializerType == null)
        //    { 
            
        //    }

        //    if (!typeof(IObjectSerializer).IsAssignableFrom(objectSerializerType))
        //    { 
            
        //    }

        //    IObjectSerializer defaultObjectSerializer = 
        //        (IObjectSerializer)EAppRuntime.Instance.CurrentApp.ObjectContainer.Resolve(objectSerializerType, 
        //                                                                                   objectSerializerTypeName);

        //    defaultObjectSerializerFactory = new ObjectSerializerFactory(defaultObjectSerializer);
        //}

        //public ObjectSerializerFactory(IObjectSerializer objectSerializer) 
        //{
        //    this.objectSerializer = objectSerializer;
        //}

        //public static ObjectSerializerFactory Default 
        //{
        //    get 
        //    {
        //        return defaultObjectSerializerFactory;
        //    }
        //}

        //public byte[] Serialize(object obj) 
        //{
        //    return this.objectSerializer.Serialize(obj);
        //}

        //public T Deserialize<T>(byte[] bytes)
        //{
        //    return this.objectSerializer.Deserialize<T>(bytes);
        //}

        #endregion
    }
}
