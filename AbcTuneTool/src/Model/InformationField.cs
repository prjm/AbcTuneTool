using System.Collections.Immutable;
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
        /// <param name="fieldValue"></param>
        public InformationField(AbcCharacterReference field, ImmutableArray<AbcCharacterReference> fieldValue) {
            FieldKind = field;
            FieldValue = fieldValue;
        }

        /// <summary>
        ///     field kind
        /// </summary>
        public AbcCharacterReference FieldKind { get; }

        /// <summary>
        ///     field value
        /// </summary>
        public ImmutableArray<AbcCharacterReference> FieldValue { get; }
    }
}
