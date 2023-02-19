namespace CnCNet.LauncherStub;

using System.Collections.ObjectModel;

public static class AdvancedMessageBoxHelper
{
    public static int? ShowMessageBoxWithSelection(string message, string title, string[] selections)
    {
        var msgbox = new AdvancedMessageBox();
        var model = (AdvancedMessageBoxViewModel)msgbox.DataContext;
        model.Title = title;
        model.Message = message;

        var commands = new ObservableCollection<CommandViewModel>();
        for (int i = 0; i < selections.Length; i++)
        {
            int i_copy = i;
            commands.Add(new CommandViewModel()
            {
                Text = selections[i],
                Command = new RelayCommand(_ =>
                {
                    msgbox.Result = i_copy;
                    msgbox.Close();
                }),
            });
        }

        model.Commands = commands;
        _ = msgbox.ShowDialog();
        return msgbox.Result as int?;
    }

    public static void ShowOkMessageBox(string message, string title, string okText = "OK")
    {
        _ = ShowMessageBoxWithSelection(message, title, new[] { okText });
    }

    public static bool ShowYesNoMessageBox(string message, string title, string yesText = "Yes", string noText = "No")
    {
        int? result = ShowMessageBoxWithSelection(message, title, new[] { yesText, noText });
        return result == 0;
    }
}