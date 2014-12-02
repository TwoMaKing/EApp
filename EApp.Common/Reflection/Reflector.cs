using System;
using System.Collections.Generic;
using System.Reflection;
using EApp.Common.Exceptions;

namespace EApp.Common.Reflection
{
    /// <summary>
    ///Reflection Helper
    /// </summary>
    /// <remarks></remarks>
    public class Reflector
    {
        private Assembly assembly;
        private Type objectType;
        private object objectInstance;

        #region Elements collection in object

        //Elements collection in object
        private Dictionary<string, MemberInfo> memberInfoCollection = new Dictionary<string, MemberInfo>();
        private Dictionary<string, PropertyInfo> propertyInfoCollection;
        private Dictionary<string, MethodInfo> methodInfoCollection;
        private Dictionary<string, FieldInfo> fieldInfoCollection;
        private Dictionary<string, EventInfo> eventInfoCollection = null;

        //name collection
        private List<string> propertyNameCollection = null;
        private List<string> fieldNameCollection = null;
        private List<string> methodNameCollection = null;

        #endregion

        #region Private methods for constructor

        private void InitializeAssembly(string assemblyName)
        {
            this.assembly = Assembly.Load(assemblyName);
            if (this.assembly == null) throw new LoadAssemblyException();
            this.objectType = this.assembly.GetType();
            this.InitializeObjectInstance();
            this.InitializeObjectReflectionInformation();
        }

        private void InitializeAssembly(string assemblyName, object[] args)
        {
            this.assembly = Assembly.Load(assemblyName);
            if (this.assembly == null) throw new LoadAssemblyException();
            this.objectType = this.assembly.GetType();
            this.InitializeObjectInstance(args);
            this.InitializeObjectReflectionInformation();
        }

        private void InitializeAssembly(string assemblyFile, string typeName)
        {
            this.assembly = Assembly.LoadFile(assemblyFile);
            if (this.assembly == null) throw new LoadAssemblyException();
            this.objectType = this.assembly.GetType(typeName);
            this.InitializeObjectInstance();
            this.InitializeObjectReflectionInformation();
        }

        private void InitializeAssembly(string assemblyFile, string typeName, object[] args)
        {
            this.assembly = Assembly.LoadFile(assemblyFile);
            if (this.assembly == null) throw new LoadAssemblyException();
            this.objectType = this.assembly.GetType(typeName);
            this.InitializeObjectInstance(args);
            this.InitializeObjectReflectionInformation();
        }

        private void InitializeAssembly(Type objType)
        {
            this.objectType = objType;
            if (this.objectType != null)
            {
                this.InitializeObjectInstance();
                this.InitializeObjectReflectionInformation();
            }
        }

        private void InitializeAssembly(Type objType, object[] args)
        {
            this.objectType = objType;
            if (this.objectType != null)
            {
                this.InitializeObjectInstance(args);
                this.InitializeObjectReflectionInformation();
            }
        }

        private void InitializeAssembly(object relObj)
        {
            this.objectType = relObj.GetType();
            this.objectInstance = relObj;
            this.InitializeObjectReflectionInformation();
        }

        #endregion

        #region Constructor
        public Reflector(string assemblyName)
        {
            this.InitializeAssembly(assemblyName);
        }

        public Reflector(string assemblyName, object[] args)
        {
            this.InitializeAssembly(assemblyName, args);
        }

        public Reflector(string assemblyFile, string typeName)
        {
            this.InitializeAssembly(assemblyFile, typeName);
        }

        public Reflector(string assemblyFile, string typeName, object[] args)
        {
            this.InitializeAssembly(assemblyFile, typeName, args);
        }

        public Reflector(Type objType)
        {
            this.InitializeAssembly(objType);
        }

        public Reflector(Type objType, object[] args)
        {
            this.InitializeAssembly(objType, args);
        }

        public Reflector(object relObj)
        {
            this.InitializeAssembly(relObj);
        }


        #endregion

        public Dictionary<string, MemberInfo> ObjectMembers
        {
            get
            {
                if (this.memberInfoCollection == null)
                {
                    this.propertyInfoCollection = this.InitializeProperties();
                    this.methodInfoCollection = this.InitializeMethods();
                    this.fieldInfoCollection = this.InitializeFields();
                }
                return this.memberInfoCollection;
            }
        }

        public Dictionary<string, PropertyInfo> ObjectProperties
        {
            get
            {
                if (this.propertyInfoCollection == null)
                {
                    this.propertyInfoCollection = this.InitializeProperties();
                }
                return this.propertyInfoCollection;
            }
        }

