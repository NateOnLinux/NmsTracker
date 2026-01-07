using NmsTracker.Domain.Discoveries;

namespace NmsTracker.DomainTests.Discoveries;
public class GalacticCoordinatesTest
{
    [Fact]
    public void X_OutOfRange_ThrowsArgumentOutOfRangeException()
    {
        const ushort x = 0x1000;
        var aorEx = Assert.Throws<ArgumentOutOfRangeException>(() => new GalacticCoordinates(x, 0, 0, 0));
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

    public static TheoryData<(UniversalAddress, GalacticCoordinates)> Coordinates =>
    [
        (new UniversalAddress(0UL), new GalacticCoordinates(2047, 2047, 127, 0)),
        (new UniversalAddress(0x00FFFFFFFFFFFFFFUL), new GalacticCoordinates(2046, 2046, 126, 65535, 255)),
        (new UniversalAddress(0x0080081358008135), new GalacticCoordinates(2356, 2055, 215, 32776, 19))
    ];
    [Theory]
    [MemberData(nameof(Coordinates))]
    public void FromUniversalAddress_ProducesExpectedCoordinates((UniversalAddress ua, GalacticCoordinates gc) coords)
    {
        var coord = GalacticCoordinates.FromUniversalAddress(coords.ua);
        Assert.Equal(coords.gc, coord);
    }
}