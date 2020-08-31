using AbcTuneTool.Common;

namespace AbcTuneTool.Model {

    /// <summary>
    ///     key field
    /// </summary>
    public class KeyField : InformationField {

        /// <summary>
        ///     create a new key field
        /// </summary>
        /// <param name="header"></param>
        /// <param name="terminal"></param>
        /// <param name="notes">used key notes</param>
        public KeyField(Terminal header, Terminal terminal, KeyNotes notes) : base(header, terminal, InformationFieldKind.Key) {
            var note = terminal.FirstChar;
            var accidental = terminal.SecondChar.AsAccidental();
            var mode = terminal.GetValueAfterWhitespace(1);
            KeyValue = notes.ForKeySignature(note, accidental, mode);
        }

        /// <summary>
        ///     key value
        /// </summary>
        public Note KeyValue { get; }
    }
}
