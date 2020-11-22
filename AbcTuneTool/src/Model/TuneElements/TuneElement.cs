using System;

namespace AbcTuneTool.Model.TuneElements {

    /// <summary>
    ///     base class for tune elements
    /// </summary>
    public abstract class TuneElement : IEquatable<TuneElement>, ISyntaxTreeElement {

        /// <summary>
        ///     check for equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public abstract bool Equals(TuneElement? other);

        /// <summary>
        ///     compute a hash code
        /// </summary>
        /// <returns></returns>
        public abstract override int GetHashCode();

        /// <summary>
        ///     check for equality
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
            => obj is TuneElement e && Equals(e);

        /// <summary>
        ///    accept a visitor
        /// </summary>
        /// <param name="visitor"></param>
        public abstract bool Accept(ISyntaxTreeVisitor visitor);
    }
}
