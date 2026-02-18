using NmsTracker.Domain.VObs.Saves;

namespace NmsTracker.Domain.Entities.Saves;

public sealed record Save(
    SaveId SaveId,
    PlatformId PlatformId,
    string Name,
    bool IsLoaded,
    bool IsSynced,
    DateTime? LastWriteTime);
