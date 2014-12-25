using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace EApp.Mvvm
{
    public abstract class ViewModelBase: INotifyPropertyChanged, IDisposable
    {
        public string DisplayName { get; protected set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        { 
            
        }

        /// <summary>
        /// Verify that the property name matches a real,  
        //  public, instance property on this object.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        protected void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this)[propertyName];

            if (propertyDescriptor == null)
            {
                throw new ArgumentException("The specified property doesn't exist in the view model.");
            }
            else
            {
                if (propertyDescriptor.IsReadOnly)
                {
                    throw new ArgumentException("The property is read-only.");
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.VerifyPropertyName(propertyName);

                PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);

                this.PropertyChanged(this, e);
            }
        }

    }
}
