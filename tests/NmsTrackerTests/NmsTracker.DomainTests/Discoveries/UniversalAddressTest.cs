using NmsTracker.Domain.Discoveries;
using NmsTracker.Domain.PlayerState;

namespace NmsTracker.DomainTests.Discoveries;
public class UniversalAddressTest
{
    [Fact]
    public void GetX_ReturnsX_IfXHasValue()
    {
        const ulong universalUlong = 36037676192792885;
        const ushort x = 0x135;
        var ua = new UniversalAddress(universalUlong);
        Assert.Equal(x, ua.X);
    }
    
    [Fact]
    public void GetZ_ReturnsZ_IfZHasValue()
    {
        const ulong universalUlong = 36037676192792885;
        const ushort z = 0x008;
        var ua = new UniversalAddress(universalUlong);
        Assert.Equal(z, ua.Z);
    }
    
    [Fact]
    public void GetY_ReturnsY_IfYHasValue()
    {
        const ulong universalUlong = 36037676192792885;
        const byte y = 0x058;
        var ua = new UniversalAddress(universalUlong);
        Assert.Equal(y, ua.Y);
    }
    
    [Fact]
    public void GetG_ReturnsG_IfGHasValue()
    {
        const ulong universalUlong = 36037676192792885;
        const byte g = 0x013;
        var ua = new UniversalAddress(universalUlong);
        Assert.Equal(g, ua.G);
    }
    
    [Fact]
    public void GetSs_ReturnsSs_IfSsHasValue()
    {
        const ulong universalUlong = 36037676192792885;
        const ushort ss = 0x008;
        var ua = new UniversalAddress(universalUlong);
        Assert.Equal(ss, ua.Ss);
    }
    
    [Fact]
    public void GetP_ReturnsP_IfPHasValue()
    {
        const ulong universalUlong = 36037676192792885;
        const byte p = 0x008;
        var ua = new UniversalAddress(universalUlong);
        Assert.Equal(p, ua.P);
    }

    public static TheoryData<(UniversalAddress, PlayerCoordinates)> PlayerCoordinates =>
    [
        (new UniversalAddress(0UL), new PlayerCoordinates(-2048, -2048, -128, 0, 0, 0)),
        (new UniversalAddress(0x00FFFFFFFFFFFFFFUL), new PlayerCoordinates(2047, 2047, 127, 255, 4095, 15)),
        (new UniversalAddress(0x0080081358008135), new PlayerCoordinates(-1739, -2040, -40, 19, 8, 8))
    ];
    [Theory]
    [MemberData(nameof(PlayerCoordinates))]
    public void ToPlayerCoordinates_ProducesExpectedCoordinates((UniversalAddress ua, PlayerCoordinates pc) coords)
    {
        var pc = coords.ua.ToPlayerCoordinates();
        Assert.Equal(coords.pc, pc);
    }
    
    [Theory]
    [MemberData(nameof(PlayerCoordinates))]
    public void FromPlayerCoordinates_ProducesExpectedAddress((UniversalAddress ua, PlayerCoordinates pc) coords)
    {
        var ua = UniversalAddress.FromPlayerCoordinates(coords.pc);
        Assert.Equal(coords.ua, ua);
    }

    public static TheoryData<(UniversalAddress, GalacticCoordinates)> Coordinates =>
    [
        (new UniversalAddress(0UL), new GalacticCoordinates(2047, 2047, 127, 0)),
        (new UniversalAddress(0x00FFFFFFFFFFFFFFUL), new GalacticCoordinates(2046, 2046, 126, 65535, 255)),
        (new UniversalAddress(0x0080081358008135), new GalacticCoordinates(2356, 2055, 215, 32776, 19))
    ];
    [Theory]
    [MemberData(nameof(Coordinates))]
    public void ToGalacticCoordinates_ProducesExpectedCoordinates((UniversalAddress ua, GalacticCoordinates gc) coords)
    {
        var gc = coords.ua.ToGalacticCoordinates();
        Assert.Equal(coords.gc, gc);
    }
    
    [Theory]
    [MemberData(nameof(Coordinates))]
    public void FromGalacticCoordinates_ProducesExpectedAddress((UniversalAddress ua, GalacticCoordinates gc) coords)
    {
        var ua = UniversalAddress.FromGalacticCoordinates(coords.gc);
        Assert.Equal(coords.ua, ua);
    }
    
    [Fact]
    public void AllZero_UaProperties_ReturnExpected()
    {
        var ua = new UniversalAddress(0UL);
        Assert.All([ua.X, ua.Z, ua.Y, ua.G, ua.Ss, ua.P], v => Assert.Equal(0, v));
    }
    
    [Fact]
    public void AllOnes_UaProperties_ReturnExpected()
    {
        var ua = new UniversalAddress(0x00FFFFFFFFFFFFFFUL);
        Assert.All([ua.X, ua.Z, ua.Ss], v => Assert.Equal(0xFFF, v));
        Assert.All([ua.Y, ua.G], v => Assert.Equal(0xFF, v));
        Assert.Equal(0x0F, ua.P);
    }
    
    [Theory]
    [InlineData(0x0080081358008135UL)]
    [InlineData(0x00FFFFFFFFFFFFFFUL)]
    [InlineData(0x0000000000000000UL)]
    public void RoundTrip_UaProperties_AreEqual(ulong value)
    {
        var ua = new UniversalAddress(value);
        Assert.Equal(value, ua.Ua);
        var gc = ua.ToGalacticCoordinates();
        var ua2 = UniversalAddress.FromGalacticCoordinates(gc);
        Assert.Equal(value, ua2.Ua);
        var pc = ua2.ToPlayerCoordinates();
        var ua3 = UniversalAddress.FromPlayerCoordinates(pc);
        Assert.Equal(value, ua3.Ua);
    }
}