namespace AbcTuneTool.Model.Fields {

    /// <summary>
    ///     voice field
    /// </summary>
    public class VoiceField : InformationField {

        /// <summary>
        ///     create a new voice field
        /// </summary>
        /// <param name="fieldHeader"></param>
        /// <param name="fieldValues"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public VoiceField(Terminal fieldHeader, Terminal fieldValues, string id, string name) : base(fieldHeader, fieldValues, InformationFieldKind.Voice) {
            Id = id;
            Name = name;
        }

        /// <summary>
        ///     voice id
        /// </summary>
        public string Id { get; }

        /// <summary>
        ///     voice name
        /// </summary>
        public string Name { get; }
    }
}
