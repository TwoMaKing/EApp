using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Mvvm.Bindings
{
    public interface IBindableControl
    {
        ViewModelBase ViewModel { get; set; }

        string ModelElementName { get; set; }

        bool FormattingEnabled { get; set; }

        string FormatString { get; set; }

        object NullValue { get; set; }
    }
}
