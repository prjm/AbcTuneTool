using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AbcTuneTool.Common;
using AbcTuneTool.FileIo;
using AbcTuneTool.Model;

namespace AbcTuneTool {

    /// <summary>
    ///
    /// </summary>
    public partial class Form1 : Form {

        /// <summary>
        ///
        /// </summary>
        public Form1()
            => InitializeComponent();

        private void Form1_Shown(object sender, System.EventArgs e) {

            var logger = new Logger();
            var cache = new StringCache();
            var pool = new StringBuilderPool();
            var pool2 = new ListPools();
            var charCache = new AbcCharacterCache();
            using var fileReader = new StreamReader(@"d:\temp\2.abc");
            using var tokenizer = new AbcTokenizer(fileReader, cache, charCache, pool, logger);
            using var parser = new AbcParser(new BufferedAbcTokenizer(tokenizer), pool2);

            var rootNode = treeView1.Nodes.Add("demo");
            var tunes = parser.ParseTuneBook();

            foreach (var tune in tunes.Tunes) {
                Func<IEnumerable<InformationField>, string> c = (q) => q.FirstOrDefault()?.FieldValue.Select(t => t.AbcChar.Value).Aggregate((x, y) => string.Concat(x, y));
                var title = c(tune.Header.Fields.Where(t => t.FieldKind.AbcChar.Value == "T:"));

                if (string.IsNullOrWhiteSpace(title))
                    title = c(tune.Header.Fields.Where(t => t.FieldKind.AbcChar.Value == "X:"));


                rootNode.Nodes.Add(title);
            }

            rootNode.Expand();

        }
    }
}
