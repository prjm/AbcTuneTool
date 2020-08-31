using System.IO;
using System.Text;
using AbcTuneTool.FileIo;
using AbcTuneTool.Model;

namespace AbcTuneSampleApp {
    public class ParseFile : AbcSampleApp {

        protected override void Run() {
            var notes = new KeyNotes();
            using var reader = new StreamReader(@"d:\temp\1.abc", Encoding.UTF8);
            using var tokenizer = new Tokenizer(reader, Cache, StringBuilderPool, Logger);
            using var bufferedTokenizer = new BufferedAbcTokenizer(tokenizer);
            using var parser = new Parser(bufferedTokenizer, ListPools, notes);


            parser.ParseInformationField();
        }

    }
}
