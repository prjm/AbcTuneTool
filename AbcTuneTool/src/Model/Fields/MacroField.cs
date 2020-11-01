namespace AbcTuneTool.Model.Fields {

    /// <summary>
    ///     macro definition
    /// </summary>
    public class MacroField : InformationField {

        /// <summary>
        ///     create a new macro field
        /// </summary>
        /// <param name="header"></param>
        /// <param name="fieldValues"></param>
        public MacroField(Terminal header, Terminal fieldValues) : base(header, fieldValues, InformationFieldKind.Macro) {
        }
    }
}
