using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace OpenSilver.Samples.Showcase;

public class CommandDemoViewModel : ObservableObject
{
    public IRelayCommand ShowMessageCommand { get; }

    private string _message = "Message";
    public string Message
    {
        get => _message;
        set
        {
            SetProperty(ref _message, value);
            ShowMessageCommand.NotifyCanExecuteChanged();
        }
    }

    public CommandDemoViewModel()
    {
        ShowMessageCommand = new RelayCommand<string>(ShowMessage, s => !string.IsNullOrWhiteSpace(s));
    }

    private void ShowMessage(string message)
    {
        MessageBox.Show($"Command is executed: {message}");
    }
}
