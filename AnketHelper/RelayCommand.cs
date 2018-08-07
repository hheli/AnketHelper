using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AnketHelper
{
    class RelayCommand : ICommand
    {   //http://msdn.microsoft.com/en-us/magazine/dd419663.aspx
        //    public RelayCommand(Action execute,  Predicate canExecute)
        //    {
        //        _execute = execute;
        //        _canExecute = canExecute;
        //    }
        //    public bool CanExecute(object parameter)
        //    {
        //        return _canExecute(parameter);
        //    }
        //    public event EventHandler CanExecuteChanged
        //    {
        //        add { CommandManager.RequerySuggested += value; }
        //        remove { CommandManager.RequerySuggested -= value; }
        //    }
        //    public void Execute(object parameter)
        //    {
        //        _execute(parameter);
        //    }
        //    private readonly Action _execute;
        //    private readonly Predicate _canExecute;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
