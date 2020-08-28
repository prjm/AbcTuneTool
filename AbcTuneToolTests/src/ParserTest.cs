using System.Collections.Immutable;
using AbcTuneTool.FileIo;
using AbcTuneTool.Model;

namespace AbcTuneToolTests {
    public class ParserTest : CommonTest {

        [TestMethod]
        public void TestParseVersion() {
            var source = "%abc-2.1";
            var tunebook = Symbol(source, (Parser p) => p.ParseTuneBook());
            Assert.NotNull(tunebook);
            Assert.AreEqual("2.1", tunebook.Version);

            source = "dfsfsf";
            tunebook = Symbol(source, (Parser p) => p.ParseTuneBook());
            Assert.NotNull(tunebook);
            Assert.AreEqual(KnownStrings.UndefinedVersion, tunebook.Version);
        }

        [TestMethod]
        public void TestParseInfoField() {
            var field = ParseInfoField("K:test");
            Assert.AreEqual("K:", field.Header.ToNewString());
            Assert.AreEqual("test", field.Value.ToNewString());
        }

        [TestMethod]
        public void TestMatchFunction() {
            var t = ImmutableArray.Create<Token>(new Token("", "ab", TokenKind.Char), new Token("", "cd", TokenKind.Char));
            var s = new Terminal(t);
            Assert.AreEqual(true, s.Matches("a"));
            Assert.AreEqual(true, s.Matches("ab"));
            Assert.AreEqual(true, s.Matches("ab"));
            Assert.AreEqual(true, s.Matches("abc"));
            Assert.AreEqual(true, s.Matches("abcd"));
            Assert.AreEqual(false, s.Matches("abcde"));
        }

        [TestMethod]
        public void TestParseInfoFields() {
            var source = "B:bar\nA:foo\n";
            var fields = Symbol(source, (Parser p) => p.ParseInformationFields());
            Assert.NotNull(fields);
            Assert.AreEqual(2, fields.Fields.Length);
            Assert.AreEqual("B:", fields.Fields[0].Header.ToNewString());
            Assert.AreEqual("A:", fields.Fields[1].Header.ToNewString());
        }

    }
}
