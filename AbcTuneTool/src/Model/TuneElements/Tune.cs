using AbcTuneTool.Model.Fields;

namespace AbcTuneTool.Model.TuneElements {

    /// <summary>
    ///     tune
    /// </summary>
    public class Tune : ISyntaxTreeElement {

        /// <summary>
        ///     create a new tune
        /// </summary>
        /// <param name="header"></param>
        /// <param name="otherLines"></param>
        /// <param name="tuneBody">body</param>
        public Tune(OtherLines otherLines, InformationFields header, TuneBody tuneBody) {
            OtherLines = otherLines;
            Header = header;
            Body = tuneBody;
        }

        /// <summary>
        ///     field header
        /// </summary>
        public InformationFields Header { get; }

        /// <summary>
        ///     body
        /// </summary>
        public TuneBody Body { get; }

        /// <summary>
        ///     other lines before the header
        /// </summary>
        public OtherLines OtherLines { get; }

        public bool Accept(ISyntaxTreeVisitor visitor) {
            var result = visitor.StartVisitNode(this);
            result &= OtherLines.Accept(visitor);
            result &= Header.Accept(visitor);
            result &= Body.Accept(visitor);
            result &= visitor.EndVisitNode(this);
            return result;
        }
    }
}
