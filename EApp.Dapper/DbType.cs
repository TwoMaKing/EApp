using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EApp.Dapper
{
    /// <summary>
    /// DBTypes
    /// </summary>
    public enum DBType
    {
        /// <summary>
        /// 
        /// </summary>
        Unkonw = SqlDbType.Variant,
        /// <summary>
        ///   <see cref="T:System.Int64" />. A 64-bit signed integer.
        ///           </summary>
        Int64 = SqlDbType.BigInt,
        /// <summary>
        ///   <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />. A fixed-length stream of binary data ranging between 1 and 8,000 bytes.
        ///           </summary>
        Binary = SqlDbType.Binary,
        /// <summary>
        ///   <see cref="T:System.Boolean" />. An unsigned numeric value that can be 0, 1, or null. 
        ///           </summary>
        Boolean = SqlDbType.Bit,
        /// <summary>
        ///   <see cref="T:System.String" />. A fixed-length stream of non-Unicode characters ranging between 1 and 8,000 characters.
        ///           </summary>
        Char = SqlDbType.Char,
        /// <summary>
        ///   <see cref="T:System.DateTime" />. Date and time data ranging in value from January 1, 1753 to December 31, 9999 to an accuracy of 3.33 milliseconds.
        ///           </summary>
        DateTime = SqlDbType.DateTime,
        /// <summary>
        ///   <see cref="T:System.Decimal" />. A fixed precision and scale numeric value between -10 38 -1 and 10 38 -1.
        ///           </summary>
        Decimal = SqlDbType.Decimal,
        /// <summary>
        ///   <see cref="T:System.Double" />. A floating point number within the range of -1.79E +308 through 1.79E +308.
        ///           </summary>
        Double = SqlDbType.Float,
        /// <summary>
        ///   <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />. A variable-length stream of binary data ranging from 0 to 2 31 -1 (or 2,147,483,647) bytes.
        ///           </summary>
        Image = SqlDbType.Image,
        /// <summary>
        ///   <see cref="T:System.Int32" />. A 32-bit signed integer.
        ///           </summary>
        Int32 = SqlDbType.Int,
        /// <summary>
        ///   <see cref="T:System.Decimal" />. A currency value ranging from -2 63 (or -9,223,372,036,854,775,808) to 2 63 -1 (or +9,223,372,036,854,775,807) with an accuracy to a ten-thousandth of a currency unit.
        ///           </summary>
        Currency = SqlDbType.Money,
        /// <summary>
        ///   <see cref="T:System.String" />. A fixed-length stream of Unicode characters ranging between 1 and 4,000 characters.
        ///           </summary>
        NChar = SqlDbType.NChar,
        /// <summary>
        ///   <see cref="T:System.String" />. A variable-length stream of Unicode data with a maximum length of 2 30 - 1 (or 1,073,741,823) characters.
        ///           </summary>
        NText = SqlDbType.NText
    ,
        /// <summary>
        ///   <see cref="T:System.String" />. A variable-length stream of Unicode characters ranging between 1 and 4,000 characters. Implicit conversion fails if the string is greater than 4,000 characters. Explicitly set the object when working with strings longer than 4,000 characters.
        ///           </summary>
        NVarChar = SqlDbType.NVarChar,
        /// <summary>
        ///   <see cref="T:System.Single" />. A floating point number within the range of -3.40E +38 through 3.40E +38.
        ///           </summary>
        Single = SqlDbType.Real,
        /// <summary>
        ///   <see cref="T:System.Guid" />. A globally unique identifier (or GUID).
        ///           </summary>
        Guid = SqlDbType.UniqueIdentifier,
        /// <summary>
        ///   <see cref="T:System.Int16" />. A 16-bit signed integer.
        ///           </summary>
        Int16 = SqlDbType.SmallInt,
        /// <summary>
        ///   <see cref="T:System.String" />. A variable-length stream of non-Unicode data with a maximum length of 2 31 -1 (or 2,147,483,647) characters.
        ///           </summary>
        Text = SqlDbType.Text,
        ///// <summary>
        /////   <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />. Automatically generated binary numbers, which are guaranteed to be unique within a database. timestamp is used typically as a mechanism for version-stamping table rows. The storage size is 8 bytes.
        /////           </summary>
        //Timestamp=SqlDbType.Timestamp,
        /// <summary>
        ///   <see cref="T:System.Byte" />. An 8-bit unsigned integer.
        ///           </summary>
        Byte = SqlDbType.TinyInt,
        ///// <summary>
        /////   <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />. A variable-length stream of binary data ranging between 1 and 8,000 bytes. Implicit conversion fails if the byte array is greater than 8,000 bytes. Explicitly set the object when working with byte arrays larger than 8,000 bytes.
        /////           </summary>
        //VarBinary=SqlDbType.VarBinary,
        /// <summary>
        ///   <see cref="T:System.String" />. A variable-length stream of non-Unicode characters ranging between 1 and 8,000 characters.
        ///           </summary>
        VarChar = SqlDbType.VarChar,
    }
}
