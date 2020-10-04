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
        /// <param name="alias"></param>
        /// <param name="symbol"></param>
        public UserDefinedField(Terminal fieldHeader, Terminal fieldValues, string alias, TuneElement? symbol) : base(fieldHeader, fieldValues, InformationFieldKind.UserDefined) {
            Alias = alias;
            Symbol = symbol;
        }

        /// <summary>
        ///     alias name
        /// </summary>
        public string Alias { get; }

        /// <summary>
        ///     alias value
        /// </summary>
        public TuneElement? Symbol { get; }
    }
}
