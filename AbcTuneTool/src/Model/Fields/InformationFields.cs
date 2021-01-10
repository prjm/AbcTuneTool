using System.Collections.Immutable;

using AbcTuneTool.Model.TuneElements;

namespace AbcTuneTool.Model.Fields {

    /// <summary>
    ///     set of information fields
    /// </summary>
    public class InformationFields : ISyntaxTreeElement {

        /// <summary>
        ///     empty set of fields
        /// </summary>
        public static readonly InformationFields Empty
                = new InformationFields(ImmutableArray<InformationField>.Empty, new Terminal(new Token()));

        /// <summary>
        ///     fields
        /// </summary>
        public ImmutableArray<InformationField> Fields { get; }

        /// <summary>
        ///     separator
        /// </summary>
        public Terminal Line { get; }

        /// <summary>
        ///     create a new set of information fields
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="line">separating line</param>
        public InformationFields(ImmutableArray<InformationField> fields, Terminal line) {
            Fields = fields;
            Line = line;
        }

        /// <summary>
        ///     accept a visitor
        /// </summary>
        /// <param name="visitor"></param>
        public bool Accept(ISyntaxTreeVisitor visitor) {
            var result = visitor.StartVisitNode(this);

            for (var index = 0; index < Fields.Length; index++)
                result &= Fields[index].Accept(visitor);

            Line.Accept(visitor);

            result &= visitor.EndVisitNode(this);
            return result;
        }
    }
}
