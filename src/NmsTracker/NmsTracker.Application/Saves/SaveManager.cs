using System.Reactive.Linq;
using NmsTracker.Domain.Entities.Saves;

namespace NmsTracker.Application.Saves;

public class SaveManager {
    private readonly IPlatformAdapter _adapter;
    public SaveManager(IPlatformAdapter adapter) {
        _adapter = adapter;
        Saves =
            adapter.PlatformsObservable.Select(ps => ps.SelectMany(p => p.Saves).ToList()).Replay(1)
                .RefCount();
    }
    
    public IObservable<IReadOnlyList<Save>> Saves { get; }

    public void Load(Save save) {
        _adapter.Load(save);
    }

    public void Unload(Save save) {
        _adapter.Unload(save);
    }
}
