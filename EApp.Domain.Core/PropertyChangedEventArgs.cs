using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EApp.Domain.Core
{
    public class PropertyChangedEventArgs : System.ComponentModel.PropertyChangedEventArgs
    {
        public PropertyChangedEventArgs(string propertyName, object oldValue, object newValue) : base(propertyName)
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

        public object OldValue
        {
            get;
            private set;
        }

        public object NewValue
        {
            get;
            private set;
        }
    }

}
