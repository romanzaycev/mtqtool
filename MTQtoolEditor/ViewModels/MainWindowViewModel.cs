using MTQtoolEditor.Services;
using ReactiveUI;

namespace MTQtoolEditor.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IUserProfileDirectory _userProfileDirectory;

    private static readonly string[] Greetings = new string[2] {
        "Hello world!",
        "YOLO"
    };

    private string _greeting = Greetings[0];
    public string Greeting
    {
        get => _greeting;
        set => this.RaiseAndSetIfChanged(ref _greeting, value);
    }

    public MainWindowViewModel(IUserProfileDirectory userProfileDirectory)
    {
        _userProfileDirectory = userProfileDirectory;
    }

    public void OnToggleTextCommand()
    {
        Greeting = _greeting == Greetings[0] ? Greetings[1] : Greetings[0];
    }

    public void OnClosed()
    {
        
    }
}