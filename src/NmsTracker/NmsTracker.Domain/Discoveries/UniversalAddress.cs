using NmsTracker.Domain.Helpers;

namespace NmsTracker.Domain.Discoveries;

/// <summary>
/// The 64-bit value representing galaxy, region, solar system, and planet
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

    public UniversalAddress(ushort x = 0, ushort z = 0, byte y = 0, byte g = 0, ushort ss = 0, byte p = 0)
        : this(Pack(x, z, y, g, ss, p)) { }

    // Pack bits to UA
    private static ulong Pack(ushort x, ushort z, byte y, byte g, ushort ss, byte p) =>
        x | ((ulong)z << 12) | ((ulong)y << 24) | ((ulong)g << 32) | ((ulong)ss << 40) | ((ulong)p << 52);

    public GalacticCoordinates ToGalacticCoordinates() => GalacticCoordinates.FromUniversalAddress(this);
}
