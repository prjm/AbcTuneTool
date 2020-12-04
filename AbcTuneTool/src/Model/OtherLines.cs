
using AbcTuneTool.Model.TuneElements;

namespace AbcTuneTool.Model {


    /// <summary>
    ///     other lines
    /// </summary>
    public class OtherLines : ISyntaxTreeElement {

        /// <summary>
        ///     create a new set of other lines
        /// </summary>
        /// <param name="lines"></param>
        public OtherLines(Terminal lines)
            => Lines = lines;

        /// <summary>
        ///     lines
        /// </summary>
        public Terminal Lines { get; }

        /// <summary>
        ///  accept a visitor
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        public bool Accept(ISyntaxTreeVisitor visitor)
            => visitor.StartVisitNode(this) && Lines.Accept(visitor) && visitor.EndVisitNode(this);
    }
}
