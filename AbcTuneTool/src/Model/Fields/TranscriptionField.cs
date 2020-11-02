using AbcTuneTool.Model;
using AbcTuneTool.Model.Fields;

namespace AbcTuneTool.src.Model.Fields {

    /// <summary>
    ///     transcription field
    /// </summary>
    public class TranscriptionField : InformationField {

        /// <summary>
        ///     create a new transcription field
        /// </summary>
        /// <param name="fieldHeader"></param>
        /// <param name="fieldValues"></param>
        public TranscriptionField(Terminal fieldHeader, Terminal fieldValues) : base(fieldHeader, fieldValues, InformationFieldKind.Transcription) {

            var kind = fieldValues.Length > 0 ? fieldValues[0] : string.Empty;

            if (string.Equals(kind, KnownStrings.AbcTranscription, System.StringComparison.OrdinalIgnoreCase))
                TranscriptionKind = TranscriptionKind.Transcription;

            else if (string.Equals(kind, KnownStrings.AbcEditedBy, System.StringComparison.OrdinalIgnoreCase))
                TranscriptionKind = TranscriptionKind.Edited;

            else if (string.Equals(kind, KnownStrings.AbcCopyright, System.StringComparison.OrdinalIgnoreCase))
                TranscriptionKind = TranscriptionKind.Copyright;

        }

        /// <summary>
        ///     kind
        /// </summary>
        public TranscriptionKind TranscriptionKind { get; }
    }
}
