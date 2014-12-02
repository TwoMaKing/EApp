using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EApp.Common.Util
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

    public class CancelEventArgs<T> : CancelEventArgs 
    {
        private T data;

        public CancelEventArgs(T data) : this(false, data) { }

        public CancelEventArgs(bool cancel, T data) : base(cancel)
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
