using AbcTuneTool.Model.TuneElements;

namespace AbcTuneTool.Model.Fields {

    /// <summary>
    ///     create a new user defined field
    /// </summary>
    public class UserDefinedField : InformationField {

        /// <summary>
        ///     create a new user defined field
        /// </summary>
        /// <param name="fieldHeader"></param>
        /// <param name="fieldValues"></param>
        /// <param name="symbol"></param>
        public UserDefinedField(Terminal fieldHeader, Terminal fieldValues, TuneElement? symbol) : base(fieldHeader, fieldValues, InformationFieldKind.UserDefined) {
            Symbol = symbol;
        }

        public TuneElement? Symbol { get; }
    }
}
