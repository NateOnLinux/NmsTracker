using System.Reactive.Linq;
using NmsTracker.Domain.Entities.Saves;

namespace NmsTracker.Application.Saves;

public class SaveListener {

    public SaveListener(IPlatformAdapter adapter) {
        Saves =
            adapter.PlatformsObservable.Select(ps => ps.SelectMany(p => p.Saves).ToList()).Replay(1)
                .RefCount();
    }
    public IObservable<IReadOnlyList<Save>> Saves { get; }
}
