namespace AbcTuneTool.Model.TuneElements {

    /// <summary>
    ///     special version comment
    /// </summary>
    public class VersionComment : ISyntaxTreeElement {

        /// <summary>
        ///     version
        /// </summary>
        public string Version { get; }

        /// <summary>
        ///     value
        /// </summary>
        public Terminal Terminal { get; }

        /// <summary>
        ///     create a new version comment
        /// </summary>
        /// <param name="value"></param>
        /// <param name="versionName"></param>
        public VersionComment(Terminal value, string versionName) {
            Terminal = value;
            Version = versionName;
        }

        /// <summary>
        ///     accept a visitor
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        public bool Accept(ISyntaxTreeVisitor visitor) =>
            visitor.StartVisitNode(this) &&
            Terminal.Accept(visitor) &&
            visitor.EndVisitNode(this);

    }
}
