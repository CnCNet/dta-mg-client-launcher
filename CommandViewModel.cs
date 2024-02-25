namespace CnCNet.LauncherStub;

using System.Windows.Input;

public class CommandViewModel : NotifyPropertyChangedBase
{
    private string? text;

    public string Text
    {
        get => text;
        set
        {
            text = value;
            NotifyPropertyChanged("Text");
        }
    }

    private ICommand? command;

    public ICommand Command
    {
        get => command;
        set
        {
            command = value;
            NotifyPropertyChanged("Command");
        }
    }
}