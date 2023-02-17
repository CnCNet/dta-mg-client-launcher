namespace CnCNet.LauncherStub;

using System.ComponentModel;
using System.Runtime.CompilerServices;

public class NotifyPropertyChangedBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// This method is called by the Set accessor of each property. <br/>
    /// The CallerMemberName attribute that is applied to the optional propertyName parameter causes the property name of the caller to be substituted as an argument.
    /// </summary>
    /// <param name="propertyName">The property name. Default to the caller.</param>
    internal void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}