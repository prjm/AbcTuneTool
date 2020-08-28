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
        /// <param name="kind">field kind</param>
        public InformationField(Terminal fieldHeader, Terminal fieldValues, InformationFieldKind kind) {
            Header = fieldHeader;
            Value = fieldValues;
            Kind = kind;
        }

        /// <summary>
        ///     get the matching information field kind
        /// </summary>
        /// <param name="firstChar"></param>
        /// <returns></returns>
        public static InformationFieldKind GetKindFor(char firstChar) =>
            firstChar switch
            {
                'A' => InformationFieldKind.Area,
                'B' => InformationFieldKind.Book,
                'C' => InformationFieldKind.Composer,
                'D' => InformationFieldKind.Discography,
                'F' => InformationFieldKind.FileUrl,
                'G' => InformationFieldKind.Group,
                'H' => InformationFieldKind.History,
                'I' => InformationFieldKind.Instruction,
                'K' => InformationFieldKind.Key,
                'L' => InformationFieldKind.UnitNoteLength,
                'M' => InformationFieldKind.Meter,
                'm' => InformationFieldKind.Macro,
                'N' => InformationFieldKind.Notes,
                'O' => InformationFieldKind.Origin,
                'P' => InformationFieldKind.Parts,
                'Q' => InformationFieldKind.Tempo,
                'R' => InformationFieldKind.Rythm,
                'r' => InformationFieldKind.Remark,
                'S' => InformationFieldKind.Source,
                's' => InformationFieldKind.SymbolLine,
                'T' => InformationFieldKind.TuneTitle,
                'U' => InformationFieldKind.UserDefined,
                'V' => InformationFieldKind.Voice,
                'W' => InformationFieldKind.WordsAfterTune,
                'w' => InformationFieldKind.WordsInTune,
                'X' => InformationFieldKind.RefNumber,
                'Z' => InformationFieldKind.Transcription,
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
