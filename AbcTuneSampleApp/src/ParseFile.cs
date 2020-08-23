using System.IO;
using System.Text;
using AbcTuneTool.FileIo;

namespace AbcTuneSampleApp {
    public class ParseFile : AbcSampleApp {

        protected override void Run() {
            using var reader = new StreamReader(@"d:\temp\1.abc", Encoding.UTF8);
            using var tokenizer = new AbcTokenizer(reader, Cache, CharCache, StringBuilderPool, Logger);
            using var bufferedTokenizer = new BufferedAbcTokenizer(tokenizer);
            using var parser = new AbcParser(bufferedTokenizer, ListPools);


            parser.ParseInformationField();
        }

    }
}
