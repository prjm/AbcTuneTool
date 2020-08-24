namespace AbcTuneTool.Model {

    /// <summary>
    ///     tune
    /// </summary>
    public class Tune {

        readonly OtherLines otherLines;

        /// <summary>
        ///     create a new tune
        /// </summary>
        /// <param name="header"></param>
        public Tune(OtherLines otherLines, InformationFields header) {
            this.otherLines = otherLines;
            this.Header = header;
        }

        /// <summary>
        ///     field header
        /// </summary>
        public InformationFields Header { get; }
    }
}
