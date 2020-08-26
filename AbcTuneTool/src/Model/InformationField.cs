namespace AbcTuneTool.Model {

    /// <summary>
    ///     information field
    /// </summary>
    public class InformationField {

        /// <summary>
        ///     create a new information field
        /// </summary>
        /// <param name="fieldHeader">header value</param>
        /// <param name="fieldValues">header values</param>
        public InformationField(Terminal fieldHeader, Terminal fieldValues) {
            Header = fieldHeader;
            Value = fieldValues;
            Kind = GetKindFor(fieldHeader.FirstChar);
        }

        private static InformationFieldKind GetKindFor(char firstChar) =>
            firstChar switch
            {
                'A' => InformationFieldKind.Area,
                'B' => InformationFieldKind.Book,
                'C' => InformationFieldKind.Composer,
                'D' => InformationFieldKind.Discography,
                'F' => InformationFieldKind.FileUrl,
                'G' => InformationFieldKind.Group,
                'H' => InformationFieldKind.History,
                _ => InformationFieldKind.Undefined,
            };

        /// <summary>
        ///     field header
        /// </summary>
        public Terminal Header { get; }

        /// <summary>
        ///     field values
        /// </summary>
        public Terminal Value { get; }

        /// <summary>
        ///     field kind
        /// </summary>
        public InformationFieldKind Kind { get; }
    }
}
