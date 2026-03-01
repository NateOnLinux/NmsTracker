using NmsTracker.Domain.VObs.Saves;

namespace NmsTracker.Infrastructure.Saves;

public class PlatformOptions {
    public string? PlatformPath { get; set; }
    public PlatformId PreferredPlatform { get; set; }
}
