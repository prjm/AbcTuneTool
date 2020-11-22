using System.Collections.Immutable;

using AbcTuneTool.Model.Fields;

namespace AbcTuneTool.Model.TuneElements {

    /// <summary>
    ///     model for a tune book
    /// </summary>
    public class TuneBook : ISyntaxTreeElement {

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

        /// <summary>
        ///     accept a visitor
        /// </summary>
        /// <param name="visitor"></param>
        public bool Accept(ISyntaxTreeVisitor visitor) {
            var result = visitor.StartVisitNode(this);
            result &= FileHeader.Accept(visitor);

            for (var i = 0; i < Tunes.Length; i++) {
                var tune = Tunes[i];
                result &= tune.Header.Accept(visitor);
                result &= tune.Body.Accept(visitor);

                if (!result)
                    return false;
            }

            result &= visitor.EndVisitNode(this);
            return result;
        }

    }
}
