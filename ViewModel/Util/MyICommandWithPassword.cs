using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bolnica
{
    public class MyICommandWithPassword : ICommand
    {
        Action<PasswordBox> _TargetExecuteMethod;
        Func<bool> _TargetCanExecuteMethod;

        public MyICommandWithPassword(Action<PasswordBox> executeMethod, Func<bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        #region ICommand Members

        bool ICommand.CanExecute(object parameter)
        {
            if (_TargetCanExecuteMethod != null)
            {
                return _TargetCanExecuteMethod();
            }

            if (_TargetExecuteMethod != null)
            {
                return true;
            }

            return false;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        void ICommand.Execute(object parameter)
        {
            // if _TargetExecuteMethod isn't null invoke
            _TargetExecuteMethod?.Invoke(parameter as PasswordBox);
        }

        #endregion
    }
}
