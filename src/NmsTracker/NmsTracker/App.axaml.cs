using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using NmsTracker.ViewModels;
using NmsTracker.Views;
using ReactiveUI;
using Splat;

namespace NmsTracker;

public class App : Application {
    public override void Initialize() {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted() {
        RegisterServices();

        switch (ApplicationLifetime) {
            case IClassicDesktopStyleApplicationLifetime cds:
                cds.MainWindow = AppLocator.Current.GetService<IViewFor<MainViewModel>>() as Window;
                break;
            case ISingleViewApplicationLifetime sv:
                sv.MainView =
                    AppLocator.Current.GetService<IViewFor<MainViewModel>>() as UserControl;
                break;
        }

        base.OnFrameworkInitializationCompleted();
    }

    public override void RegisterServices() {
        base.RegisterServices();
        IMutableDependencyResolver s = AppLocator.CurrentMutable;
        s.RegisterLazySingleton(() => new MainViewModel(), typeof(MainViewModel));

        switch (ApplicationLifetime) {
            case IClassicDesktopStyleApplicationLifetime:
                s.Register(() => new MainWindow(), typeof(IViewFor<MainViewModel>));
                break;
            case ISingleViewApplicationLifetime:
                s.Register(() => new MainView(), typeof(IViewFor<MainViewModel>));
                break;
        }
    }
}
