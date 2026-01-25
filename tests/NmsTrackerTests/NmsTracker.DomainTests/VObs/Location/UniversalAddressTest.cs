using NmsTracker.Domain.Entities.PlayerState;
using NmsTracker.Domain.VObs.Location;

namespace NmsTracker.DomainTests.VObs.Location;

public class UniversalAddressTest {

    public static TheoryData<PlayerCoordinates, ulong> PcUa => [
        (new PlayerCoordinates(-8, 0, -2, 0, 486, 0), 0x0001E600FE000FF8),
        (new PlayerCoordinates(0, 0, 0, 0, 0, 0), 0UL),
        (new PlayerCoordinates(-1, -1, -1, 255, 4095, 15), 0x00FFFFFFFFFFFFFF),
        (new PlayerCoordinates(309, 8, 88, 19, 8, 8), 0x0080081358008135)
    ];

    public static TheoryData<ulong, PlayerCoordinates> UaPc => [
        (0x0001E600FE000FF8, new PlayerCoordinates(-8, 0, -2, 0, 486, 0)),
        (0UL, new PlayerCoordinates(0, 0, 0, 0, 0, 0)),
        (0x00FFFFFFFFFFFFFF, new PlayerCoordinates(-1, -1, -1, 255, 4095, 15)),
        (0x0080081358008135, new PlayerCoordinates(309, 8, 88, 19, 8, 8))
    ];

    public static TheoryData<GalacticCoordinates, ulong> GcUa => [
        (new GalacticCoordinates(0x7F7, 0x7FF, 0x7D, 0x0, 0x1E6, 0x0), 0x0001E600FE000FF8UL),
        (new GalacticCoordinates(0x7FF, 0x7FF, 0x7F, 0x0, 0x0, 0x0), 0x0UL),
        (new GalacticCoordinates(0x7FE, 0x7FE, 0x7E, 0xFF, 0xFFF, 0xF), 0x00FFFFFFFFFFFFFFUL),
        (new GalacticCoordinates(0x934, 0x807, 0xD7, 0x13, 0x8, 0x8), 0x0080081358008135UL)
    ];

    public static TheoryData<ulong, GalacticCoordinates> UaGc => [
        (0x0001E600FE000FF8UL, new GalacticCoordinates(0x7F7, 0x7FF, 0x7D, 0x0, 0x1E6, 0x0)),
        (0x0UL, new GalacticCoordinates(0x7FF, 0x7FF, 0x7F, 0x0, 0x0, 0x0)),
        (0x00FFFFFFFFFFFFFFUL, new GalacticCoordinates(0x7FE, 0x7FE, 0x7E, 0xFF, 0xFFF, 0xF)),
        (0x0080081358008135UL, new GalacticCoordinates(0x934, 0x807, 0xD7, 0x13, 0x8, 0x8))
    ];
    [Theory]
    [InlineData(0UL, 0x0)]
    [InlineData(0x0080081358008135UL, 0x135)]
    [InlineData(0x00FFFFFFFFFFFFFFUL, 0xFFF)]
    [InlineData(0xFFFFFFFFFFFFFFFFUL, 0xFFF)]
    public void GetX_ReturnsX(ulong ua, ushort expect) {
        Assert.Equal(expect, ua.GetX());
    }

    [Theory]
    [InlineData(0UL, 0x0)]
    [InlineData(0x0080081358008135UL, 0x008)]
    [InlineData(0x00FFFFFFFFFFFFFFUL, 0xFFF)]
    [InlineData(0xFFFFFFFFFFFFFFFFUL, 0xFFF)]
    public void GetZ_ReturnsZ(ulong ua, ushort expect) {
        Assert.Equal(expect, ua.GetZ());
    }

    [Theory]
    [InlineData(0UL, 0x0)]
    [InlineData(0x0080081358008135UL, 0x058)]
    [InlineData(0x00FFFFFFFFFFFFFFUL, 0x0FF)]
    [InlineData(0xFFFFFFFFFFFFFFFFUL, 0x0FF)]
    public void GetY_ReturnsY(ulong ua, byte expect) {
        Assert.Equal(expect, ua.GetY());
    }

    [Theory]
    [InlineData(0UL, 0x0)]
    [InlineData(0x0080081358008135UL, 0x13)]
    [InlineData(0x00FFFFFFFFFFFFFFUL, 0xFF)]
    [InlineData(0xFFFFFFFFFFFFFFFFUL, 0xFF)]
    public void GetG_ReturnsG(ulong ua, byte expect) {
        Assert.Equal(expect, ua.GetG());
    }

    [Theory]
    [InlineData(0UL, 0x0)]
    [InlineData(0x0080081358008135UL, 0x8)]
    [InlineData(0x00FFFFFFFFFFFFFFUL, 0xFFF)]
    [InlineData(0xFFFFFFFFFFFFFFFFUL, 0xFFF)]
    public void GetSs_ReturnsSs(ulong ua, ushort expect) {
        Assert.Equal(expect, ua.GetSs());
    }

    [Theory]
    [InlineData(0UL, 0x0)]
    [InlineData(0x0080081358008135UL, 0x8)]
    [InlineData(0x00FFFFFFFFFFFFFFUL, 0xF)]
    [InlineData(0xFFFFFFFFFFFFFFFFUL, 0xF)]
    public void GetP_ReturnsP(ulong ua, byte expect) {
        Assert.Equal(expect, ua.GetP());
    }
    [Theory]
    [MemberData(nameof(PcUa))]
    public void FromPlayerCoordinates_ProducesExpectedUniversalAddress(PlayerCoordinates pc, ulong ua) {
        ulong coord = UniversalAddress.FromPlayerCoordinates(pc);
        Assert.Equal(ua, coord);
    }
    [Theory]
    [MemberData(nameof(UaPc))]
    public void ToPlayerCoordinates_ProducesExpectedCoordinates(ulong ua, PlayerCoordinates pc) {
        var coord = UniversalAddress.ToPlayerCoordinates(ua);
        Assert.Equal(pc, coord);
    }
    [Theory]
    [MemberData(nameof(GcUa))]
    public void FromGalacticCoordinates_ProducesExpectedUniversalAddress(GalacticCoordinates gc, ulong ua) {
        ulong coord = UniversalAddress.FromGalacticCoordinates(gc);
        Assert.Equal(ua, coord);
    }
    [Theory]
    [MemberData(nameof(UaGc))]
    public void ToGalacticCoordinates_ProducesExpectedCoordinates(ulong ua, GalacticCoordinates gc) {
        var coord = UniversalAddress.ToGalacticCoordinates(ua);
        Assert.Equal(gc, coord);
    }
}
