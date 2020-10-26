using AbcTuneTool.Model.KeyTables;
using AbcTuneTool.Model.Symbolic;

namespace AbcTuneTool.Model.Fields {

    /// <summary>
    ///     key field
    /// </summary>
    public class KeyField : ClefField {

        /// <summary>
        ///     create a new key field
        /// </summary>
        /// <param name="header"></param>
        /// <param name="value"></param>
        /// <param name="clef"></param>
        /// <param name="isValid"></param>
        /// <param name="table"></param>
        public KeyField(Terminal header, Terminal value, ClefSettings clef, KeyTable table, KeyStatus isValid) : base(header, value, InformationFieldKind.Key, clef) {
            IsValidKey = isValid;
            KeyValue = table;
        }

        /// <summary>
        ///     <c>true</c> if this is a valid key
        /// </summary>
        public KeyStatus IsValidKey { get; }

        /// <summary>
        ///     key value
        /// </summary>
        public KeyTable KeyValue { get; }

    }
}
