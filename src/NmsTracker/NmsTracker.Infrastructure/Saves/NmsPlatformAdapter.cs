using libNOM.io;
using libNOM.io.Enums;
using libNOM.io.Interfaces;
using NmsTracker.Application.Saves;
using NmsTracker.Domain.Entities.Saves;
using NmsTracker.Domain.VObs.Saves;
using System.Reactive.Linq;
using Microsoft.Extensions.Options;
using Platform = NmsTracker.Domain.Entities.Saves.Platform;

namespace NmsTracker.Infrastructure.Saves;

public class NmsPlatformAdapter : IPlatformAdapter {
    private readonly PlatformCollection _platformCollection;

    public NmsPlatformAdapter(PlatformCollection platformCollection,
        IOptionsMonitor<PlatformOptions> options) {
        _platformCollection = platformCollection;

        PlatformsObservable =
            Observable.FromEvent<PlatformOptions>(handler => options.OnChange(handler), _ => { })
                .Select(o => {
                    _platformCollection.Reinitialize();
                    _platformCollection.AnalyzePath(o.PlatformPath,
                        o.PreferredPlatform.ToPlatformEnum());
                    return ConstructPlatformChangeEvent();
                }).Replay(1).RefCount();
    }
    public IObservable<PlatformChangeEvent> PlatformsObservable { get; }

    public void Load(Save save) {
        IPlatform? p = GetPlatform(save.PlatformId);

        if (p is null) {
            throw new InvalidOperationException();
        }

        IContainer? c = GetContainer(p, save);

        if (c is null) {
            throw new InvalidOperationException();
        }

        p.Load(c);
    }

    public void Unload() {
        _platformCollection.Reinitialize();
    }

    private PlatformChangeEvent ConstructPlatformChangeEvent() {
        List<Platform> platforms = [
            .. GetPlatforms()
                .Select(p => new Platform(p.PlatformEnum.ToPlatformId(), p.Location, GetSaves(p)))
        ];
        return new PlatformChangeEvent(platforms, DateTime.UtcNow);
    }

    private static List<Save> GetSaves(IPlatform platform) {
        return platform.GetSaveContainers().Select(c => SaveSelector(platform, c)).ToList();

        static Save SaveSelector(IPlatform p, IContainer c) {
            Save s =
                new(new SaveId(c.Identifier), p.PlatformEnum.ToPlatformId(), c.SaveName, c.IsLoaded,
                    c.IsSynced, c.LastWriteTime?.DateTime);
            return s;
        }
    }

    private IPlatform? GetPlatform(PlatformId platformId) {
        return GetPlatforms().FirstOrDefault(p => p.PlatformEnum == platformId.ToPlatformEnum());
    }

    private static IContainer? GetContainer(IPlatform platform, Save save) {
        IEnumerable<IContainer> containers = platform.GetSaveContainers();
        return containers.FirstOrDefault(c => c.Identifier == save.SaveId.Value);
    }

    private List<IPlatform> GetPlatforms() => _platformCollection.ToList();
}

internal static class PlatformMappings {
    public static PlatformId ToPlatformId(this PlatformEnum platform) => platform switch {
        PlatformEnum.Steam => PlatformId.Steam,
        PlatformEnum.Microsoft => PlatformId.Microsoft,
        PlatformEnum.Gog => PlatformId.Gog,
        PlatformEnum.Switch => PlatformId.Switch,
        PlatformEnum.Playstation => PlatformId.Playstation,
        _ => PlatformId.Unknown
    };

    public static PlatformEnum ToPlatformEnum(this PlatformId platform) => platform switch {
        PlatformId.Steam => PlatformEnum.Steam,
        PlatformId.Microsoft => PlatformEnum.Microsoft,
        PlatformId.Gog => PlatformEnum.Gog,
        PlatformId.Switch => PlatformEnum.Switch,
        PlatformId.Playstation => PlatformEnum.Playstation,
        _ => PlatformEnum.Unknown
    };
}
