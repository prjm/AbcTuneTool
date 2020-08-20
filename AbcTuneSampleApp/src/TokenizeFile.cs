using System.IO;
using System.Text;
using AbcTuneTool.FileIo;

namespace AbcTuneSampleApp {
    public class TokenizeFile : AbcSampleApp {

        protected override void Run() {
            using var reader = new StreamReader(@"d:\temp\2.abc", Encoding.UTF8);
            using var tokenizer = new AbcTokenizer(reader, Cache, CharCache, StringBuilderPool, Logger);
            while (tokenizer.HasToken)
                tokenizer.ReadNextToken();
        }

    }
}
