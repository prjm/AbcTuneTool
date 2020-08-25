using System;
using System.IO;
using AbcTuneTool.Common;
using AbcTuneTool.FileIo;
using AbcTuneTool.Model;
using AbcTuneToolTest;

namespace AbcTuneToolTests {
    public class ParserTest : CommonTest {

        protected T RunParserTest<T>(string toParse, Func<AbcParser, T> tester) {
            var cache = new StringCache();
            var charCache = new AbcCharacterCache();
            var pool = new StringBuilderPool();
            var logger = new Logger();
            var listPool = new ListPools();
            using var reader = new StringReader(toParse);
            using var tokenizer = new AbcTokenizer(reader, cache, charCache, pool, logger);
            using var bufferedTokenizer = new BufferedAbcTokenizer(tokenizer);
            using var parser = new AbcParser(bufferedTokenizer, listPool);
            return tester(parser);
        }

        protected T Symbol<T>(string toParse, Func<AbcParser, T> f)
            => RunParserTest(toParse, (AbcParser p) => f(p));

        [TestMethod]
        public void TestParseVersion() {
            var source = "%abc-2.1";
            var tunebook = Symbol(source, (AbcParser p) => p.ParseTuneBook());
            Assert.NotNull(tunebook);
            Assert.AreEqual("2.1", tunebook.Version);

            source = "dfsfsf";
            tunebook = Symbol(source, (AbcParser p) => p.ParseTuneBook());
            Assert.NotNull(tunebook);
            Assert.AreEqual(KnownStrings.UndefinedVersion, tunebook.Version);
        }


        [TestMethod]
        public void TestParseInfoField() {
            var source = "K:test";
            var field = Symbol(source, (AbcParser p) => p.ParseInformationField());
            Assert.NotNull(field);
            Assert.AreEqual("K:", field.FieldKind.AbcChar.Value);
        }

        [TestMethod]
        public void TestParseInfoFields() {
            var source = "B:bar\nA:foo\n";
            var fields = Symbol(source, (AbcParser p) => p.ParseInformationFields());
            Assert.NotNull(fields);
            Assert.AreEqual(2, fields.Fields.Length);
            Assert.AreEqual("B:", fields.Fields[0].FieldKind.AbcChar.Value);
            Assert.AreEqual("A:", fields.Fields[1].FieldKind.AbcChar.Value);
        }

    }
}
