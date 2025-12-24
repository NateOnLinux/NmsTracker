namespace NmsTracker.Domain.Discoveries;

/// <summary>
/// The 64-bit value stored in the save file which represents the coordinates for a particular a region, solar system,
/// and planet
/// </summary>
/// <param name="Ua">The 64-bit Universal Address</param>
public readonly record struct UniversalAddress(ulong Ua)
{
    private const ulong XMask = 0x00_0_000_00_00_000_FFFUL;
    private const ulong ZMask = 0x00_0_000_00_00_FFF_000UL;
    private const ulong YMask = 0x00_0_000_00_FF_000_000UL;
    private const ulong GMask = 0x00_0_000_FF_00_000_000UL;
    private const ulong SsMask = 0x00_0_FFF_00_00_000_000UL;
    private const ulong PMask = 0x00_F_000_00_00_000_000UL;
    public ushort X => (ushort)((Ua & XMask) >> 0);
    public ushort Z => (ushort)((Ua & ZMask) >> 12);
    public byte Y => (byte)((Ua & YMask) >> 24);
    public byte G => (byte)((Ua & GMask) >> 32);
    public ushort Ss => (ushort)((Ua & SsMask) >> 40);
    public byte P => (byte)((Ua & PMask) >> 52);

    public UniversalAddress(ushort x = 0, ushort z = 0, byte y = 0, byte g = 0, ushort ss = 0, byte p = 0)
        : this(Encode(x, z, y, g, ss, p)) { }

    private static ulong Encode(ushort x, ushort z, byte y, byte g, ushort ss, byte p) =>
        x | ((ulong)z << 12) | ((ulong)y << 24) | ((ulong)g << 32) | ((ulong)ss << 40) | ((ulong)p << 52);

    public GalacticCoordinates Decode()
    {
        var x = CoordinateValues.XFromUA(X);
        var z = CoordinateValues.ZFromUA(Z);
        var y = CoordinateValues.YFromUA(Y);
        // The fourth section of the Signal Booster's galactic coordinates encodes the planet and solar system [PSSS]
        var pSs = (ushort)((Ss & 0x0FFF) |
                           ((P & 0x0F) << 12));
        return new GalacticCoordinates(x, z, y, pSs);
    }
}
