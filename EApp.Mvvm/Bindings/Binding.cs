using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EApp.Mvvm.Bindings
{
    public class Binding : System.Windows.Forms.Binding
    {
        public Binding(string propertyName, object dataSource, string dataMember) : base(propertyName, dataSource, dataMember) { }

        public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled) : base(propertyName, dataSource, dataMember, formattingEnabled) { }

        public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode)
            : base(propertyName, dataSource, dataMember, formattingEnabled, dataSourceUpdateMode) { }

        public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue)
            : base(propertyName, dataSource, dataMember, formattingEnabled, dataSourceUpdateMode, nullValue) { }

        [DefaultValue(false)]
        public bool IsAsync { get; set; }

        public object AsyncState { get; set; }
    }
}
