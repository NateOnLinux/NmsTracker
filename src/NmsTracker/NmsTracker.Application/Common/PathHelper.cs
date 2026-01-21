namespace NmsTracker.Application.Common;

public class PathHelper() {

    public PathIsValidResult IsWellFormed(string path) {
        if (string.IsNullOrWhiteSpace(path)) {
            return new PathIsValidResult(false, "Path does not contain any characters");
        }
        HashSet<char> invalidChars = GetInvalidChars(path);

        if (invalidChars.Count > 0) {
            return new PathIsValidResult(false,
                $"Path '{path}' contains the following invalid characters " +
                $"for your platform: {string.Join(", ", invalidChars)}");
        }

        if (!Path.IsPathRooted(path)) {
            return new PathIsValidResult(false, $"Path '{path}' is not rooted. Expected an absolute path");
        }
        return new PathIsValidResult();
    }

    private static HashSet<char> GetInvalidChars(string path) {
        HashSet<char> invalidChars = Path.GetInvalidPathChars().ToHashSet();
        return path.Where(invalidChars.Contains).ToHashSet();
    }
}

public record PathIsValidResult(bool IsValid = true, string? Reason = null);
