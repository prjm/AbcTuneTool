using System.Text;

using AbcTuneTool.Model;
using AbcTuneTool.Model.TuneElements;

namespace AbcTuneToolTests {

    public class TerminalVisitor : ISyntaxTreeStartVisitor<Terminal> {
        private readonly StringBuilder buffer;

        public TerminalVisitor()
            => buffer = new StringBuilder();

        public bool StartVisit(Terminal element) {
            buffer.Append(element.ToNewString());
            return true;
        }


        /// <summary>
        ///     visitor value
        /// </summary>
        public string Value
            => buffer.ToString();


        public static string GetSource(ISyntaxTreeElement element) {
            var visitor = new TerminalVisitor();
            element.Accept(visitor);
            return visitor.Value;
        }

    }
}
