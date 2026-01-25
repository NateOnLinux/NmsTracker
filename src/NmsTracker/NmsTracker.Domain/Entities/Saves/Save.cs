namespace NmsTracker.Domain.Entities.Saves;

public readonly record struct Save(Name Name, PlatformId PlatformId, SaveId ContainerId, DateTime LastModifyTime) { }

public readonly record struct Name(string Value);

public readonly record struct SaveId(string Value);
