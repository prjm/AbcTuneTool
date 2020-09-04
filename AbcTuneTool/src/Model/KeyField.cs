using System;
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
        /// <param name="value"></param>
        /// <param name="notes">used key notes</param>
        public KeyField(Terminal header, Terminal value, KeyNotes notes) : base(header, value, InformationFieldKind.Key) {
            var note = value.FirstChar;
            var accidental = value.SecondChar.AsAccidental();
            var mode = value.GetValueAfterWhitespace(1);

            if (value.IsEmpty || value.Matches(KnownStrings.None, StringComparison.OrdinalIgnoreCase) || value.IsWhitespace)
                KeyValue = Note.None;
            else
                KeyValue = notes.ForKeySignature(note, accidental, mode);
        }

        /// <summary>
        ///     key value
        /// </summary>
        public Note KeyValue { get; }
    }
}
