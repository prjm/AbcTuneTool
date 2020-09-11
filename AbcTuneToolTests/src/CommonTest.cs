using System;
using System.Collections.Generic;
using System.IO;
using AbcTuneTool.Common;
using AbcTuneTool.FileIo;
using AbcTuneTool.Model;

namespace AbcTuneToolTests {

    public abstract class CommonTest {

        protected static T RunParserTest<T>(string toParse, Func<Parser, T> tester) {
            var cache = new StringCache();
            var pool = new StringBuilderPool();
            var logger = new Logger();
            var listPool = new ListPools();
            using var reader = new StringReader(toParse);
            using var tokenizer = new Tokenizer(reader, cache, pool, logger);
            using var bufferedTokenizer = new BufferedAbcTokenizer(tokenizer);
            using var parser = new Parser(bufferedTokenizer, listPool);
            return tester(parser);
        }

        protected static T Symbol<T>(string toParse, Func<Parser, T> f)
            => RunParserTest(toParse, (Parser p) => f(p));


        protected InformationField ParseInfoField(string source) {
            var result = Symbol(source, (Parser p) => p.ParseInformationField());
            Assert.NotNull(result);
            return result;
        }

        protected InstructionField ParseInstructionField(string source) {
            var result = Symbol(source, (Parser p) => p.ParseInformationField()) as InstructionField;
            Assert.NotNull(result);
            return result;
        }

        protected static KeyField ParseKeyField(string source, KeyStatus keyStatus = KeyStatus.ValidKey) {
            var result = Symbol(source, (Parser p) => p.ParseInformationField()) as KeyField;
            Assert.NotNull(result);
            Assert.AreEqual(keyStatus, result.IsValidKey);
            return result;
        }

        protected static Tone[] StringToTones(params string[] tones)
            => StringToTones((IEnumerable<string>)tones);

        protected static Tone[] StringToTones(IEnumerable<string> tones) {
            var t = new List<Tone>();
            foreach (var tone in tones) {

                if (tone.Length == 0)
                    continue;

                Assert.AreEqual(2, tone.Length);
                var n = tone[0];
                var a = tone[1].AsAccidental();
                t.Add(new Tone(n, a));
            }

            return t.ToArray();
        }

    }
}
