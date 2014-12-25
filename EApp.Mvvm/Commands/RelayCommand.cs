using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Mvvm.Commands
{
    public class RelayCommand : ICommand
    {
        private Action<object> action;

        public event EventHandler CanExecuteChanged;
        
        public RelayCommand(Action<object> action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public bool CanExecute(object parameter, IUIElement commandTarget)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            if (this.action == null)
            {
                return;
            }

            this.action(parameter);
        }

        public void Execute(object parameter, IUIElement commandTarget)
        {
            throw new NotImplementedException();
        }

    }
}