        public Dictionary<string, MethodInfo> ObjectMethods
        {
            get
            {
                if (this.methodInfoCollection == null)
                {
                    this.methodInfoCollection = this.InitializeMethods();
                }
                return this.methodInfoCollection;
            }
        }

        public Dictionary<string, FieldInfo> ObjectFields
        {
            get
            {
                if (this.fieldInfoCollection == null)
                {
                    this.fieldInfoCollection = this.InitializeFields();
                }
                return this.fieldInfoCollection;
            }
        }

        public List<string> PropertyNames
        {
            get
            {
                if (this.propertyNameCollection == null)
                {
                    this.propertyInfoCollection = this.InitializeProperties();
                }
                return this.propertyNameCollection;
            }
        }

        public List<string> FieldNames
        {
            get
            {
                if (this.fieldInfoCollection == null)
                {
                    this.fieldInfoCollection = this.InitializeFields();
                }
                return this.fieldNameCollection;
            }
        }

        public List<string> MethodNames
        {
            get
            {
                if (this.methodInfoCollection == null)
                {
                    this.methodInfoCollection = this.InitializeMethods();
                }
                return this.methodNameCollection;
            }
        }

        public bool ExistAttribute(string memberName, Type attributeType)
        {
            return this.memberInfoCollection[memberName].GetCustomAttributes(attributeType, true).Length > 0;
        }

        public bool SetPropertyValue(string propertyName, object value)
        {
            return this.DoSetPropertyValue(propertyName, value);
        }

        public object GetPropertyValue(string propertyName)
        {
            return this.DoGetPropertyValue(propertyName);
        }

        public bool SetFieldValue(string fieldName, object value)
        {
            return this.DoSetFieldValue(fieldName, value);
        }

        public object GetFieldValue(string fieldName)
        {
            return this.DoGetFieldValue(fieldName);
        }

        public object ExecuteMethod(string methodName, object[] args)
        {
            return this.DoExecuteMethod(methodName, args);
        }

        public List<Attribute> GetMemberAttributes(string memberName)
        {
            return DoGetMemberAttribute(memberName);
        }

        public Attribute GetMemberAttribute(string memberName, Type attributeType)
        {
            return (Attribute)this.memberInfoCollection[memberName].GetCustomAttributes(attributeType, true)[0];
        }

        public object GetMemberAttributeValue(string memberName, Type attributetype, string propertyName)
        {
            Attribute attr = (Attribute)this.memberInfoCollection[memberName].GetCustomAttributes(attributetype, true)[0];
            return attr.GetType().GetProperty(propertyName).GetValue(attr, null);
        }

