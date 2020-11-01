using AbcTuneTool.Model;
using AbcTuneTool.Model.Fields;

namespace AbcTuneTool.src.Model.Fields {

    /// <summary>
    ///     reference number field
    /// </summary>
    public class ReferenceNumberField : InformationField {

        /// <summary>
        ///     create a new reference number field
        /// </summary>
        /// <param name="fieldHeader"></param>
        /// <param name="fieldValues"></param>
        public ReferenceNumberField(Terminal fieldHeader, Terminal fieldValues) : base(fieldHeader, fieldValues, InformationFieldKind.RefNumber) {
            if (fieldValues.Length > 0 && ulong.TryParse(fieldValues[0], out var number))
                Number = number;
            else
                Number = default;
        }

        /// <summary>
        ///     number
        /// </summary>
        public ulong? Number { get; }
    }
}
