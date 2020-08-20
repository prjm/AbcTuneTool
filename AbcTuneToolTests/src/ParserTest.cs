using System;
using System.IO;
using AbcTuneTool.Common;
using AbcTuneTool.FileIo;
using AbcTuneToolTest;

namespace AbcTuneToolTests {
    public class ParserTest : CommonTest {

        protected T RunParserTest<T>(string toParse, Func<AbcParser, T> tester) {
            var cache = new StringCache();
            var charCache = new AbcCharacterCache();
            var pool = new StringBuilderPool();
            var logger = new Logger();
            using var reader = new StringReader(toParse);
            using var tokenizer = new AbcTokenizer(reader, cache, charCache, pool, logger);
            using var bufferedTokenizer = new BufferedAbcTokenizer(tokenizer);
            using var parser = new AbcParser(bufferedTokenizer);
            return tester(parser);
        }

        protected T Symbol<T>(string toParse, Func<AbcParser, T> f)
            => RunParserTest(toParse, (AbcParser p) => f(p));

        [TestMethod]
        public void TestParseInfoField() {
            var field = Symbol("K:test", (AbcParser p) => p.ParseInformationField());
            Assert.NotNull(field);
            Assert.AreEqual("K:", field.FieldKind.AbcChar.Value);
        }

    }
}
