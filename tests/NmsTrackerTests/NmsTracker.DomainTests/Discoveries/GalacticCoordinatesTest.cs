using NmsTracker.Domain.Discoveries;

namespace NmsTracker.DomainTests.Discoveries;
public class GalacticCoordinatesTest
{
    [Fact]
    public void X_OutOfRange_ThrowsArgumentOutOfRangeException()
    {
        const ushort x = 0x1000;
        var aorEx = Assert.Throws<ArgumentOutOfRangeException>(() => new GalacticCoordinates(4096, 0, 0, 0));
        Assert.Equal("X", aorEx.ParamName);
    }

    [Fact]
    public void Z_OutOfRange_ThrowsArgumentOutOfRangeException()
    {
        const ushort z = 0x1000;
        var aorEx = Assert.Throws<ArgumentOutOfRangeException>(() => new GalacticCoordinates(0, z, 0, 0));
        Assert.Equal("Z", aorEx.ParamName);
    }
    
    [Theory]
    [InlineData(0, 0, 0, 0, 0)]
    [InlineData(4095, 4095, 255, 0xFFFF, 255)]
    public void Constructor_CreatesInstance(ushort x, ushort z, byte y, ushort ps, byte g)
    {
        var coord = new GalacticCoordinates(x, z, y, ps, g);
        Assert.Equal(x, coord.X);
        Assert.Equal(z, coord.Z);
        Assert.Equal(y, coord.Y);
        Assert.Equal(ps, coord.PSs);
        Assert.Equal(g, coord.G);
    }

    [Fact]
    public void FromUniversalAddress_ProducesExpectedCoordinates()
    {
        const ulong value = 36037676192792885UL;
        var coord = GalacticCoordinates.FromUniversalAddress(new UniversalAddress(value));
        
        // Expected values
        const ushort x = 0x934;
        const ushort z = 0x807;
        const byte y = 0xD7;
        const byte g = 0x013;
        const ushort ss = 0x008;
        const byte p = 0x008;
        const ushort pss = (ss & 0x0FFF) | ((p & 0x0F) << 12);
        
        Assert.Equal(x, coord.X);
        Assert.Equal(z, coord.Z);
        Assert.Equal(y, coord.Y);
        Assert.Equal(g, coord.G);
        Assert.Equal(pss, coord.PSs);
    }
}