using NmsTracker.Domain.VObs.Discoveries;

namespace NmsTracker.Domain.Entities.Discoveries;

public readonly record struct Discovery(DiscoveryType Type, ulong Coordinates);
