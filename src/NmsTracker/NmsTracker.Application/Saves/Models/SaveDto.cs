namespace NmsTracker.Application.Saves.Models;

public record SaveDto(
    Platform Platform,
    string SaveIdentifier,
    string Name,
    bool IsLoaded,
    TimeSpan PlayTime,
    DateTime? LastModifyDate = null);
