using NmsTracker.Domain.Helpers;

namespace NmsTracker.Domain.Discoveries;

/// <summary>
/// Represents the decoded representation of the Universal Address<br/>
/// The decoded coordinates are in the range X,Z: 0-4096, Y: 0-255, PSs: 4 bit Planet + 12 bit SolarSystem Index
/// </summary>
/// <param name="Ua"></param>
public readonly record struct GalacticCoordinates(ushort X, ushort Z, byte Y, ushort PSs, byte? G = 0)
{
    public static GalacticCoordinates FromUniversalAddress(UniversalAddress ua)
    {
        var x = CoordinatesHelper.DecodeX(ua.X);
        var z = CoordinatesHelper.DecodeZ(ua.Z);
        var y = CoordinatesHelper.DecodeY(ua.Y);
        var pSs = (ushort)((ua.Ss & 0x0FFF) | ((ua.P & 0x0F) << 12));
        var g = ua.G;
        return new GalacticCoordinates(x, z, y, pSs, g);
    }
}
