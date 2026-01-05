using NmsTracker.Domain.PlayerState;
using Xunit.Internal;

namespace NmsTracker.DomainTests.PlayerState;

public class PlayerCoordinatesTest
{
    [Theory]
    [InlineData(-2049)]
    [InlineData(2048)]
    public void X_OutOfRange_ThrowsArgumentOutOfRangeException(short x)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new PlayerCoordinates(x, 0, 0, 0, 0, 0));
    }

    [Theory]
    [InlineData(-2049)]
    [InlineData(2048)]
    public void Z_OutOfRange_ThrowsArgumentOutOfRangeException(short z)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new PlayerCoordinates(0, z, 0, 0, 0, 0));
    }

    [Fact]
    public void Ss_OutOfRange_ThrowsArgumentOutOfRangeException()
    {
        const ushort ss = 4096;
        Assert.Throws<ArgumentOutOfRangeException>(() => new PlayerCoordinates(0, 0, 0, 0, ss, 0));
    }

    [Fact]
    public void P_OutOfRange_ThrowsArgumentOutOfRangeException()
    {
        const byte p = 16;
        Assert.Throws<ArgumentOutOfRangeException>(() => new PlayerCoordinates(0, 0, 0, 0, 0, p));
    }

    [Theory]
    [InlineData(-2048, -2048, -128, 0, 0, 0)]
    [InlineData(2047, 2047, 127, 255, 4095, 15)]
    public void Constructor_CreatesInstance(short x, short z, sbyte y, byte g, ushort ss, byte p)
    {
        var pc = new PlayerCoordinates(x, z, y, g, ss, p);
        Assert.Equal(x, pc.X);
        Assert.Equal(z, pc.Z);
        Assert.Equal(y, pc.Y);
        Assert.Equal(g, pc.G);
        Assert.Equal(ss, pc.Ss);
        Assert.Equal(p, pc.P);
    }
}