using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MTQtoolEditor.IoC;
using MTQtoolEditor.ViewModels;
using MTQtoolEditor.Views;
using SimpleInjector;

namespace MTQtoolEditor;

public partial class App : Application
{
    private readonly Container _container;
    
    public App()
    {
        _container = new ContainerBuilder().Build();
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainWindowViewModel = _container.GetInstance<MainWindowViewModel>();
            
            desktop.MainWindow = new MainWindow()
            {
                DataContext = mainWindowViewModel,
                Container = _container
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}

