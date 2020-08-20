using AbcTuneTool.FileIo;

namespace AbcTuneTool.Model {

    /// <summary>
    ///     information field
    /// </summary>
    public class InformationField {

        /// <summary>
        ///     create a new information field
        /// </summary>
        /// <param name="field"></param>
        public InformationField(AbcCharacterReference field)
            => FieldKind = field;

        /// <summary>
        ///     field kind
        /// </summary>
        public AbcCharacterReference FieldKind { get; }
    }
}
