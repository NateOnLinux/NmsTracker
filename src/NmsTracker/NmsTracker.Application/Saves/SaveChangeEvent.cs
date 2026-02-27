using NmsTracker.Application.Shared;
using NmsTracker.Domain.Entities.Saves;

namespace NmsTracker.Application.Saves;

public sealed record SaveChangeEvent(IReadOnlyList<Save> Saves, DateTimeOffset Timestamp)
    : ChangeEvent(Timestamp);
