using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Mvvm.Commands
{
    /// <summary>
    ///  Defines a command.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        bool CanExecute(object parameter);

        /// <summary>
        ///  Determines whether this System.Windows.Input.RoutedCommand can execute in
        //   its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        /// <param name="commandtarget">Element at which to being looking for command handlers.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        bool CanExecute(object parameter, IUIElement commandTarget);

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter"></param>
        void Execute(object parameter);

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        /// <param name="commandtarget">Element at which to being looking for command handlers.</param>
        void Execute(object parameter, IUIElement commandTarget);

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        event EventHandler CanExecuteChanged;
    }
}
