using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EApp.Core
{
    public class CancelEventArgs<T> : CancelEventArgs
    {
        private T data;

        public CancelEventArgs(T data) : this(false, data) { }

        public CancelEventArgs(bool cancel, T data)
            : base(cancel)
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
