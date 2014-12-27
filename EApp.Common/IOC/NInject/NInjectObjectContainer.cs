using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Common.Exceptions;
using EApp.Core;
using Ninject;
using Ninject.Syntax;

namespace EApp.Common.IOC
{
    public class NInjectObjectContainer : IObjectContainer
    {
        private IKernel kernel;

        public NInjectObjectContainer()
        {
            this.kernel = new StandardKernel();
        }

        public void InitializeFromConfigFile(string configSectionName)
        {
            return;
        }

        public T GetWrapperContainer<T>()
        {
            if (typeof(T).Equals(this.kernel))
            {
                return (T)this.kernel;
            }

            throw new InfrastructureException("The wrapped container type provided by the current object container should be '{0}'.", typeof(StandardKernel));
        }

        public void RegisterType(Type t)
        {
            this.RegisterType(t, ObjectLifetimeMode.Singleton);
        }

        public void RegisterType(Type t, ObjectLifetimeMode lifeitme)
        {
            IBindingWhenInNamedWithOrOnSyntax<object> bindingInSyntax = this.kernel.Bind(t).ToSelf();

            this.SetLifetimeManager(bindingInSyntax, lifeitme);
        }

        public void RegisterType(Type t, string name)
        {
            this.RegisterType(t, name, ObjectLifetimeMode.Singleton);
        }

        public void RegisterType(Type t, string name, ObjectLifetimeMode lifeitme)
        {
            IBindingWhenInNamedWithOrOnSyntax<object> bindingInSyntax = this.kernel.Bind(t).ToSelf();

            bindingInSyntax.Named(name);

            this.SetLifetimeManager(bindingInSyntax, lifeitme);
        }

        public void RegisterType(Type from, Type to, string name)
        {
            this.RegisterType(from, to, name, ObjectLifetimeMode.Singleton);
        }

        public void RegisterType(Type from, Type to, string name, ObjectLifetimeMode lifeitme)
        {
            IBindingWhenInNamedWithOrOnSyntax<object> bindingInSyntax = this.kernel.Bind(from).To(to);

            bindingInSyntax.Named(name);

            this.SetLifetimeManager(bindingInSyntax, lifeitme);
        }

        public void RegisterType<TFrom>(Type to, string name)
        {
            this.RegisterType<TFrom>(to, name, ObjectLifetimeMode.WeakReferenceRequest);
        }

        public void RegisterType<TFrom>(Type to, string name, ObjectLifetimeMode lifeitme)
        {
            IBindingWhenInNamedWithOrOnSyntax<TFrom> bindingInSyntax = this.kernel.Bind<TFrom>().To(to);

            bindingInSyntax.Named(name);

            this.SetLifetimeManager(bindingInSyntax, lifeitme);
        }

        public void RegisterType<TFrom, TTo>(string name) where TTo : TFrom
        {
            this.RegisterType<TFrom, TTo>(name, ObjectLifetimeMode.Singleton);
        }

        public void RegisterType<TFrom, TTo>(string name, ObjectLifetimeMode lifeitme) where TTo : TFrom
        {
            IBindingWhenInNamedWithOrOnSyntax<TFrom> bindingInSyntax = this.kernel.Bind<TFrom>().To<TTo>();

            bindingInSyntax.Named(name);

            this.SetLifetimeManager(bindingInSyntax, lifeitme);
        }

        public T Resolve<T>()
        {
            return this.kernel.TryGet<T>();
        }

        public T Resolve<T>(string name)
        {
            return this.kernel.TryGet<T>(name);
        }

        public object Resolve(Type t)
        {
            return this.kernel.TryGet(t);
        }

        public object Resolve(Type t, string name)
        {
            return this.kernel.TryGet(t, name);
        }

        public IEnumerable<object> ResolveAll(Type t)
        {
            return this.kernel.GetAll(t);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return this.kernel.GetAll<T>();
        }

        public bool Registered(Type t)
        {
            return false;
        }

        public bool Registered(Type t, string name)
        {
            return false;
        }

        public bool Registered<T>()
        {
            return false;
        }

        public bool Registered<T>(string name)
        {
            return false;
        }

        private void SetLifetimeManager<TObject>(IBindingWhenInNamedWithOrOnSyntax<TObject> bindingInSyntax, ObjectLifetimeMode lifeitme) 
        {
            if (lifeitme == ObjectLifetimeMode.Transient)
            {
                bindingInSyntax.InTransientScope();
            }
            else if (lifeitme == ObjectLifetimeMode.Singleton)
            {
                bindingInSyntax.InSingletonScope();
            }
            else if (lifeitme == ObjectLifetimeMode.WeakReferenceRequest)
            {
                bindingInSyntax.InRequestScope();
            }
            else
            {
                bindingInSyntax.InThreadScope();
            }
        }
    }
}
