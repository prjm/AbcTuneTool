using System;
using System.IO;
using System.Text;
using AbcTuneTool.Common;
using AbcTuneTool.FileIo;
using AbcTuneTool.Model;

namespace AbcTuneToolTests {
    public class TokenizerTest {

        protected void RunTokenizerTest(string toTokenize, params (TokenKind kind, string value)[] tokens)
            => RunTokenizerTest(toTokenize, new int[0], tokens);

        protected void RunTokenizerTest(string toTokenize, int[] messageNumbers, params (TokenKind kind, string value)[] tokens) {

            void tester(Tokenizer tokenizer) {
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
                var logger = tokenizer.Logger;

                Assert.AreEqual(messageNumbers.Length, logger.EntryCount);
                for (var i = 0; i < messageNumbers.Length; i++) {
                    Assert.AreEqual(messageNumbers[i], logger.Entries[i].MessageNumber);
                }
            }

            RunTokenizerTest(toTokenize, tester);
        }


        protected void RunTokenizerTest(string toTokenize, Action<Tokenizer> tester) {
            var logger = new Logger();
            var cache = new StringCache();
            var sbPool = new StringBuilderPool();
            using var reader = new StringReader(toTokenize);
            using var tokenizer = new Tokenizer(reader, cache, sbPool, logger);

            tester(tokenizer);
        }

        private (TokenKind kind, string value) Eof()
            => (TokenKind.Eof, string.Empty);

        private (TokenKind kind, string value) HdrCnt()
            => (TokenKind.HeaderContinuation, string.Empty);

        private (TokenKind kind, string value) Mnemo(char v)
            => (TokenKind.Mnenomic, new string(v, 1));

        private (TokenKind kind, string value) FontSize(char v)
            => (TokenKind.FontSize, new string(v, 1));

        private (TokenKind kind, string value) Chr(char v)
            => (TokenKind.Char, new string(v, 1));

        private (TokenKind kind, string value) Chr(string v)
            => (TokenKind.Char, v);

        private (TokenKind kind, string value) InfoField(string v)
            => (TokenKind.InformationFieldHeader, v);

        private (TokenKind kind, string value) EmptyLine()
            => (TokenKind.EmptyLine, string.Empty);

        private (TokenKind kind, string value) Linebreak()
            => (TokenKind.Linebreak, string.Empty);

        private (TokenKind kind, string value) Cnt()
            => (TokenKind.LineContinuation, string.Empty);

        private (TokenKind kind, string value) U2(char v)
            => (TokenKind.FixedUnicody2Byte, new string(v, 1));

        private (TokenKind kind, string value) U4(string v)
            => (TokenKind.FixedUnicode4Byte, v);

        private (TokenKind kind, string value) Entity(char v)
            => (TokenKind.Entity, new string(v, 1));

        private (TokenKind kind, string value) Entity(string v)
            => (TokenKind.Entity, v);

        private (TokenKind kind, string value) Backslash()
            => (TokenKind.Backslash, "\\");

        private (TokenKind kind, string value) Percent()
            => (TokenKind.Percent, "%");

        private (TokenKind kind, string value) Ampersand()
        => (TokenKind.Ampersand, "&");

        private (TokenKind kind, string value) Dollar()
            => (TokenKind.Dollar, "$");

        private (TokenKind kind, string value) Comment()
            => (TokenKind.Comment, string.Empty);

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
            RunTokenizerTest("$Q", new[] { LogMessage.InvalidFontSize2 }, Chr(""), Eof()); ;
        }

        [TestMethod]
        public void TestSpecials() {
            RunTokenizerTest("\\\\", Backslash(), Eof());
            RunTokenizerTest("\\%", Percent(), Eof());
            RunTokenizerTest("\\&", Ampersand(), Eof());
            RunTokenizerTest("g & t", Chr('g'), Chr(' '), Ampersand(), Chr(' '), Chr('t'), Eof());
            RunTokenizerTest("$$", Dollar(), Eof());

            RunTokenizerTest("K:", InfoField("K:"), Eof());
            RunTokenizerTest("A:", InfoField("A:"), Eof());
            RunTokenizerTest(" K:", Chr(' '), Chr('K'), Chr(':'), Eof());
            RunTokenizerTest("K", Chr("K"), Eof());

            RunTokenizerTest("A:\nB:", InfoField("A:"), Linebreak(), InfoField("B:"), Eof());
        }

        [TestMethod]
        public void TestEntities() {
            RunTokenizerTest("&copy;", Entity('©'), Eof());
            RunTokenizerTest("&larr;", Entity('←'), Eof());
            RunTokenizerTest("&", new[] { LogMessage.InvalidEntity1 }, Chr(""), Eof());
            RunTokenizerTest("&;", new[] { LogMessage.InvalidEntity1 }, Chr(""), Eof());
            RunTokenizerTest("&?;", new[] { LogMessage.InvalidEntity2 }, Entity(""), Chr("?"), Chr(";"), Eof());

            // special case: invalid entity and newline
            RunTokenizerTest("&c.\nx", new[] { LogMessage.InvalidEntity2 }, Entity(""), Chr("."), Linebreak(), Chr("x"), Eof());
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
        public void TestEmptyLine() {
            RunTokenizerTest("\n", EmptyLine(), Eof());
            RunTokenizerTest("\r\n", EmptyLine(), Eof());
            RunTokenizerTest("x\r\n", Chr("x"), Linebreak(), Eof());
        }

        [TestMethod]
        public void TestContinuations() {
            RunTokenizerTest("a\\\nb", Chr("a"), Cnt(), Chr("b"), Eof());
            RunTokenizerTest("a\\ \nb", Chr("a"), Cnt(), Chr("b"), Eof());
            RunTokenizerTest("a\\ %dummy \nb", Chr("a"), Cnt(), Chr("b"), Eof());
            RunTokenizerTest("A:x\n+:x", InfoField("A:"), Chr("x"), HdrCnt(), Chr("x"), Eof());
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

        [TestMethod]
        public void TestBufferedTokenizer() {
            static void tester(Tokenizer tokenizer) {
                using var bufferedTokenizer = new BufferedAbcTokenizer(tokenizer);
                Assert.AreEqual(TokenKind.Char, bufferedTokenizer.Lookahead(0).Kind);
                Assert.AreEqual(TokenKind.Char, bufferedTokenizer.Lookahead(1).Kind);
                Assert.AreEqual(TokenKind.Comment, bufferedTokenizer.Lookahead(2).Kind);
                Assert.AreEqual(TokenKind.Eof, bufferedTokenizer.Lookahead(3).Kind);
                Assert.AreEqual(TokenKind.Eof, bufferedTokenizer.Lookahead(4).Kind);

            };

            RunTokenizerTest("a % dd", tester);
        }

    }
}
