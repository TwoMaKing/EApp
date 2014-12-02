using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EApp.Windows.Mvc
{
    public class ReflectedParameterDescriptor : ParameterDescriptor
    {
        private ParameterInfo parameterInfo;

        public ReflectedParameterDescriptor(ParameterInfo parameterInfo) 
        {
            this.parameterInfo = parameterInfo;  
        }

        public override object DefaultValue
        {
            get
            {
                return this.parameterInfo.DefaultValue;
            }
        }

        public override string ParameterName
        {
            get 
            {
                return this.parameterInfo.Name;
            }
        }

        public override Type ParameterType
        {
            get 
            {
                return this.parameterInfo.ParameterType;
            }
        }

        public ParameterInfo ParameterInfo 
        {
            get 
            {
                return this.parameterInfo;
            }
        }
    }
}
