using System;
using System.Windows.Input;

namespace WorkerViewer.Infrastructure
{
    public class CommandHandler : ICommand
    {
        private Action _action;
        private bool _canExecute;

        public CommandHandler(Action action, bool canExecute)
        {
            this._action = action;
            this._canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }
        public void Execute(object parameter)
        {
            _action();
        }
    }

    public class CommandHandlerGeneric<T> : ICommand
    {
        private Action<T> _action;
        private bool _canExecute;

        public CommandHandlerGeneric(Action<T> action, bool canExecute)
        {
            this._action = action;
            this._canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return this._canExecute;
        }
        public void Execute(object parameter)
        {
            _action((T)parameter);
        }
    }
}
