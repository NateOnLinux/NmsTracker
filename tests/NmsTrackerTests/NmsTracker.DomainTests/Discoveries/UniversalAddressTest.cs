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
    
    [Theory]
    [InlineData(-2048, -2048, -128, 0, 0, 0, 0x0000000000000000UL)]
    [InlineData(2047, 2047, 127, 255, 4095, 15, 0x00FFFFFFFFFFFFFFUL)]
    [InlineData(-1739, -2040, -40, 19, 8, 8, 0x0080081358008135UL)]
    public void FromPlayerCoordinates_ProducesExpectedUA(short x, short z, sbyte y, byte g, ushort ss, byte p, ulong expected)
    {
        var pc = new PlayerCoordinates(x, z, y, g, ss, p);
        var ua = UniversalAddress.FromPlayerCoordinates(pc);
        Assert.Equal(expected, ua.Ua);
    }
    
    [Fact]
    public void ToGalacticCoordinates_ProducesGalacticCoordinates()
    {
        const ulong universalUlong = 36037676192792885;
        var ua = new UniversalAddress(universalUlong);
        var gCoords = ua.ToGalacticCoordinates();
        const ushort gX = 0x934;
        const ushort gZ = 0x807;
        const byte gY = 0xD7;
        const ushort gPSs = 0x8008;
        const byte gG = 0x13;
        Assert.Equal(gX, gCoords.X);
        Assert.Equal(gZ, gCoords.Z);
        Assert.Equal(gY, gCoords.Y);
        Assert.Equal(gPSs, gCoords.PSs);
        Assert.Equal(gG, gCoords.G);
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
}