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
        /// <param name="version">tune book version</param>
        public TuneBook(string version, InformationFields fileHeader, ImmutableArray<Tune> immutableArrays) {
            Version = version;
            FileHeader = fileHeader;
            Tunes = immutableArrays;
        }

        /// <summary>
        ///     version information
        /// </summary>
        public string Version { get; }

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
