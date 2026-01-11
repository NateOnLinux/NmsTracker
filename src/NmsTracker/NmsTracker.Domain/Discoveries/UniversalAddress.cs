using NmsTracker.Domain.Helpers;
using NmsTracker.Domain.PlayerState;

namespace NmsTracker.Domain.Discoveries;

/// <summary>
/// The 64-bit value representing galaxy, region, solar system, and planet.<br/>
/// See the No Man's Sky Wiki for more information about
/// <see href="https://nomanssky.miraheze.org/wiki/Universal_Address">Universal Address</see>
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

    // specify UL to avoid packing 0's unnecessarily
    public UniversalAddress() : this(0UL) { }

    public PlayerCoordinates ToPlayerCoordinates() => PlayerCoordinates.FromUniversalAddress(this);

    public GalacticCoordinates ToGalacticCoordinates() => GalacticCoordinates.FromUniversalAddress(this);

    public static UniversalAddress FromPlayerCoordinates(PlayerCoordinates playerCoords)
    {
        var x = (ushort)(playerCoords.X + 2048);
        var z = (ushort)(playerCoords.Z + 2048);
        var y = (byte)(playerCoords.Y + 128);
        var coords = Pack(x, z, y, playerCoords.G, playerCoords.Ss, playerCoords.P);
        return new UniversalAddress(coords);
    }

    public static UniversalAddress FromGalacticCoordinates(GalacticCoordinates galacticCoordinates)
    {
        ushort x = CoordinatesHelper.EncodeX(galacticCoordinates.X);
        ushort z = CoordinatesHelper.EncodeZ(galacticCoordinates.Z);
        byte y = CoordinatesHelper.EncodeY(galacticCoordinates.Y);
        byte g = galacticCoordinates.G ?? 0;
        ushort ss = (ushort)((galacticCoordinates.PSs << 4) >> 4);
        byte p = (byte)(galacticCoordinates.PSs >> 12);

        var coords = Pack(x, z, y, g, ss, p);
        return new UniversalAddress(coords);
    }

    // Pack bits to UA
    private static ulong Pack(ushort x, ushort z, byte y, byte g, ushort ss, byte p) =>
        x | ((ulong)z << 12) | ((ulong)y << 24) | ((ulong)g << 32) | ((ulong)ss << 40) | ((ulong)p << 52);

}