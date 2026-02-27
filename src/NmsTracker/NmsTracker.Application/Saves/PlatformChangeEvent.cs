using NmsTracker.Application.Common;
using NmsTracker.Domain.Entities.Saves;

namespace NmsTracker.Application.Saves;

public sealed record PlatformChangeEvent(
    IReadOnlyList<Platform> Platforms,
    DateTimeOffset Timestamp) : ChangeEvent(Timestamp);
