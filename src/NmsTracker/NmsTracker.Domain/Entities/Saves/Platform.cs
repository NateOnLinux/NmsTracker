using NmsTracker.Domain.VObs.Saves;

namespace NmsTracker.Domain.Entities.Saves;

public readonly record struct Platform(
    PlatformId PlatformId,
    DirectoryInfo? Location,
    IReadOnlyList<Save> Saves);
