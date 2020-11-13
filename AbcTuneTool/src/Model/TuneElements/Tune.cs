using AbcTuneTool.Model.Fields;

namespace AbcTuneTool.Model.TuneElements {

    /// <summary>
    ///     tune
    /// </summary>
    public class Tune {

        readonly OtherLines otherLines;

        /// <summary>
        ///     create a new tune
        /// </summary>
        /// <param name="header"></param>
        /// <param name="otherLines"></param>
        /// <param name="tuneBody">body</param>
        public Tune(OtherLines otherLines, InformationFields header, TuneBody tuneBody) {
            this.otherLines = otherLines;
            Header = header;
            Body = tuneBody;
        }

        /// <summary>
        ///     field header
        /// </summary>
        public InformationFields Header { get; }

        /// <summary>
        ///     body
        /// </summary>
        public TuneBody Body { get; }
    }
}
