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
                = new InformationFields(ImmutableArray<InformationField>.Empty);

        /// <summary>
        ///     fields
        /// </summary>
        public ImmutableArray<InformationField> Fields { get; }

        /// <summary>
        ///     create a new set of information fields
        /// </summary>
        /// <param name="fields"></param>
        public InformationFields(ImmutableArray<InformationField> fields)
            => Fields = fields;

        /// <summary>
        ///     accept a visitor
        /// </summary>
        /// <param name="visitor"></param>
        public bool Accept(ISyntaxTreeVisitor visitor) {
            var result = visitor.StartVisitNode(this);

            for (var index = 0; index < Fields.Length; index++)
                result &= Fields[index].Accept(visitor);

            result &= visitor.EndVisitNode(this);
            return result;
        }
    }
}
