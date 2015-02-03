using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace EApp.Data.Queries
{
    [Serializable()]
    [XmlRoot("ObjectMappingConfiguration")]
    public class ObjectMappingSchema
    {
        [XmlArray("entities")]
        [XmlArrayItem("entity")]
        public List<EntityConfiguration> Entities { get; set; }
    }

    [Serializable()]
    public class EntityConfiguration
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("table")]
        public string TableName { get; set; }

        [XmlAttribute("type")]
        public string TypeName { get; set; }

        [XmlArray("properties")]
        [XmlArrayItem("property")]
        public List<PropertyConfiguration> Properties { get; set; }
    }

    [Serializable()]
    public class PropertyConfiguration
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("type")]
        public string PropertyType { get; set; }

        [XmlAttribute("field")]
        public string FieldName { get; set; }

        [XmlAttribute("sqlType")]
        public string SqlType { get; set; }

        [XmlAttribute("isPrimaryKey")]
        public bool IsPrimaryKey { get; set; }

        [XmlAttribute("isAutoIdentity")]
        public bool IsAutoIdentity { get; set; }

        [XmlAttribute("isRelationKey")]
        public bool IsRelationKey  {get;set;}

        [XmlAttribute("relatedType")]
        public string RelatedType { get; set; }

        [XmlAttribute("relatedForeignKey")]
        public string RelatedForeignKey { get; set; }

        public DbType DbType
        {
            get
            {
                switch (SqlType.TrimStart().Split(' ', '(')[0].ToLower())
                {
                    case "bigint":
                        return System.Data.DbType.Int64;
                    case "int":
                        return System.Data.DbType.Int32;
                    case "smallint":
                        return System.Data.DbType.Int16;
                    case "tinyint":
                        return System.Data.DbType.Byte;
                    case "bit":
                        return System.Data.DbType.Boolean;
                    case "decimal":
                        return System.Data.DbType.Decimal;
                    case "numberic":
                        return System.Data.DbType.Decimal;
                    case "money":
                        return System.Data.DbType.Decimal;
                    case "smallmoney":
                        return System.Data.DbType.Decimal;
                    case "float":
                        return System.Data.DbType.Double;
                    case "real":
                        return System.Data.DbType.Double;
                    case "datetime":
                        return System.Data.DbType.DateTime;
                    case "smalldatetime":
                        return System.Data.DbType.DateTime;
                    case "timestamp":
                        return System.Data.DbType.DateTime;
                    case "char":
                        return System.Data.DbType.AnsiStringFixedLength;
                    case "varchar":
                        return System.Data.DbType.AnsiString;
                    case "text":
                        return System.Data.DbType.AnsiString;
                    case "nchar":
                        return System.Data.DbType.StringFixedLength;
                    case "nvarchar":
                        return System.Data.DbType.String;
                    case "ntext":
                        return System.Data.DbType.String;
                    case "binary":
                        return System.Data.DbType.Binary;
                    case "varbinary":
                        return System.Data.DbType.Binary;
                    case "image":
                        return System.Data.DbType.Binary;
                    case "uniqueidentifier":
                        return System.Data.DbType.Guid;
                }

                //should not reach here
                return System.Data.DbType.String;
            }
        }

        private string GetDefaultSqlType(Type type)
        {
            if (type.IsEnum)
            {
                return "int";
            }
            else if (type == typeof(long) || type == typeof(long?))
            {
                return "bigint";
            }
            else if (type == typeof(int) || type == typeof(int?))
            {
                return "int";
            }
            else if (type == typeof(short) || type == typeof(short?))
            {
                return "smallint";
            }
            else if (type == typeof(byte) || type == typeof(byte?))
            {
                return "tinyint";
            }
            else if (type == typeof(bool) || type == typeof(bool?))
            {
                return "bit";
            }
            else if (type == typeof(decimal) || type == typeof(decimal?))
            {
                return "decimal";
            }
            else if (type == typeof(float) || type == typeof(float?))
            {
                return "real";
            }
            else if (type == typeof(double) || type == typeof(double?))
            {
                return "float";
            }
            else if (type == typeof(string))
            {
                return "nvarchar(127)";
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                return "datetime";
            }
            else if (type == typeof(char) || type == typeof(char?))
            {
                return "nchar";
            }
            else if (type == typeof(string))
            {
                return "nvarchar(127)";
            }
            else if (type == typeof(byte[]))
            {
                return "image";
            }
            else if (type == typeof(Guid) || type == typeof(Guid?))
            {
                return "uniqueidentifier";
            }

            return "ntext";
        }

    }
}
