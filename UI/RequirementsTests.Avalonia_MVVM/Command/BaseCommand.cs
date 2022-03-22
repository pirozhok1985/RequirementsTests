using System;
using System.Windows.Input;

namespace RequirementsTests.Avalonia_MVVM.Command;

public abstract class BaseCommand : ICommand
{
    protected virtual bool CanExecute(object? parameter) => true;
    protected abstract void Execute(object? parameter);
    
    bool ICommand.CanExecute(object? parameter) => CanExecute(parameter) ;
    void ICommand.Execute(object? parameter) => Execute(parameter);
    
    public event EventHandler? CanExecuteChanged;
}