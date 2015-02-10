using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using EApp.Common.Serialization;
using EApp.Data.Configuration;
using EApp.Data.Mapping;

namespace EApp.Data.Mapping
{
    public sealed class MetaDataManager
    {
        private readonly static MetaDataManager instance = new MetaDataManager();

        private static Dictionary<string, EntityConfiguration> entityConfigs = new Dictionary<string, EntityConfiguration>();

        private readonly static object lockObject = new object();

        private MetaDataManager() { }

        static MetaDataManager() 
        {
            LoadEntityMappingConfiguration();
        }

        private static void LoadEntityMappingConfiguration() 
        {
            lock (lockObject)
            {
                EAppDataConfigurationSection configuration = (EAppDataConfigurationSection)ConfigurationManager.GetSection("EAppData");

                EntityMappingConfiguration entityMappingConfig = null;

                foreach (EntityMappingElement entityMappingItem in configuration.EntityMappings)
                {
                    string mappingName = entityMappingItem.Name;

                    string mappingFile = entityMappingItem.File;

                    using (FileStream mappingFileStream = new FileStream(mappingFile, FileMode.Open, FileAccess.Read))
                    {
                        using (MemoryStream mappingMemoryStream = new MemoryStream())
                        {
                            byte[] buffers = new byte[2048];

                            int position = mappingFileStream.Read(buffers, 0, buffers.Length);

                            mappingMemoryStream.Seek(0, SeekOrigin.Begin);

                            while (position > 0)
                            {
                                mappingMemoryStream.Write(buffers, 0, position);

                                position = mappingFileStream.Read(buffers, 0, buffers.Length);
                            }

                            mappingMemoryStream.Flush();

                            entityMappingConfig =
                                ObjectSerializerFactory.GetObjectSerializer("XML").
                                Deserialize<EntityMappingConfiguration>(mappingMemoryStream.ToArray());

                            mappingMemoryStream.Close();
                        }

                        mappingFileStream.Close();
                    }

                    if (entityMappingConfig.Entities != null &&
                        entityMappingConfig.Entities.Length > 0)
                    {
                        foreach (EntityConfiguration entityConfig in entityMappingConfig.Entities)
                        {
                            if (!entityConfigs.ContainsKey(entityConfig.Name))
                            {
                                entityConfigs.Add(entityConfig.Name, entityConfig);
                            }
                        }
                    }
                }
            }
        }

        public static MetaDataManager Instance 
        {
            get 
            {
                return instance;
            }
        }

        private bool ValidateEntityConfiguration()
        {
            if (entityConfigs != null &&
                entityConfigs.Count > 0)
            {
                return true;
            }

            return false;
        }

        public string ResolveTableName(string entityName)
        {
            if (ValidateEntityConfiguration())
            {
                if (entityConfigs.ContainsKey(entityName))
                {
                    string tableName = entityConfigs[entityName].TableName;

                    if (!string.IsNullOrEmpty(tableName) &&
                        !string.IsNullOrWhiteSpace(tableName))
                    {
                        return tableName;
                    }
                    else
                    {
                        return entityName;
                    }
                }
                else
                {
                    return entityName;
                }
            }
            else
            {
                return entityName;
            }
        }

        public string ResolveFieldName(string entityName, string propertyName)
        {
            if (ValidateEntityConfiguration())
            {
                if (entityConfigs.ContainsKey(entityName))
                {
                    var property = entityConfigs[entityName].Properties.FirstOrDefault(p => p.Name.Equals(propertyName));

                    if (property != null && 
                        !string.IsNullOrEmpty(property.ColumnName) &&
                        !string.IsNullOrWhiteSpace(property.ColumnName))
                    {
                        return property.ColumnName;
                    }
                    else
                    {
                        return propertyName;
                    }
                }
                else
                {
                    return propertyName;
                }
            }
            else
            {
                return propertyName;
            }
        }

        /// <summary>
        /// Resolves the table name by using the given type.
        /// </summary>
        /// <typeparam name="T">The type of the object to be resolved.</typeparam>
        /// <returns>The table name.</returns>
        public string ResolveTableName<T>() where T : class, new()
        {
            if (ValidateEntityConfiguration())
            {
                return this.ResolveTableName(typeof(T).Name);
            }
            else
            {
                return typeof(T).Name;
            }
        }

        /// <summary>
        /// Resolves the field name by using the given type and property name.
        /// </summary>
        /// <typeparam name="T">The type of the object to be resolved.</typeparam>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The field name.</returns>
        public string ResolveFieldName<T>(string propertyName) where T : class, new()
        {
            if (ValidateEntityConfiguration())
            {
                return this.ResolveFieldName(typeof(T).Name, propertyName);
            }
            else
            {
                return propertyName;
            }
        }

        public bool IsAutoIdentityField(string entityName, string propertyName)
        {
            if (ValidateEntityConfiguration())
            {
                if (entityConfigs.ContainsKey(entityName))
                {
                    var property = entityConfigs[entityName].Properties.FirstOrDefault(p => p.Name.Equals(propertyName));

                    if (property != null &&
                        !string.IsNullOrEmpty(property.ColumnName) &&
                        !string.IsNullOrWhiteSpace(property.ColumnName))
                    {
                        return property.IsAutoIdentity;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Checks if the given property is mapped to an auto-generated identity field.
        /// </summary>
        /// <typeparam name="T">The type of the object to be resolved.</typeparam>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the field is mapped to an auto-generated identity, otherwise false.</returns>
        public bool IsAutoIdentityField<T>(string propertyName) where T : class, new()
        {
            if (ValidateEntityConfiguration())
            {
                return this.IsAutoIdentityField(typeof(T).Name, propertyName);
            }
            else
            {
                return false;
            }
        }
    }
}
