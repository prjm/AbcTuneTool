using System;
using System.IO;
using AbcTuneTool.Common;
using AbcTuneTool.FileIo;
using AbcTuneTool.Model;

namespace AbcTuneToolTests {

    public abstract class CommonTest {

        protected T RunParserTest<T>(string toParse, Func<AbcParser, T> tester) {
            var cache = new StringCache();
            var pool = new StringBuilderPool();
            var logger = new Logger();
            var listPool = new ListPools();
            using var reader = new StringReader(toParse);
            using var tokenizer = new Tokenizer(reader, cache, pool, logger);
            using var bufferedTokenizer = new BufferedAbcTokenizer(tokenizer);
            using var parser = new AbcParser(bufferedTokenizer, listPool);
            return tester(parser);
        }

        protected T Symbol<T>(string toParse, Func<AbcParser, T> f)
            => RunParserTest(toParse, (AbcParser p) => f(p));


        protected InformationField ParseInfoField(string source) {
            var result = Symbol(source, (AbcParser p) => p.ParseInformationField());
            Assert.NotNull(result);
            return result;
        }

    }
}
