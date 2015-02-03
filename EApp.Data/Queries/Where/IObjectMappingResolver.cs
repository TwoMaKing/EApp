
namespace EApp.Data.Queries
{
    /// <summary>
    /// Represents that the implemented classes are storage mapping resolvers.
    /// </summary>
    public interface IObjectMappingResolver
    {
        /// <summary>
        /// Resolves the table name by using the given type.
        /// </summary>
        /// <typeparam name="T">The type of the object to be resolved.</typeparam>
        /// <returns>The table name.</returns>
        string ResolveTableName<T>()
            where T : class, new();
        /// <summary>
        /// Resolves the field name by using the given type and property name.
        /// </summary>
        /// <typeparam name="T">The type of the object to be resolved.</typeparam>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The field name.</returns>
        string ResolveFieldName<T>(string propertyName)
            where T : class, new();
        /// <summary>
        /// Checks if the given property is mapped to an auto-generated identity field.
        /// </summary>
        /// <typeparam name="T">The type of the object to be resolved.</typeparam>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the field is mapped to an auto-generated identity, otherwise false.</returns>
        bool IsAutoIdentityField<T>(string propertyName)
            where T : class, new();
    }
}
