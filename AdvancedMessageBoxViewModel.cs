namespace CnCNet.LauncherStub;

using System.Collections.ObjectModel;

public class AdvancedMessageBoxViewModel : NotifyPropertyChangedBase
{
    private ObservableCollection<CommandViewModel>? commands;

    public ObservableCollection<CommandViewModel> Commands
    {
        get => commands;
        set
        {
            commands = value;
            NotifyPropertyChanged("Commands");
        }
    }

    private string? title;

    public string Title
    {
        get => title;
        set
        {
            title = value;
            NotifyPropertyChanged("Title");
        }
    }

    private string? message;

    public string Message
    {
        get => message;
        set
        {
            message = value;
            NotifyPropertyChanged("Message");
        }
    }
}