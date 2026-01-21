using NmsTracker.Domain.VObs.Location;

namespace NmsTracker.DomainTests.VObs.Location;

public class GalacticCoordinatesTest
{
    [Fact]
    public void X_OutOfRange_ThrowsArgumentOutOfRangeException()
    {
        const ushort x = 0x1000;
        var aorEx = Assert.Throws<ArgumentOutOfRangeException>(() => new GalacticCoordinates(x, 0, 0, 0, 0, 0));
        Assert.Equal("x", aorEx.ParamName);
    }

    [Fact]
    public void Z_OutOfRange_ThrowsArgumentOutOfRangeException()
    {
        const ushort z = 0x1000;
        var aorEx = Assert.Throws<ArgumentOutOfRangeException>(() => new GalacticCoordinates(0, z, 0, 0, 0, 0));
        Assert.Equal("z", aorEx.ParamName);
    }

    [Theory]
    [InlineData(0, 0, 0, 0, 0, 0)]
    [InlineData(4095, 4095, 255, 0xF, 0xFFF, 15)]
    public void Constructor_CreatesInstance(ushort x, ushort z, byte y, byte g, ushort ss, byte p)
    {
        var coord = new GalacticCoordinates(x, z, y, g, ss, p);
        Assert.Equal(x, coord.X);
        Assert.Equal(z, coord.Z);
        Assert.Equal(y, coord.Y);
        Assert.Equal(ss, coord.Ss);
        Assert.Equal(p, coord.P);
        Assert.Equal(g, coord.G);
    }

    [Theory]
    [InlineData(4096, 0, 0, 0, 0, 0)] // X out of range
    [InlineData(0, 4096, 0, 0, 0, 0)] // Z out of range
    public void Constructor_InvalidValues_ThrowsArgumentOutOfRangeException(ushort x, ushort z, byte y, byte g, ushort ss, byte p)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new GalacticCoordinates(x, z, y, g, ss, p));
    }

    [Theory]
    [InlineData(0x0, 0x000, 0)]
    [InlineData(0x1, 0x234, 1)]
    [InlineData(0xF, 0x234, 0xF)]
    [InlineData(0xF, 0xFFF, 0xF)]
    public void P_Property_ExtractsPlanet(byte p, ushort ss, byte expect)
    {
        var coord = new GalacticCoordinates(0, 0, 0, 0, ss, p);
        Assert.Equal(expect, coord.P);
    }

    [Theory]
    [InlineData(0x0, 0x000, 0)]
    [InlineData(0x1, 0x234, 0x234)]
    [InlineData(0xF, 0x234, 0x234)]
    [InlineData(0xF, 0xFFF, 0xFFF)]
    public void Ss_Property_ExtractsSolarSystem(byte p, ushort ss, ushort expect)
    {
        var coord = new GalacticCoordinates(0, 0, 0, 0, ss, p);
        Assert.Equal(expect, coord.Ss);
    }
}
