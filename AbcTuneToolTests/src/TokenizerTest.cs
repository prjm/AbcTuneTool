using System.IO;
using System.Text;
using AbcTuneTool.Common;
using AbcTuneTool.FileIo;
using AbcTuneTool.Model;

namespace AbcTuneToolTests {
    public class TokenizerTest {

        protected void RunTokenizerTest(string toTokenize, params (AbcCharacterKind kind, string value)[] tokens)
            => RunTokenizerTest(toTokenize, new int[0], tokens);

        protected void RunTokenizerTest(string toTokenize, int[] messageNumbers, params (AbcCharacterKind kind, string value)[] tokens) {
            var logger = new Logger();
            var cache = new StringCache();
            using var reader = new StringReader(toTokenize);
            using var tokenizer = new AbcTokenizer(reader, cache, logger);

            var sb = new StringBuilder();

            var counter = 0;
            while (tokenizer.HasToken) {
                tokenizer.ReadNextToken();
                var currentToken = tokenizer.CurrentToken;
                sb.Append(currentToken.OriginalValue);
                var (kind, value) = tokens[counter];
                counter++;
                Assert.AreEqual(kind, currentToken.Kind);
                Assert.AreEqual(value, currentToken.Value);
            }

            Assert.AreEqual(toTokenize, sb.ToString());

            Assert.AreEqual(messageNumbers.Length, logger.EntryCount);
            for (var i = 0; i < messageNumbers.Length; i++) {
                Assert.AreEqual(messageNumbers[i], logger.Entries[i].MessageNumber);
            }

        }

        private (AbcCharacterKind kind, string value) Eof()
            => (AbcCharacterKind.Eof, string.Empty);

        private (AbcCharacterKind kind, string value) Mnemo(char v)
            => (AbcCharacterKind.Mnenomic, new string(v, 1));

        private (AbcCharacterKind kind, string value) FontSize(char v)
            => (AbcCharacterKind.FontSize, new string(v, 1));

        private (AbcCharacterKind kind, string value) Chr(char v)
            => (AbcCharacterKind.Char, new string(v, 1));

        private (AbcCharacterKind kind, string value) Chr(string v)
        => (AbcCharacterKind.Char, v);


        private (AbcCharacterKind kind, string value) U2(char v)
            => (AbcCharacterKind.FixedUnicody2Byte, new string(v, 1));

        private (AbcCharacterKind kind, string value) U4(string v)
            => (AbcCharacterKind.FixedUnicode4Byte, v);

        private (AbcCharacterKind kind, string value) Entity(char v)
            => (AbcCharacterKind.Entity, new string(v, 1));

        private (AbcCharacterKind kind, string value) Backslash()
            => (AbcCharacterKind.Backslash, "\\");

        private (AbcCharacterKind kind, string value) Percent()
            => (AbcCharacterKind.Percent, "%");

        private (AbcCharacterKind kind, string value) Ampersand()
        => (AbcCharacterKind.Ampersand, "&");

        private (AbcCharacterKind kind, string value) Dollar()
            => (AbcCharacterKind.Dollar, "$");

        private (AbcCharacterKind kind, string value) Comment()
            => (AbcCharacterKind.Comment, string.Empty);

        [TestMethod]
        public void TestSimple() {
            RunTokenizerTest("", Eof());
            RunTokenizerTest("c", Chr('c'), Eof());
            RunTokenizerTest("cc c", Chr('c'), Chr('c'), Chr(' '), Chr('c'), Eof());
        }

        [TestMethod]
        public void TestMnemos() {
            RunTokenizerTest("\\AE", Mnemo('Æ'), Eof());
            RunTokenizerTest("\\", new[] { LogMessage.InvalidMnemo1 }, Chr(""), Eof());
            RunTokenizerTest("\\A", new[] { LogMessage.InvalidMnemo2 }, Chr(""), Eof());
            RunTokenizerTest("\\??", new[] { LogMessage.UnknownMnemo }, Chr(""), Eof());
        }

        [TestMethod]
        public void TestFontSize() {
            RunTokenizerTest("$1", FontSize('1'), Eof());
            RunTokenizerTest("$", new[] { LogMessage.InvalidFontSize }, Chr(""), Eof());
            RunTokenizerTest("$Q", new[] { LogMessage.InvalidFontSize }, Chr(""), Eof()); ;

        }

        [TestMethod]
        public void TestSpecials() {
            RunTokenizerTest("\\\\", Backslash(), Eof());
            RunTokenizerTest("\\%", Percent(), Eof());
            RunTokenizerTest("\\&", Ampersand(), Eof());
            RunTokenizerTest("g & t", Chr('g'), Chr(' '), Ampersand(), Chr(' '), Chr('t'), Eof());
            RunTokenizerTest("$$", Dollar(), Eof());
        }

        [TestMethod]
        public void TestEntities() {
            RunTokenizerTest("&copy;", Entity('©'), Eof());
            RunTokenizerTest("&larr;", Entity('⇐'), Eof());
            RunTokenizerTest("&", new[] { LogMessage.InvalidEntity }, Chr(""), Eof());
            RunTokenizerTest("&;", new[] { LogMessage.InvalidEntity }, Chr(""), Eof());
            RunTokenizerTest("&???;", new[] { LogMessage.UnknownEntity }, Chr(""), Eof());
        }

        [TestMethod]
        public void TestFixedUnicode() {
            RunTokenizerTest(@"\u0066", U2('f'), Eof());
            RunTokenizerTest(@"\u0066x", U2('f'), Chr('x'), Eof());
            RunTokenizerTest(@"\uE", Mnemo('Ĕ'), Eof());
            RunTokenizerTest(@"\u0E", new[] { LogMessage.UnknownMnemo }, Chr(""), Chr('E'), Eof());
            RunTokenizerTest(@"\u00E", new[] { LogMessage.UnknownMnemo }, Chr(""), Chr('0'), Chr('E'), Eof());

            RunTokenizerTest(@"\U00000066", U4("f"), Eof());
            RunTokenizerTest(@"\U0000066", U2('\0'), Chr('0'), Chr('6'), Chr('6'), Eof());

        }

        [TestMethod]
        public void TestComment() {
            RunTokenizerTest("%", Comment(), Eof());
            RunTokenizerTest("a%b", Chr('a'), Comment(), Eof());
            RunTokenizerTest("a\\AE%", Chr('a'), Mnemo('Æ'), Comment(), Eof());

            RunTokenizerTest("%x\u000Ax", Comment(), Chr('x'), Eof());
            RunTokenizerTest("%x\u000Bx", Comment(), Chr('x'), Eof());
            RunTokenizerTest("%x\u000Cx", Comment(), Chr('x'), Eof());
            RunTokenizerTest("%x\u000Dx", Comment(), Chr('x'), Eof());
            RunTokenizerTest("%x\u000D\u000Ax", Comment(), Chr('x'), Eof());
            RunTokenizerTest("%x\u0085x", Comment(), Chr('x'), Eof());
            RunTokenizerTest("%x\u2028x", Comment(), Chr('x'), Eof());
            RunTokenizerTest("%x\u2029x", Comment(), Chr('x'), Eof());

        }

    }
}
