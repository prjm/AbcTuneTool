namespace AbcTuneTool.Model
{

    /// <summary>
    ///     ABC character
    /// </summary>
    public readonly struct AbcCharacter
    {

        /// <summary>
        ///     create a new ABC character
        /// </summary>
        /// <param name="kind"></param>
        /// <param name="value"></param>
        public AbcCharacter(AbcCharacterKind kind, char value)
        {
            Value = value;
            Kind = kind;
        }

        /// <summary>
        ///     character value
        /// </summary>
        public readonly char Value { get; }

        /// <summary>
        ///     character kind
        /// </summary>
        public readonly AbcCharacterKind Kind { get; }

    }
}
