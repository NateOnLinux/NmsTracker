using System.Reactive;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace NmsTracker.ViewModels;

public partial class MainViewModel : ViewModelBase {
    [Reactive(SetModifier = AccessModifier.Private)]
    private string _greeting;

    public MainViewModel() {
        Greeting = "Welcome to NMS Tracker!";
    }
}
