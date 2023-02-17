namespace CnCNet.LauncherStub;

using System;
using System.Windows.Input;

public class RelayCommand : ICommand
{
    private readonly Action<object> execute;

    public RelayCommand(Action<object> execute)
    {
        this.execute = execute;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        execute(parameter);
    }
}