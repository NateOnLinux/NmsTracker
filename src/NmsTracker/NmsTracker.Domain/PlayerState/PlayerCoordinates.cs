namespace NmsTracker.Domain.PlayerState;

/// <summary>
/// Represents decimal coordinates from the player's current or previous location in the save file.
/// </summary>
public readonly record struct PlayerCoordinates
{
    private readonly short _x;
    
    /// <summary>
    /// Regional X Coordinate in range -2048 &#8804; value &#8804; 2047
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Provided value is out of range</exception>
    public short X
    {
        get => _x;
        private init
        {
            if (value is < -2048 or > 2047)
                throw new ArgumentOutOfRangeException(nameof(value));
            _x = value;
        }
    }
    
    private readonly short _z;
    
    /// <summary>
    /// Regional Z Coordinate in range -2048 &#8804; value &#8804; 2047
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Provided value is out of range</exception>
    public short Z
    {
        get => _z;
        private init
        {
            if (value is < -2048 or > 2047)
                throw new ArgumentOutOfRangeException(nameof(value));
            _z = value;
        }
    }
    
    /// <summary>
    /// Regional Y Coordinate in range -128 &#8804; value &#8804; 127
    /// </summary>
    public sbyte Y { get; private init; }
    
    /// <summary>
    /// Galaxy ID in range 0 &#8804; value &#8804; 255
    /// </summary>
    public byte G { get; private init; }

    private readonly ushort _ss;
    /// <summary>
    /// Solar System ID in range 0 &#8804; value &#8804; 4095
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Provided value is out of range</exception>
    public ushort Ss
    {
        get => _ss;
        private init
        {
            if (value > 4095)
                throw new ArgumentOutOfRangeException(nameof(value));
            _ss = value;
        }
    }
    
    private readonly byte _p;
    /// <summary>
    /// Planet ID in range range 0 &#8804; value &#8804; 15
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public byte P
    {
        get => _p;
        private init
        {
            if (value > 15)
                throw new ArgumentOutOfRangeException(nameof(value));
            _p = value;
        }
    }

    public PlayerCoordinates(short x, short z, sbyte y, byte g, ushort ss, byte p)
    {
        X = x;
        Z = z;
        Y = y;
        G = g;
        Ss = ss;
        P = p;
    }
}
