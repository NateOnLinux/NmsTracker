using NmsTracker.ViewModels;
using ReactiveUI.Avalonia;

namespace NmsTracker.Views;

public partial class MainWindow : ReactiveWindow<MainViewModel> {
    public MainWindow() {
        InitializeComponent();
    }
}
