using AbcTuneTool.Model.Fields;
using AbcTuneTool.Model.Symbolic;

namespace AbcTuneTool.Model {

    /// <summary>
    ///     note length field
    /// </summary>
    public class LengthField : InformationField {

        /// <summary>
        ///     create a new length field
        /// </summary>
        /// <param name="header"></param>
        /// <param name="value"></param>
        public LengthField(Terminal header, Terminal value) : base(header, value, InformationFieldKind.UnitNoteLength)
            => Length = ParseFraction(Value.GetValueAfterWhitespace(0));

        /// <summary>
        ///     given length
        /// </summary>
        public Fraction Length { get; }

    }
}
