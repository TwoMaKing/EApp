using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EApp.Mvvm.Bindings;
using EApp.Mvvm.Commands;

namespace EApp.Mvvm.Controls
{
    public class MvTextBox : TextBox, IUIElement, IBindableControl
    {
        public CommandBindingCollection CommandBindings
        {
            get { throw new NotImplementedException(); }
        }

        public ViewModelBase DataContext
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Control Owner
        {
            get 
            { 
                return this; 
            }
        }


        #region IBindableControl members

        public ViewModelBase ViewModel
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string ModelElementName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool FormattingEnabled
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string FormatString
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public object NullValue
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
