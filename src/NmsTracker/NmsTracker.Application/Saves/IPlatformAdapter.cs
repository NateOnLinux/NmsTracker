using NmsTracker.Domain.Entities.Saves;

namespace NmsTracker.Application.Saves;

public interface IPlatformAdapter {
    IObservable<IReadOnlyList<Platform>> PlatformsObservable { get; }

    void Load(Save save);
    void Unload(Save save);
}
