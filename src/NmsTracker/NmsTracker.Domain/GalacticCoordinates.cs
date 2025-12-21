namespace NmsTracker.Domain;

/// <summary>
/// Represents the encoded representation of the galactic coordinates.<br/>
/// The encoded coordinates are in the range X,Z: 0-4096, Y: 0-255, PSs: 4 bit Planet + 12 bit SolarSystem Index
/// </summary>
/// <param name="Ua"></param>
public readonly record struct GalacticCoordinates(ushort X, ushort Z, byte Y, ushort PSs);
