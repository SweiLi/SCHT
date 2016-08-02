using System;
using System.Windows.Input;

namespace BdhConfEditor
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> m_Execute;
        private readonly Predicate<object> m_CanExecute;

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null) throw new ArgumentNullException("execute");

            this.m_Execute = execute;
            this.m_CanExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.m_CanExecute == null ? true : this.m_CanExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            this.m_Execute(parameter);
        }
    }
}
