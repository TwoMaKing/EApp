
using System;
using System.IO;
using System.Linq;
using EApp.Common.Serialization;

namespace EApp.Data.Mapping
{
    /// <summary>
    /// Represents the XML storage mapping resolver.
    /// </summary>
    public class XmlObjectMappingResolver : IObjectMappingResolver
    {
        #region Private Fields
        private readonly string fileName;
        private readonly IObjectSerializer serializer = new ObjectXmlSerializer();
        private readonly EntityMappingConfiguration entityMappingConfiguration;
        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of <c>XmlStorageMappingResolver</c> class.
        /// </summary>
        /// <param name="fileName">The file name of the external XML mapping file.</param>
        public XmlObjectMappingResolver(string fileName)
        {
            this.fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            using (FileStream fileStream = new FileStream(this.fileName, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, Convert.ToInt32(fileStream.Length));
                this.entityMappingConfiguration = serializer.Deserialize<EntityMappingConfiguration>(bytes);
                fileStream.Close();
            }
        }

        /// <summary>
        ///  Initializes a new instance of <c>XmlStorageMappingResolver</c> class.
        /// </summary>
        /// <param name="entityMappingConfiguration">The instance of EntityMappingConfiguration</param>
        public XmlObjectMappingResolver(EntityMappingConfiguration entityMappingConfiguration)
        {
            this.entityMappingConfiguration = entityMappingConfiguration;
        }

        #endregion

        #region Private Methods
        private bool ValidateSchema()
        {
            if (entityMappingConfiguration != null &&
                entityMappingConfiguration.Entities != null &&
                entityMappingConfiguration.Entities.Length > 0)
                return true;
            return false;
        }
        #endregion

        #region IStorageMappingResolver Members

        public string ResolveTableName(string objectOrTypeName)
        {
            if (ValidateSchema())
            {
                var entityConfiguration = entityMappingConfiguration.Entities.FirstOrDefault(p => p.TypeName.Equals(objectOrTypeName) || p.Name.Equals(objectOrTypeName));
                if (entityConfiguration != null && !string.IsNullOrEmpty(entityConfiguration.TableName))
                    return entityConfiguration.TableName;
                else
                    return objectOrTypeName;
            }
            else
                return objectOrTypeName;
        }

        public string ResolveFieldName(string objectOrTypeName, string propertyName)
        {
            if (ValidateSchema())
            {
                var dataType = entityMappingConfiguration.Entities.FirstOrDefault(p => p.TypeName.Equals(objectOrTypeName) || p.Name.Equals(objectOrTypeName));
                if (dataType != null)
                {
                    if (dataType.Properties != null && dataType.Properties.Length > 0)
                    {
                        var property = dataType.Properties.FirstOrDefault(p => p.Name.Equals(propertyName));
                        if (property != null && !string.IsNullOrEmpty(property.ColumnName))
                            return property.ColumnName;
                        else
                            return propertyName;
                    }
                    else
                        return propertyName;
                }
                else
                    return propertyName;
            }
            else
                return propertyName;
        }

        /// <summary>
        /// Resolves the table name by using the given type.
        /// </summary>
        /// <typeparam name="T">The type of the object to be resolved.</typeparam>
        /// <returns>The table name.</returns>
        public string ResolveTableName<T>() where T : class, new()
        {
            if (ValidateSchema())
            {
                var entityConfiguration = entityMappingConfiguration.Entities.FirstOrDefault(p => p.TypeName.Equals(typeof(T).FullName));
                if (entityConfiguration != null && !string.IsNullOrEmpty(entityConfiguration.TableName))
                    return entityConfiguration.TableName;
                else
                    return typeof(T).Name;
            }
            else
                return typeof(T).Name;
        }
        /// <summary>
        /// Resolves the field name by using the given type and property name.
        /// </summary>
        /// <typeparam name="T">The type of the object to be resolved.</typeparam>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The field name.</returns>
        public string ResolveFieldName<T>(string propertyName) where T : class, new()
        {
            if (ValidateSchema())
            {
                var dataType = entityMappingConfiguration.Entities.FirstOrDefault(p => p.TypeName.Equals(typeof(T).FullName));
                if (dataType != null)
                {
                    if (dataType.Properties != null && dataType.Properties.Length > 0)
                    {
                        var property = dataType.Properties.FirstOrDefault(p => p.Name.Equals(propertyName));
                        if (property != null && !string.IsNullOrEmpty(property.ColumnName))
                            return property.ColumnName;
                        else
                            return propertyName;
                    }
                    else
                        return propertyName;
                }
                else
                    return propertyName;
            }
            else
                return propertyName;
        }
        /// <summary>
        /// Checks if the given property is mapped to an auto-generated identity field.
        /// </summary>
        /// <typeparam name="T">The type of the object to be resolved.</typeparam>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the field is mapped to an auto-generated identity, otherwise false.</returns>
        public bool IsAutoIdentityField<T>(string propertyName) where T : class, new()
        {
            if (ValidateSchema())
            {
                var dataType = entityMappingConfiguration.Entities.FirstOrDefault(p => p.TypeName.Equals(typeof(T).FullName));
                if (dataType != null)
                {
                    if (dataType.Properties != null && dataType.Properties.Length > 0)
                    {
                        var property = dataType.Properties.FirstOrDefault(p => p.Name.Equals(propertyName));
                        if (property != null)
                            return property.IsAutoIdentity;
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }
        #endregion

    }
}
