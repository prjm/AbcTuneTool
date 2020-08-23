using System.Collections.Immutable;

namespace AbcTuneTool.Model {

    /// <summary>
    ///     model for a tune book
    /// </summary>
    public class TuneBook {

        /// <summary>
        ///     create a new tune book
        /// </summary>
        /// <param name="fileHeader"></param>
        /// <param name="immutableArrays"></param>
        public TuneBook(InformationFields fileHeader, ImmutableArray<Tune> immutableArrays) {
            FileHeader = fileHeader;
            Tunes = immutableArrays;
        }

        /// <summary>
        ///     file header
        /// </summary>
        public InformationFields FileHeader { get; }

        /// <summary>
        ///     tunes
        /// </summary>
        public ImmutableArray<Tune> Tunes { get; }
    }
}
