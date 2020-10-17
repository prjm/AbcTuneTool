using System.Collections.Immutable;

namespace AbcTuneTool.Model.Fields {

    /// <summary>
    ///     set of information fields
    /// </summary>
    public class InformationFields {

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
    }
}
