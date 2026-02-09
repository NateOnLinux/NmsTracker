using System;
using System.Reactive.Disposables.Fluent;
using NmsTracker.ViewModels;
using ReactiveUI;
using ReactiveUI.Avalonia;
using Splat;

namespace NmsTracker.Views;

public partial class MainView : ReactiveUserControl<MainViewModel> {
    public MainView() {
        InitializeComponent();
        ViewModel =
            AppLocator.Current.GetService<MainViewModel>() ??
            throw new InvalidOperationException("Failed to get MainViewModel");
        this.WhenActivated(disposables => {
            this.OneWayBind(ViewModel, vm => vm.Greeting, v => v.GreetingTextBlock.Text)
                .DisposeWith(disposables);
        });
    }
}
