namespace AbcTuneTool.Model {

    /// <summary>
    ///     ABC character
    /// </summary>
    public readonly struct AbcCharacter {

        /// <summary>
        ///     create a new ABC character
        /// </summary>
        /// <param name="kind">token kind</param>
        /// <param name="value">token value</param>
        /// <param name="originalValue">original value</param>
        public AbcCharacter(AbcCharacterKind kind, char value, string originalValue) {
            Value = value;
            OriginalValue = originalValue;
            Kind = kind;
        }

        /// <summary>
        ///     character value
        /// </summary>
        public readonly char Value { get; }

        /// <summary>
        ///     original value
        /// </summary>
        public readonly string OriginalValue { get; }

        /// <summary>
        ///     character kind
        /// </summary>
        public readonly AbcCharacterKind Kind { get; }

    }
}
