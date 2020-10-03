using System;

namespace AbcTuneTool.Model.TuneElements {

    /// <summary>
    ///     note annotation
    /// </summary>
    public class Annotation : TuneElement {

        /// <summary>
        ///     create a new annotation
        /// </summary>
        /// <param name="position"></param>
        /// <param name="text"></param>
        public Annotation(AnnotationPosition position, string text) {
            Position = position;
            Text = text;
        }

        /// <summary>
        ///     position
        /// </summary>
        public AnnotationPosition Position { get; }

        /// <summary>
        ///     annotation text
        /// </summary>
        public string Text { get; }

        /// <summary>
        ///     check for equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(TuneElement? other)
            => other is Annotation a && a.Position == Position && string.Equals(a.Text, Text, System.StringComparison.Ordinal);

        /// <summary>
        ///     compute a hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
            => HashCode.Combine(Position, Text);
    }
}
