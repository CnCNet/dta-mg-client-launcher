namespace CnCNet.LauncherStub;

using System.Windows.Input;

public class CommandViewModel : NotifyPropertyChangedBase
{
    private string _Text;
    public string Text { get => _Text; set { _Text = value; this.NotifyPropertyChanged(); } }

    private ICommand _Command;
    public ICommand Command { get => _Command; set { _Command = value; this.NotifyPropertyChanged(); } }
}
