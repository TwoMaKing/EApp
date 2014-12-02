using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EApp.Core
{
    public class EventArgs<T> : EventArgs
    {
        private T data;

        public EventArgs(T data) 
        { 
            this.data = data;
        }

        public T Data
        {
            get
            {
                return this.data;
            }
        }
    }

}
