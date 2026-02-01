namespace NmsTracker.Domain.Utils;

public static class CoordinatesHelper {

    private static readonly Lazy<byte[]> UAtoGC8 =
        new(() => Enumerable.Range(0, 256).Select(n => unchecked((byte)(n + 127))).ToArray());

    private static readonly Lazy<sbyte[]> GCtoUA8 =
        new(() => Enumerable.Range(0, 256).Select(n => unchecked((sbyte)(n - 127))).ToArray());

    private static readonly Lazy<ushort[]> UAtoGC12 =
        new(() => Enumerable.Range(0, 4096).Select(n => (ushort)((n + 2047) & 0x0FFF)).ToArray());

    private static readonly Lazy<short[]> GCtoUA12 =
        new(() => Enumerable.Range(0, 4096).Select(n => (short)((n - 2047) & 0x0FFF)).ToArray());

    /// <summary>
    ///     Lookup table for converting Universal Address to Galactic Coordinates.
    ///     (n + 127) mod 256
    /// </summary>
    /// <remarks>
    ///     <list type="table">
    ///         <listheader>
    ///             <term> GC </term> <description> UA </description>
    ///         </listheader>
    ///         <item>
    ///             <term> 0x00 </term><description> 0x81 </description>
    ///         </item>
    ///         <item>
    ///             <term> … </term><description> … </description>
    ///         </item>
    ///         <item>
    ///             <term> 0x7E </term><description> 0xFF </description>
    ///         </item>
    ///         <item>
    ///             <term> 0x7F </term><description> 0x00 </description>
    ///         </item>
    ///         <item>
    ///             <term> 0x80 </term><description> 0x01 </description>
    ///         </item>
    ///         <item>
    ///             <term> … </term><description> … </description>
    ///         </item>
    ///         <item>
    ///             <term> 0xFE </term><description> 0x7F </description>
    ///         </item>
    ///     </list>
    /// </remarks>
    public static ReadOnlySpan<byte> UAtoGCLut8 => UAtoGC8.Value;

    /// <summary>
    ///     Lookup table for converting Galactic Coordinates to Universal Address.
    ///     (n - 127) mod 256
    /// </summary>
    /// <remarks>
    ///     <list type="table">
    ///         <listheader>
    ///             <term> GC </term> <description> UA </description>
    ///         </listheader>
    ///         <item>
    ///             <term> 0x00 </term><description> 0x81 </description>
    ///         </item>
    ///         <item>
    ///             <term> … </term><description> … </description>
    ///         </item>
    ///         <item>
    ///             <term> 0x7E </term><description> 0xFF </description>
    ///         </item>
    ///         <item>
    ///             <term> 0x7F </term><description> 0x00 </description>
    ///         </item>
    ///         <item>
    ///             <term> 0x80 </term><description> 0x01 </description>
    ///         </item>
    ///         <item>
    ///             <term> … </term><description> … </description>
    ///         </item>
    ///         <item>
    ///             <term> 0xFE </term><description> 0x7F </description>
    ///         </item>
    ///     </list>
    /// </remarks>
    public static ReadOnlySpan<sbyte> GCtoUALut8 => GCtoUA8.Value;

    /// <summary> Lookup table for converting Universal Address to Galactic Coordinates. (n + 2047) mod 4096 </summary>
    /// <remarks>
    ///     <list type="table">
    ///         <listheader>
    ///             <term> GC </term> <description> UA </description>
    ///         </listheader>
    ///         <item>
    ///             <term> 0x000 </term><description> 0x801 </description>
    ///         </item>
    ///         <item>
    ///             <term> … </term><description> … </description>
    ///         </item>
    ///         <item>
    ///             <term> 0x7FE </term><description> 0xFFF </description>
    ///         </item>
    ///         <item>
    ///             <term> 0x7FF </term><description> 0x000 </description>
    ///         </item>
    ///         <item>
    ///             <term> 0x800 </term><description> 0x001 </description>
    ///         </item>
    ///         <item>
    ///             <term> … </term><description> … </description>
    ///         </item>
    ///         <item>
    ///             <term> 0xFFE </term><description> 0x7FF </description>
    ///         </item>
    ///     </list>
    /// </remarks>
    public static ReadOnlySpan<ushort> UAtoGCLut12 => UAtoGC12.Value;

    /// <summary> Lookup table for converting Galactic Coordinates to Universal Address. (n - 2047) mod 4096 </summary>
    /// <remarks>
    ///     <list type="table">
    ///         <listheader>
    ///             <term> GC </term> <description> UA </description>
    ///         </listheader>
    ///         <item>
    ///             <term> 0x000 </term><description> 0x801 </description>
    ///         </item>
    ///         <item>
    ///             <term> … </term><description> … </description>
    ///         </item>
    ///         <item>
    ///             <term> 0x7FE </term><description> 0xFFF </description>
    ///         </item>
    ///         <item>
    ///             <term> 0x7FF </term><description> 0x000 </description>
    ///         </item>
    ///         <item>
    ///             <term> 0x800 </term><description> 0x001 </description>
    ///         </item>
    ///         <item>
    ///             <term> … </term><description> … </description>
    ///         </item>
    ///         <item>
    ///             <term> 0xFFE </term><description> 0x7FF </description>
    ///         </item>
    ///     </list>
    /// </remarks>
    public static ReadOnlySpan<short> GCtoUALut12 => GCtoUA12.Value;
}
