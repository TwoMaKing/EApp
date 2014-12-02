using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Windows.Mvc
{
    public sealed class ControllerDescriptorFactory
    {
        private static SortedList<string, ControllerDescriptor> controllerDescriptorList = 
            new SortedList<string, ControllerDescriptor>();

        public static void AddControllerDescriptor(string controllerName, ControllerDescriptor controllerDescriptor)
        {
            if (controllerDescriptorList.ContainsKey(controllerName))
            {
                throw new ArgumentException("A Controller Descriptor with the same key already exists in the Controller Descriptor Dictionary");
            }

            controllerDescriptorList.Add(controllerName, controllerDescriptor);
        }

        public static ControllerDescriptor GetControllerDescriptor(string controllerName)
        {
            if (controllerDescriptorList.ContainsKey(controllerName))
            {
                return controllerDescriptorList[controllerName];
            }

            return null;
        }

        public static bool Contains(string controllerName)
        {
            return controllerDescriptorList.ContainsKey(controllerName);
        }
    }
}
