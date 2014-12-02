using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Common.IOC
{
    public interface IObjectContainer 
    {
        /// <summary>
        /// Initializes the object container by using the application/web config file.
        /// </summary>
        /// <param name="configSectionName">The name of the ConfigurationSection in the application/web config file
        /// which is used for initializing the object container.</param>
        void InitializeFromConfigFile(string configSectionName);

        /// <summary>
        /// Gets the wrapped container instance.
        /// </summary>
        /// <typeparam name="T">The type of the wrapped container.</typeparam>
        /// <returns>The instance of the wrapped container.</returns>
        T GetWrapperContainer<T>();

        void RegisterType(Type t);

        void RegisterType(Type t, string name);

        void RegisterType(Type from, Type to, string name);

        void RegisterType<TFrom, TTo>(string name) where TTo : TFrom;

        T Resolve<T>(string name);

        object Resolve(Type t, string name);

        IEnumerable<object> ResolveAll(Type t);

        IEnumerable<T> ResolveAll<T>();

        bool Registered(Type t);

        bool Registered(Type t, string name);

        bool Registered<T>();

        bool Registered<T>(string name);
    }
}
