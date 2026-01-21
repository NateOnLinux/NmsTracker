namespace NmsTracker.Domain.Entities.Saves;

public readonly record struct Platform(PlatformId PlatformId, Save[] Saves);

public readonly record struct PlatformId(string Value);