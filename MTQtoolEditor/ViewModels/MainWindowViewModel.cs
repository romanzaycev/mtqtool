using ReactiveUI;

namespace MTQtoolEditor.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
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

    public MainWindowViewModel()
    {
    }

    public void OnToggleTextCommand()
    {
        Greeting = _greeting == Greetings[0] ? Greetings[1] : Greetings[0];
    }
}