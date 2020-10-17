using AbcTuneTool.Model.Symbolic;

namespace AbcTuneTool.Model.Fields {

    /// <summary>
    ///     voice field
    /// </summary>
    public class VoiceField : ClefField {

        /// <summary>
        ///     create a new voice field
        /// </summary>
        /// <param name="fieldHeader"></param>
        /// <param name="fieldValues"></param>
        /// <param name="id"></param>
        /// <param name="subname"></param>
        /// <param name="name"></param>
        /// <param name="stem"></param>
        /// <param name="clef"></param>
        public VoiceField(Terminal fieldHeader, Terminal fieldValues, string id, string name, string subname, StemDirection stem, ClefSettings clef) : base(fieldHeader, fieldValues, InformationFieldKind.Voice, clef) {
            Id = id;
            Name = name;
            Subname = subname;
            StemDirection = stem;
        }

        /// <summary>
        ///     voice id
        /// </summary>
        public string Id { get; }

        /// <summary>
        ///     voice name
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     sub name
        /// </summary>
        public string Subname { get; }

        /// <summary>
        ///     stem direction
        /// </summary>
        public StemDirection StemDirection { get; }
    }
}
