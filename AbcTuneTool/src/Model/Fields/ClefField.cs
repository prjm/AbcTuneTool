using AbcTuneTool.Model.Symbolic;

namespace AbcTuneTool.Model.Fields {

    /// <summary>
    ///     field with clefs
    /// </summary>
    public class ClefField : InformationField {

        /// <summary>
        ///     create a new clef field
        /// </summary>
        /// <param name="fieldHeader"></param>
        /// <param name="fieldValues"></param>
        /// <param name="kind"></param>
        /// <param name="clef"></param>
        protected ClefField(Terminal fieldHeader, Terminal fieldValues, InformationFieldKind kind, ClefSettings clef) : base(fieldHeader, fieldValues, kind)
            => Clef = clef;

        /// <summary>
        ///     clef
        /// </summary>
        public ClefSettings Clef { get; }


    }
}
