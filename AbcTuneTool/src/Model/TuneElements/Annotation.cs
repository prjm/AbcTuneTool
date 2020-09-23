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

    }
}
