using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using MTQtoolEditor.Services;
using SimpleInjector;

namespace MTQtoolEditor.Views;

public partial class MainWindow : Window
{
    public Container? Container;
    
    public MainWindow()
    {
        InitializeComponent();
        Closed += OnClosed;
    }

    private void OnClosed(object? sender, EventArgs e)
    {
        if (Container is not { } c)
        {
            // FIXME Log?
            
            return;
        }
        
        var userDirectoryService = c.GetInstance<IUserProfileDirectory>();
        
        Console.WriteLine($"Profile dir: {userDirectoryService.GetAppDirectory()}");
        
        // FIXME: Save Grid state
    }

    private void MenuItem_OnClick(object? sender, RoutedEventArgs e)
    {
        (Application.Current?.ApplicationLifetime as ClassicDesktopStyleApplicationLifetime)?.Shutdown(0);
    }
}