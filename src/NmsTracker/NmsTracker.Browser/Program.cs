using System.Threading.Tasks;
using Avalonia;
using Avalonia.Browser;
using NmsTracker;

internal sealed class Program {
    private static Task Main(string[] args) => BuildAvaloniaApp().WithInterFont().StartBrowserAppAsync("out");

    public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>();
}
