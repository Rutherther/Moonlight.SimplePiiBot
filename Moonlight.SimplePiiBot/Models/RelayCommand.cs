using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Moonlight.SimplePiiBot.Models
{
    public class RelayCommand : ICommand
    {
        Action TargetExecuteMethod;
        Func<bool> TargetCanExecuteMethod;
        public RelayCommand(Action executeMethod)
        {
            TargetExecuteMethod = executeMethod;
        }

        public RelayCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            TargetExecuteMethod = executeMethod;
            TargetCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, System.EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;

        bool ICommand.CanExecute(object parameter)
        {
            if (TargetCanExecuteMethod != null)
                return TargetCanExecuteMethod();
            if (TargetExecuteMethod != null)
                return true;
            return false;
        }

        void ICommand.Execute(object parameter)
        {
            TargetExecuteMethod?.Invoke();
        }
    }

    public class RelayCommand<T> : ICommand
    {
        Action<T> TargetExecuteMethod;
        Func<bool> TargetCanExecuteMethod;
        public RelayCommand(Action<T> executeMethod)
        {
            TargetExecuteMethod = executeMethod;
        }

        public RelayCommand(Action<T> executeMethod, Func<bool> canExecuteMethod)
        {
            TargetExecuteMethod = executeMethod;
            TargetCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, System.EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;

        bool ICommand.CanExecute(object parameter)
        {
            if (TargetCanExecuteMethod != null)
                return TargetCanExecuteMethod();
            if (TargetExecuteMethod != null)
                return true;
            return false;
        }

        void ICommand.Execute(object parameter)
        {
            TargetExecuteMethod?.Invoke((T)parameter);
        }
    }
}
