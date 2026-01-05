using NmsTracker.Domain.Helpers;

namespace NmsTracker.Domain.Discoveries;

/// <summary>
/// See the No Man's Sky Wiki for more information about
/// <see href="https://nomanssky.miraheze.org/wiki/Galactic_Coordinates">Galactic Coordinates</see>
/// </summary>
public readonly record struct GalacticCoordinates
{
    private readonly ushort _x;
    /// <summary>
    /// Regional X Coordinate in range 0 &#8804; value &#8804; 4095
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Provided value is out of range</exception>
    public ushort X
    {
        get => _x;
        private init
        {
            if (value > 4095)
                throw new ArgumentOutOfRangeException(nameof(X), value, "Value must be less than 4096");
            _x = value;
        }
    }
    
    private readonly ushort _z;
    /// <summary>
    /// Regional X Coordinate in range 0 &#8804; value &#8804; 4095
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Provided value is out of range</exception>
    public ushort Z
    {
        get => _z;
        private init
        {
            if (value > 4095)
                throw new ArgumentOutOfRangeException(nameof(Z), value, "Value must be less than 4096");
            _z = value;
        }
    }
    
    /// <summary>
    /// Regional Y Coordinate in range 0 &#8804; value &#8804; 255
    /// </summary>
    public byte Y { get; }
    
    /// <summary>
    /// 4 byte Planet Index and 12 Byte Solar System Index<br/>
    /// See the No Man's Sky Wiki for more information about
    /// <see href="https://nomanssky.miraheze.org/wiki/Galactic_Coordinates">Galactic Coordinates</see><br/>
    /// </summary>
    public ushort PSs { get; }
    
    /// <summary>
    /// Optional Galaxy ID in range 0 &#8804; value &#8804; 255
    /// </summary>
    public byte? G { get; }
    
    /// <summary>
    /// Represents the decoded representation of the Universal Address<br/>
    /// The decoded coordinates are in the range X,Z: 0-4096, Y: 0-255, PSs: 4 bit Planet + 12 bit SolarSystem Index
    /// </summary>
    public GalacticCoordinates(ushort x, ushort z, byte y, ushort pSs, byte? g = 0)
    {
        X = x;
        Z = z;
        Y = y;
        PSs = pSs;
        G = g;
    }
    
    public static GalacticCoordinates FromUniversalAddress(UniversalAddress ua)
    {
        var x = CoordinatesHelper.DecodeX(ua.X);
        var z = CoordinatesHelper.DecodeZ(ua.Z);
        var y = CoordinatesHelper.DecodeY(ua.Y);
        // pSs is packed 4 bytes [PSSS]
        var pSs = (ushort)((ua.Ss & 0x0FFF) | ((ua.P & 0xF) << 12));
        var g = ua.G;
        return new GalacticCoordinates(x, z, y, pSs, g);
    }
}
