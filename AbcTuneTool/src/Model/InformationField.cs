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
            FieldKind = fieldHeader;
            FieldValue = fieldValues;
        }

        /// <summary>
        ///     field header
        /// </summary>
        public Terminal FieldKind { get; }

        /// <summary>
        ///     field values
        /// </summary>
        public Terminal FieldValue { get; }
    }
}
