using System;

namespace RequirementsTests.Avalonia_MVVM.Command;

public class DelegateCommand : BaseCommand
{
    private Func<object, bool> _CanExecute;
    private Action<object> _Execute;

    public DelegateCommand(Action<object> execute, Func<object,bool> canExecute)
    {
        _Execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _CanExecute = canExecute;
    }

    protected override bool CanExecute(object? parameter) => _CanExecute?.Invoke(parameter!) ?? true;
    protected override void Execute(object? parameter) => _Execute.Invoke(parameter!);
}