        protected virtual bool DoSetFieldValue(string fieldName, object value)
        {
            if (this.fieldInfoCollection == null) return false;
            if (!this.fieldInfoCollection.ContainsKey(fieldName)) return false;
            try
            {
                this.fieldInfoCollection[fieldName].SetValue(this.objectInstance, value);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected virtual object DoGetFieldValue(string fieldName)
        {
            if (this.fieldInfoCollection == null) return false;
            if (!this.fieldInfoCollection.ContainsKey(fieldName)) return false;
            try
            {
                return this.fieldInfoCollection[fieldName].GetValue(this.objectInstance);
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        protected virtual bool DoSetPropertyValue(string propertyName, object value)
        {
            if (this.propertyInfoCollection == null) return false;
            if (!this.propertyInfoCollection.ContainsKey(propertyName)) return false;
            try
            {
                this.propertyInfoCollection[propertyName].SetValue(this.objectInstance, value, null);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected virtual object DoGetPropertyValue(string propertyName)
        {
            if (this.propertyInfoCollection == null) return false;
            if (!this.propertyInfoCollection.ContainsKey(propertyName)) return false;
            try
            {
                return this.propertyInfoCollection[propertyName].GetValue(this.objectInstance, null);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        protected virtual object DoExecuteMethod(string methodName, object[] args)
        {
            if (this.methodInfoCollection == null) return false;
            if (!this.methodInfoCollection.ContainsKey(methodName)) return false;
            try
            {
                return this.methodInfoCollection[methodName].Invoke(this.objectInstance, args);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        protected virtual List<Attribute> DoGetMemberAttribute(string memberName)
        {
            List<Attribute> list = new List<Attribute>();
            foreach (object o in this.memberInfoCollection[memberName].GetCustomAttributes(true))
            {
                Attribute attr = (Attribute)o;
                list.Add(attr);
            }
            return list;
        }

        protected virtual void InitializeObjectReflectionInformation()
        {
            this.propertyInfoCollection = this.InitializeProperties();
            this.methodInfoCollection = this.InitializeMethods();
            this.fieldInfoCollection = this.InitializeFields();
        }

        protected void InitializeObjectInstance()
        {
            this.objectInstance = Activator.CreateInstance(this.objectType, BindingFlags.CreateInstance, null, null, null);
        }

        protected void InitializeObjectInstance(object[] args)
        {
            this.objectInstance = Activator.CreateInstance(this.objectType, BindingFlags.CreateInstance, null, args, null);
        }

        protected virtual Dictionary<string, PropertyInfo> InitializeProperties()
        {
            try
            {
                Dictionary<string, PropertyInfo> list = new Dictionary<string, PropertyInfo>();
                List<string> nameList = new List<string>();
                foreach (MemberInfo member in this.objectType.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField | BindingFlags.SetField))
                {
                    if ((member.MemberType & MemberTypes.Property) == MemberTypes.Property)
                    {
                        list.Add(member.Name, (PropertyInfo)member);
                        nameList.Add(member.Name);
                        this.memberInfoCollection.Add(member.Name, member);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        protected virtual Dictionary<string, MethodInfo> InitializeMethods()
        {
            try
            {
                Dictionary<string, MethodInfo> list = new Dictionary<string, MethodInfo>();
                List<string> nameList = new List<string>();
                foreach (MethodInfo method in this.objectType.GetMethods(BindingFlags.Instance | BindingFlags.Public))
                {
                    list.Add(method.Name, method);
                    nameList.Add(method.Name);
                    this.memberInfoCollection.Add(method.Name, method);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        protected virtual Dictionary<string, FieldInfo> InitializeFields()
        {
            try
            {
                Dictionary<string, FieldInfo> list = new Dictionary<string, FieldInfo>();
                List<string> nameList = new List<string>();
                foreach (MemberInfo member in this.objectType.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField | BindingFlags.SetField))
                {
                    if ((member.MemberType & MemberTypes.Field) == MemberTypes.Field)
                    {
                        list.Add(member.Name, (FieldInfo)member);
                        nameList.Add(member.Name);
                        this.memberInfoCollection.Add(member.Name, member);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #region Static methods

        public static ParameterInfo[] GetParameters(MethodInfo methodInfo) 
        {
            if (methodInfo == null)
            {
                return null;
            }

            return methodInfo.GetParameters();
        }

        public static bool ContainSpecifiedAttribute(object specifiedObject, Type attributeType)
        {
            return specifiedObject.GetType().GetCustomAttributes(attributeType, true).Length > 0;
        }

        public static Type GetBaseObjectType(object specifiedObject)
        {
            return specifiedObject.GetType().BaseType;
        }

        public static PropertyInfo[] GetMatchedPropertiesForEachAttribute(object specifiedObject, Type attributeType)
        {
            List<PropertyInfo> properties = new List<PropertyInfo>();
            Reflector hepler = new Reflector(specifiedObject);
            foreach (KeyValuePair<String, PropertyInfo> item in hepler.ObjectProperties)
            {
                if (hepler.ExistAttribute(item.Key, attributeType))
                {
                    properties.Add(item.Value);
                }
            }
            return properties.ToArray();
        }

        public static Attribute GetObjectSpecifiedAttribute(object specifiedObject, Type attributeType)
        {
            return specifiedObject.GetType().GetCustomAttributes(attributeType, false)[0] as Attribute;
        }

        public static object GetPropertyValue(object specifiedObject, string propertyName)
        {
            return specifiedObject.GetType().GetProperty(propertyName,
                                             BindingFlags.Public | BindingFlags.Instance).GetValue(specifiedObject, null);
        }

        public static object GetPropertyAttribute(PropertyInfo propertyInfo, Type attributeType)
        {
            object[] attributies = propertyInfo.GetCustomAttributes(attributeType, false);
            if (attributies.Length > 0)
            {
                return attributies[0];
            }
            else
            {
                return null;
            }
        }

        public static bool ContainSpecifiedInterface(object specifiedObject, Type interfaceType)
        {
            Type[] interfaces = specifiedObject.GetType().GetInterfaces();

            if (interfaces == null) return false;

            foreach (Type item in interfaces)
            {
                if (item == interfaceType)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

    }
}
