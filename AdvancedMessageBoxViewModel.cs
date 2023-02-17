namespace CnCNet.LauncherStub;

using System.Collections.ObjectModel;

public class AdvancedMessageBoxViewModel : NotifyPropertyChangedBase
{

    private ObservableCollection<CommandViewModel> _Commands;
    public ObservableCollection<CommandViewModel> Commands { get => _Commands; set { _Commands = value; this.NotifyPropertyChanged(); } }

    private string _Title;
    public string Title { get => _Title; set { _Title = value; this.NotifyPropertyChanged(); } }

    private string _Message;
    public string Message { get => _Message; set { _Message = value; this.NotifyPropertyChanged(); } }
}
