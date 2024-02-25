namespace CnCNet.LauncherStub;

using System.ComponentModel;

public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

#if NET45_OR_GREATER || NETSTANDARD || NET
    /// <summary>
    /// This method is called by the Set accessor of each property. <br/>
    /// The CallerMemberName attribute that is applied to the optional propertyName parameter causes the property name of the caller to be substituted as an argument.
    /// </summary>
    /// <param name="propertyName">The property name. Default to the caller.</param>
    internal void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
#else
    internal void NotifyPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
#endif
}