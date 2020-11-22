namespace AbcTuneTool.Model.TuneElements {

    /// <summary>
    ///     basic interface for syntax tree elements
    /// </summary>
    public interface ISyntaxTreeElement {

        /// <summary>
        ///     accent a visitor
        /// </summary>
        /// <param name="visitor"></param>
        public bool Accept(ISyntaxTreeVisitor visitor);

    }
}
