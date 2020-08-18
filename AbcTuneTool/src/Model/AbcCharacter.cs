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
        public AbcCharacter(string value, string originalValue, AbcCharacterKind kind) {
            Value = value;
            OriginalValue = originalValue;
            Kind = kind;
        }

        /// <summary>
        ///     character value
        /// </summary>
        public readonly string Value { get; }

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
