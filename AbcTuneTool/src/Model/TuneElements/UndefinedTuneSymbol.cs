namespace AbcTuneTool.Model.TuneElements {

    /// <summary>
    ///     undefined tune symbol
    /// </summary>
    public class UndefinedTuneSymbol : TuneElement {

        /// <summary>
        ///     check for equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(TuneElement? other)
            => other is UndefinedTuneSymbol;

        /// <summary>
        ///     compute a hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
            => 17;
    }
}
