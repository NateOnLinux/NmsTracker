namespace NmsTracker.Domain.VObs.Location;

/// <summary>
/// See the No Man's Sky Wiki for more information about
/// <see href="https://nomanssky.miraheze.org/wiki/Galactic_Coordinates"> Galactic Coordinates </see>
/// </summary>
public readonly record struct GalacticCoordinates {

    /// <summary>
    /// Represents the decoded representation of the Universal Address<br /> The decoded coordinates are in the range
    /// X,Z: 0-4096, Y: 0-255, PSs: 4 bit Planet + 12 bit SolarSystem Index
    /// </summary>
    private GalacticCoordinates(ushort x, ushort z, byte y, ushort pSs, byte g) {
        if (x > 4095) {
            throw new ArgumentOutOfRangeException(nameof(x), x, "X coordinate must be in range 0 to 4095");
        }

        if (z > 4095) {
            throw new ArgumentOutOfRangeException(nameof(z), z, "Z coordinate must be in range 0 to 4095");
        }
        X = x;
        Z = z;
        Y = y;
        PSs = pSs;
        G = g;
    }

    public GalacticCoordinates(ushort x, ushort z, byte y, byte g, ushort ss, byte p) : this(x, z, y,
        (ushort)((ss & 0x0FFF) | ((p & 0xF) << 12)), g) {
        if (ss > 4095) {
            throw new ArgumentOutOfRangeException(nameof(ss), "Solar System Index must be in range 0 to 4095");
        }

        if (p > 15) {
            throw new ArgumentOutOfRangeException(nameof(p), p, "Planet Index must be in range 0 to 15");
        }
    }
    /// <summary> Regional X Coordinate in range 0 &#8804; value &#8804; 4095 </summary>
    public ushort X { get; }

    /// <summary> Regional Z Coordinate in range 0 &#8804; value &#8804; 4095 </summary>
    public ushort Z { get; }

    /// <summary> Regional Y Coordinate in range 0 &#8804; value &#8804; 255 </summary>
    public byte Y { get; }

    /// <summary>
    /// 4 Bit Planet Index and 12 Bit Solar System Index<br /> See the No Man's Sky Wiki for more information about
    /// <see href="https://nomanssky.miraheze.org/wiki/Galactic_Coordinates"> Galactic Coordinates </see><br />
    /// </summary>
    public ushort PSs { get; }

    /// <summary> Galaxy ID in range 0 &#8804; value &#8804; 255 </summary>
    public byte G { get; }

    /// <summary> Planet part extracted from <see cref="PSs" /> </summary>
    public byte P => (byte)(PSs >> 12);

    /// <summary> Solar System part extracted from <see cref="PSs" /> </summary>
    public ushort Ss => (ushort)(PSs & 0x0FFF);
}
