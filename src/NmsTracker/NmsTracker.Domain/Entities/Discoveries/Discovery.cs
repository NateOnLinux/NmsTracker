using NmsTracker.Domain.VObs.Discoveries;

namespace NmsTracker.Domain.Entities.Discoveries;

public readonly record struct Discovery {

    public Discovery(DiscoveryType type, ulong ua) {
        Type = type;
        UniversalAddress = ua;
    }
    public DiscoveryType Type { get; init; }
    public ulong UniversalAddress { get; init; }
}
