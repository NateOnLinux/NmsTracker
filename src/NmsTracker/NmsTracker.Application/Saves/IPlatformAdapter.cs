using NmsTracker.Domain.Entities.Saves;

namespace NmsTracker.Application.Saves;

public interface IPlatformAdapter {
    IObservable<PlatformChangeEvent> PlatformsObservable { get; }

    void Load(Save save);
    void Unload(Save save);
}
