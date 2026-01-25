namespace NmsTracker.Domain.Entities.PlayerState;

/// <summary> Represents decimal coordinates from the player's current or previous location in the save file. </summary>
public readonly record struct PlayerCoordinates {

    public PlayerCoordinates(short x, short z, sbyte y, byte g, ushort ss, byte p) {
        if (x is > 2047 or < -2048) {
            throw new ArgumentOutOfRangeException(nameof(x), "X coordinate must be in range -2048 to 2047");
        }

        if (z is > 2047 or < -2048) {
            throw new ArgumentOutOfRangeException(nameof(z), "Z coordinate must be in range -2048 to 2047");
        }

        if (ss > 4095) {
            throw new ArgumentOutOfRangeException(nameof(ss), "Solar System Index must be in range 0 to 4095");
        }

        if (p > 15) {
            throw new ArgumentOutOfRangeException(nameof(p), "Planet Index must be in range 0 to 15");
        }
        X = x;
        Z = z;
        Y = y;
        G = g;
        Ss = ss;
        P = p;
    }
    /// <summary> Regional X Coordinate in range -2048 &#8804; value &#8804; 2047 </summary>
    public short X { get; }

    /// <summary> Regional Z Coordinate in range -2048 &#8804; value &#8804; 2047 </summary>
    public short Z { get; }

    /// <summary> Regional Y Coordinate in range -128 &#8804; value &#8804; 127 </summary>
    public sbyte Y { get; }

    /// <summary> Galaxy ID in range 0 &#8804; value &#8804; 255 </summary>
    public byte G { get; }

    /// <summary> Solar System ID in range 0 &#8804; value &#8804; 4095 </summary>
    public ushort Ss { get; }

    /// <summary> Planet ID in range range 0 &#8804; value &#8804; 15 </summary>
    public byte P { get; }
}
