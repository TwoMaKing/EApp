using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using EApp.Common.Exceptions;
using EApp.Core;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace EApp.Common.IoC
{
    public class UnityObjectContainer : IObjectContainer
    {
        private IUnityContainer unityContainer;

        public UnityObjectContainer()
        {
            this.unityContainer = new UnityContainer();
        }

        public void InitializeFromConfigFile(string configSectionName)
        {
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection(configSectionName);
            
            section.Configure(this.unityContainer);
        }

        public T GetWrapperContainer<T>()
        {
            if (typeof(T).Equals(this.unityContainer.GetType()) ||
                typeof(T).IsAssignableFrom(this.unityContainer.GetType()))
            {
                return (T)this.unityContainer;
            }

            throw new InfrastructureException("The wrapped container type provided by the current object container should be '{0}'.", typeof(UnityContainer));
        }

        public void RegisterType(Type t)
        {
            this.RegisterType(t, ObjectLifetimeMode.WeakReferenceRequest);
        }

        public void RegisterType(Type t, ObjectLifetimeMode lifeitme)
        {
            this.unityContainer.RegisterType(t, this.GetLifetimeManager(lifeitme));
        }

        public void RegisterType(Type t, string name)
        {
            this.RegisterType(t, name, ObjectLifetimeMode.WeakReferenceRequest);
        }

        public void RegisterType(Type t, string name, ObjectLifetimeMode lifeitme)
        {
            this.unityContainer.RegisterType(t, name, this.GetLifetimeManager(lifeitme));
        }

        public void RegisterType(Type from, Type to, string name)
        {
            this.RegisterType(from, to, name, ObjectLifetimeMode.WeakReferenceRequest);
        }

        public void RegisterType(Type from, Type to, string name, ObjectLifetimeMode lifeitme)
        {
            this.unityContainer.RegisterType(from, to, name, this.GetLifetimeManager(lifeitme));
        }

        public void RegisterType<TFrom>(Type to, string name)
        {
            this.RegisterType<TFrom>(to, name, ObjectLifetimeMode.WeakReferenceRequest);
        }

        public void RegisterType<TFrom>(Type to, string name, ObjectLifetimeMode lifeitme)
        {
            this.unityContainer.RegisterType(typeof(TFrom), to, name, this.GetLifetimeManager(lifeitme));
        }

        public void RegisterType<TFrom, TTo>(string name) where TTo : TFrom
        {
            this.RegisterType<TFrom, TTo>(name, ObjectLifetimeMode.WeakReferenceRequest);
        }

        public void RegisterType<TFrom, TTo>(string name, ObjectLifetimeMode lifeitme) where TTo : TFrom
        {
            this.unityContainer.RegisterType<TFrom, TTo>(name, this.GetLifetimeManager(lifeitme));
        }

        public T Resolve<T>()
        {
            return this.unityContainer.Resolve<T>();
        }

        public T Resolve<T>(string name)
        {
            return this.unityContainer.Resolve<T>(name);
        }

        public object Resolve(Type t)
        {
            return this.unityContainer.Resolve(t);
        }

        public object Resolve(Type t, string name)
        {
            return this.unityContainer.Resolve(t, name);
        }

        public bool Registered(Type t)
        {
            return this.unityContainer.IsRegistered(t);
        }

        public IEnumerable<object> ResolveAll(Type t)
        {
            return this.unityContainer.ResolveAll(t);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return this.unityContainer.ResolveAll<T>();
        }

        public bool Registered(Type t, string name)
        {
            return this.unityContainer.IsRegistered(t, name);
        }

        public bool Registered<T>()
        {
            return this.unityContainer.IsRegistered<T>();
        }

        public bool Registered<T>(string name)
        {
            return this.unityContainer.IsRegistered<T>(name);
        }

        private LifetimeManager GetLifetimeManager(ObjectLifetimeMode lifeitme)
        { 
            if (lifeitme == ObjectLifetimeMode.Transient)
            {
                return new TransientLifetimeManager();
            }
            else if (lifeitme == ObjectLifetimeMode.Singleton)
            {
                return new ContainerControlledLifetimeManager();
            }
            else if (lifeitme == ObjectLifetimeMode.WeakReferenceRequest)
            {
                return new ExternallyControlledLifetimeManager();
            }
            else
            {
                return new PerThreadLifetimeManager();
            }
        }

        public IEnumerable<Type> TypesFrom
        {
            get 
            {
                List<Type> typesFrom = new List<Type>();

                foreach (var registerItem in this.unityContainer.Registrations)
                {
                    typesFrom.Add(registerItem.RegisteredType);
                }

                return typesFrom;
            }
        }

        public IEnumerable<Type> TypesMapTo
        {
            get 
            {
                List<Type> typesMapTo = new List<Type>();

                foreach (var registerItem in this.unityContainer.Registrations)
                {
                    typesMapTo.Add(registerItem.MappedToType);
                }

                return typesMapTo;
            }
        }
    }
}
