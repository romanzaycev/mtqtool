using System;
using MTQtoolEditor.Services;
using MTQtoolEditor.ViewModels;
using MTQtoolEditor.Views;
using SimpleInjector;

namespace MTQtoolEditor.IoC;

public class ContainerBuilder
{
    public Container Build()
    {
        var c = new Container();
        
        Register(c);
        c.Verify();

        return c;
    }

    private void Register(Container container)
    {
        // Services
        container.Register<IUserProfileDirectory, UserProfileDirectory>(Lifestyle.Singleton);
        
        // View models
        container.Register<MainWindowViewModel>();
    }
}