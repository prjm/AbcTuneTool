namespace AbcTuneTool.Model {

    /// <summary>
    ///     user defined shortcut field
    /// </summary>
    public class UserDefinedShortcutFields : InformationField {

        /// <summary>
        ///     create a new shortcut field
        /// </summary>
        /// <param name="fieldHeader"></param>
        /// <param name="fieldValues"></param>
        public UserDefinedShortcutFields(Terminal fieldHeader, Terminal fieldValues) : base(fieldHeader, fieldValues, InformationFieldKind.UserDefined) {
        }

        /// <summary>
        ///     apply shortcuts
        /// </summary>
        /// <param name="shortcuts"></param>
        public void Apply(SymbolShortcuts shortcuts) {

        }

    }
}
