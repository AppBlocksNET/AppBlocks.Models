using System;
using System.Diagnostics;
using System.Windows.Input;

namespace AppBlocks.Models.Commands
{
    public class BaseCommand : ICommand
    {
        //private bool ICommand _command;

        private bool _isEnabled = true;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => IsEnabled;

        public virtual void Execute(object parameter) => Trace.WriteLine($"{this}.Execute:{DateTime.Now.ToShortTimeString()}");

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (value != _isEnabled)
                {
                    _isEnabled = value;
                    if (CanExecuteChanged != null)
                    {
                        CanExecuteChanged(this, EventArgs.Empty);
                    }
                }
            }
        }
    }
}