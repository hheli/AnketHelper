using System;
using System.Windows;
using System.Windows.Input;

namespace AnketHelper
{
    public class BtnClickCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
                var bcc = (MyViewModel)parameter;
                bcc.save();
                return;
        }
    }
    public class CnlBtnClickCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var bcc = (MyViewModel)parameter;
            bcc.cancel();
            return;
        }
    }
}