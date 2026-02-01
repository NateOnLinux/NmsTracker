using System.Runtime.CompilerServices;
using NmsTracker.Domain.Entities.PlayerState;
using CH = NmsTracker.Domain.Utils.CoordinatesHelper;

namespace NmsTracker.Domain.VObs.Location;

/// <summary>
/// The 64-bit value representing galaxy, region, solar system, and planet.<br /> See the No Man's Sky Wiki for
/// more information about <see href="https://nomanssky.miraheze.org/wiki/Universal_Address"> Universal Address </see>
/// </summary>
public static class UniversalAddress {
    private const ulong XMask = 0x00_0_000_00_00_000_FFFUL;
    private const ulong ZMask = 0x00_0_000_00_00_FFF_000UL;
    private const ulong YMask = 0x00_0_000_00_FF_000_000UL;
    private const ulong GMask = 0x00_0_000_FF_00_000_000UL;
    private const ulong SsMask = 0x00_0_FFF_00_00_000_000UL;
    private const ulong PMask = 0x00_F_000_00_00_000_000UL;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short GetX(this ulong ua) => (short)((ua & XMask) >> 0);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short GetZ(this ulong ua) => (short)((ua & ZMask) >> 12);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte GetY(this ulong ua) => (sbyte)((ua & YMask) >> 24);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte GetG(this ulong ua) => (byte)((ua & GMask) >> 32);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort GetSs(this ulong ua) => (ushort)((ua & SsMask) >> 40);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte GetP(this ulong ua) => (byte)((ua & PMask) >> 52);

    public static PlayerCoordinates ToPlayerCoordinates(ulong ua) {
        (ushort x, ushort z, byte y) = DecodeCoordinates(ua.GetX(), ua.GetZ(), ua.GetY());
        var pcX = (short)(x - 2047);
        var pcZ = (short)(z - 2047);
        var pcY = (sbyte)(y - 127);
        return new PlayerCoordinates(pcX, pcZ, pcY, ua.GetG(), ua.GetSs(), ua.GetP());
    }

    public static ulong FromPlayerCoordinates(PlayerCoordinates pc) {
        var gcX = (ushort)(pc.X + 2047);
        var gcZ = (ushort)(pc.Z + 2047);
        var gcY = (byte)(pc.Y + 127);
        (short uaX, short uaZ, sbyte uaY) = EncodeCoordinates(gcX, gcZ, gcY);
        return Pack(uaX, uaZ, uaY, pc.G, pc.Ss, pc.P);
    }

    public static GalacticCoordinates ToGalacticCoordinates(ulong ua) {
        (ushort x, ushort z, byte y) = DecodeCoordinates(ua.GetX(), ua.GetZ(), ua.GetY());
        return new GalacticCoordinates(x, z, y, ua.GetG(), ua.GetSs(), ua.GetP());
    }

    public static ulong FromGalacticCoordinates(GalacticCoordinates gc) {
        (short uaX, short uaZ, sbyte uaY) = EncodeCoordinates(gc.X, gc.Z, gc.Y);
        return Pack(uaX, uaZ, uaY, gc.G, gc.Ss, gc.P);
    }

    private static ulong Pack(short x, short z, sbyte y, byte g, ushort ss, byte p) => (ushort)x |
        (((ulong)(ushort)z) << 12) |
        (((ulong)(byte)y) << 24) |
        ((ulong)g << 32) |
        ((ulong)ss << 40) |
        ((ulong)p << 52);

    private static (ushort x, ushort z, byte y) DecodeCoordinates(short x, short z, sbyte y) => (
        CH.UAtoGCLut12[(ushort)x], CH.UAtoGCLut12[(ushort)z], CH.UAtoGCLut8[(byte)y]);

    private static (short x, short z, sbyte y) EncodeCoordinates(ushort x, ushort z, byte y) =>
        (CH.GCtoUALut12[x], CH.GCtoUALut12[z], CH.GCtoUALut8[y]);
}
