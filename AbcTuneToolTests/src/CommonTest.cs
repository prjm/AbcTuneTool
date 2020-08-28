﻿using System;
using System.IO;
using AbcTuneTool.Common;
using AbcTuneTool.FileIo;
using AbcTuneTool.Model;

namespace AbcTuneToolTests {

    public abstract class CommonTest {

        protected T RunParserTest<T>(string toParse, Func<Parser, T> tester) {
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

        protected T Symbol<T>(string toParse, Func<Parser, T> f)
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


    }
}
