using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using AbcTuneTool.Common;
using AbcTuneTool.FileIo;
using AbcTuneTool.Model;
using AbcTuneTool.Model.Fields;
using AbcTuneTool.Model.TuneElements;
using AbcTuneTool.src.Model.Fields;

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

        protected TuneBody ParseTuneBody(string data) {
            var source = data.Replace("§", Environment.NewLine);
            var result = Symbol(source, (Parser p) => p.ParseTuneBody());
            Assert.NotNull(result);
            return result;
        }

        protected TuneBook ParseTuneBook(string data) {
            var source = data.Replace("§", Environment.NewLine);
            var result = Symbol(source, (Parser p) => p.ParseTuneBook());
            Assert.NotNull(result);
            return result;
        }

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

        protected static LengthField ParseLengthField(string source) {
            var result = Symbol(source, (Parser p) => p.ParseInformationField()) as LengthField;
            Assert.NotNull(result);
            return result;
        }

        protected static MeterField ParseMeterField(string source) {
            var result = Symbol(source, (Parser p) => p.ParseInformationField()) as MeterField;
            Assert.NotNull(result);
            return result;
        }

        protected static MacroField ParseMacroField(string source) {
            var result = Symbol(source, (Parser p) => p.ParseInformationField()) as MacroField;
            Assert.NotNull(result);
            return result;
        }

        protected static PartsField ParsePartsField(string source) {
            var result = Symbol(source, (Parser p) => p.ParseInformationField()) as PartsField;
            Assert.NotNull(result);
            return result;
        }

        protected static TempoField ParseTempoField(string source) {
            var result = Symbol(source, (Parser p) => p.ParseInformationField()) as TempoField;
            Assert.NotNull(result);
            return result;
        }

        protected static SymbolLineField ParseSymbolLineField(string source) {
            var result = Symbol(source, (Parser p) => p.ParseInformationField()) as SymbolLineField;
            Assert.NotNull(result);
            return result;
        }

        protected static UserDefinedField ParseUserDefinedField(string source) {
            var result = Symbol(source, (Parser p) => p.ParseInformationField()) as UserDefinedField;
            Assert.NotNull(result);
            return result;
        }

        protected static VoiceField ParseVoiceField(string source) {
            var result = Symbol(source, (Parser p) => p.ParseInformationField()) as VoiceField;
            Assert.NotNull(result);
            return result;
        }

        protected static ReferenceNumberField ParseRefNumberField(string source) {
            var result = Symbol(source, (Parser p) => p.ParseInformationField()) as ReferenceNumberField;
            Assert.NotNull(result);
            return result;
        }

        protected static TranscriptionField ParseTranscriptionField(string source) {
            var result = Symbol(source, (Parser p) => p.ParseInformationField()) as TranscriptionField;
            Assert.NotNull(result);
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

        protected static IEnumerable<string> Split(string str, int chunkSize)
            => Enumerable.Range(0, str.Length / chunkSize).Select(i => str.Substring(i * chunkSize, chunkSize));

        protected static Note[] StringToNotes(string notes, int chunksize = 1)
            => StringToNotes(Split(notes, chunksize));

        protected static Note[] StringToNotes(IEnumerable<string> notes) {
            var result = new Note[notes.Count()];
            var i = 0;

            foreach (var note in notes) {
                var ups = note.Count(c => c == '\'');
                var downs = note.Count(c => c == ',');
                result[i] = new Note(note[0], ups - downs);
                i++;
            }

            return result;
        }

    }
}
