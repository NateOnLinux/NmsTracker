using NmsTracker.Domain;
using NmsTracker.Domain.Discoveries;

namespace NmsTracker.DomainTests;

public class UniversalAddressTest
{
    // 00_8_008_13_58_008_135
    private const ulong UniversalUlong = 36037676192792885;
    private const ulong GcX = 0x934;
    private const ulong GcZ = 0x807;
    private const ulong GcY = 0xD7;
    private const ulong GcPSs = 0x8008;
    
    [Fact]
    public void GetX_ReturnsX_IfXHasValue()
    {
        var ua = new UniversalAddress(UniversalUlong);
        Assert.Equal(0x135, ua.X);
    }

    [Fact]
    public void GetZ_ReturnsZ_IfZHasValue()
    {
        var ua = new UniversalAddress(UniversalUlong);
        Assert.Equal(0x8, ua.Z);
    }

    [Fact]
    public void GetY_ReturnsY_IfYHasValue()
    {
        var ua = new UniversalAddress(UniversalUlong);
        Assert.Equal(0x58, ua.Y);
    }
    
    [Fact]
    public void GetG_ReturnsG_IfGHasValue()
    {
        var ua = new UniversalAddress(UniversalUlong);
        Assert.Equal(0x13, ua.G);
    }

    [Fact]
    public void GetSs_ReturnsSs_IfSsHasValue()
    {
        var ua = new UniversalAddress(UniversalUlong);
        Assert.Equal(0x8, ua.Ss);
    }

    [Fact]
    public void GetP_ReturnsP_IfPHasValue()
    {
        var ua = new UniversalAddress(UniversalUlong);
        Assert.Equal(0x8, ua.P);
    }
    
    [Fact]
    public void ConstructingUniversalAddress_FromKnownFields_ProducesExpectedUlong()
    {
        var ua = new UniversalAddress(x: 0x135, z: 0x008, y: 0x58, g: 0x13, ss: 0x008, p: 0x8);
        Assert.Equal(UniversalUlong, ua.Ua);
    }

    [Fact]
    public void Decode_ProducesGalacticCoordinates()
    {
        var ua = new UniversalAddress(UniversalUlong);
        GalacticCoordinates gCoords = ua.Decode();
        Assert.Equal(GcX, gCoords.X);
        Assert.Equal(GcZ, gCoords.Z);
        Assert.Equal(GcY, gCoords.Y);
        Assert.Equal(GcPSs, gCoords.PSs);
    }
    
    [Fact]
    public void AllZero_UaProperties_ReturnExpected()
    {
        var ua = new UniversalAddress(0UL);
        Assert.All([ua.X, ua.Z, ua.Y, ua.G, ua.Ss, ua.P], v => Assert.Equal(0, v));
        var decoded = ua.Decode();
        Assert.All([decoded.X, decoded.Z], v => Assert.Equal(0x7FF, v));
        Assert.Equal(0x7F,  decoded.Y);
        Assert.Equal(0, decoded.PSs);
    }

    [Fact]
    public void AllOnes_UaProperties_ReturnExpected()
    {
        var ua = new UniversalAddress(0x00FFFFFFFFFFFFFFUL);
        Assert.All([ua.X, ua.Z, ua.Ss], v => Assert.Equal(0xFFF, v));
        Assert.All([ua.Y, ua.G], v => Assert.Equal(0xFF, v));
        Assert.Equal(0x0F, ua.P);
        var decoded = ua.Decode();
        Assert.All([decoded.X, decoded.Z], v => Assert.Equal(0x7FE, v));
        Assert.Equal(0x7E, decoded.Y);
        Assert.Equal(0xFFFF, decoded.PSs);
    }
}
