using System.Collections.Generic;
using System.Linq;
using AbcTuneTool.Common;
using AbcTuneTool.Model;

namespace AbcTuneToolTests {


    public class ToneSystemTest : CommonTest {

        private class MAJ : MajorKeyTable { }
        private class MIN : MinorKeyTable { }

        private void TestKey<T>(string key, string accidentals, params string[] tones) where T : KeyTable, new() {
            var k = new T();
            Assert.AreEqual(2, key.Length);
            var n = key[0];
            var a = key[1].AsAccidental();
            Assert.AreEqual(true, k.TryFindKey(n, a, out var _));
            Assert.AreEqual(true, k.DefineKey(n, a));

            var t = StringToTones(tones);
            var x = StringToTones(Split(accidentals, 2));

            Assert.AreEqualSeq(t, k.Tones.MainTones.ToArray());
            Assert.AreEqualSeq(x, k.Tones.Accidentals.ToArray());
        }

        private static IEnumerable<string> Split(string str, int chunkSize)
            => Enumerable.Range(0, str.Length / chunkSize).Select(i => str.Substring(i * chunkSize, chunkSize));

        [TestMethod]
        public void TestAccidentals() {
            Assert.AreEqual(Accidental.Flat, 'b'.AsAccidental());
            Assert.AreEqual(Accidental.Sharp, '#'.AsAccidental());
            Assert.AreEqual(Accidental.Sharp, '^'.AsAccidental());
            Assert.AreEqual(Accidental.Flat, '_'.AsAccidental());
            Assert.AreEqual(Accidental.Natural, '='.AsAccidental());

            Assert.AreEqual(Accidental.Flat, "b".AsAccidental());
            Assert.AreEqual(Accidental.Sharp, "#".AsAccidental());
            Assert.AreEqual(Accidental.Sharp, "^".AsAccidental());
            Assert.AreEqual(Accidental.Flat, "_".AsAccidental());
            Assert.AreEqual(Accidental.Natural, "=".AsAccidental());
            Assert.AreEqual(Accidental.DoubleFlat, "bb".AsAccidental());
            Assert.AreEqual(Accidental.DoubleSharp, "##".AsAccidental());
            Assert.AreEqual(Accidental.DoubleSharp, "^^".AsAccidental());
            Assert.AreEqual(Accidental.DoubleFlat, "__".AsAccidental());

        }

        [TestMethod]
        public void TestTone() {
            Assert.AreEqual(" a".AsTonePrefixAccidentals(), new Tone('a', Accidental.Undefined));
        }

        [TestMethod]
        public void TestMajorScale() {

            TestKey<MAJ>("C ", "", "c ", "d ", "e ", "f ", "g ", "a ", "b ", "c ");

            TestKey<MAJ>("G ", "f#", "g ", "a ", "b ", "c ", "d ", "e ", "f#", "g ");
            TestKey<MAJ>("D ", "f#c#", "d ", "e ", "f#", "g ", "a ", "b ", "c#", "d ");
            TestKey<MAJ>("A ", "f#c#g#", "a ", "b ", "c#", "d ", "e ", "f#", "g#", "a ");
            TestKey<MAJ>("E ", "f#c#g#d#", "e ", "f#", "g#", "a ", "b ", "c#", "d#", "e ");
            TestKey<MAJ>("B ", "f#c#g#d#a#", "b ", "c#", "d#", "e ", "f#", "g#", "a#", "b ");
            TestKey<MAJ>("F#", "f#c#g#d#a#e#", "f#", "g#", "a#", "b ", "c#", "d#", "e#", "f#");
            TestKey<MAJ>("C#", "f#c#g#d#a#e#b#", "c#", "d#", "e#", "f#", "g#", "a#", "b#", "c#");

            TestKey<MAJ>("F ", "bb", "f ", "g ", "a ", "bb", "c ", "d ", "e ", "f ");
            TestKey<MAJ>("Bb", "bbeb", "bb", "c ", "d ", "eb", "f ", "g ", "a ", "bb");
            TestKey<MAJ>("Eb", "bbebab", "eb", "f ", "g ", "ab", "bb", "c ", "d ", "eb");
            TestKey<MAJ>("Ab", "bbebabdb", "ab", "bb", "c ", "db", "eb", "f ", "g ", "ab");
            TestKey<MAJ>("Db", "bbebabdbgb", "db", "eb", "f ", "gb", "ab", "bb", "c ", "db");
            TestKey<MAJ>("Gb", "bbebabdbgbcb", "gb", "ab", "bb", "cb", "db", "eb", "f ", "gb");
            TestKey<MAJ>("Cb", "bbebabdbgbcbfb", "cb", "db", "eb", "fb", "gb", "ab", "bb", "cb");

        }

        [TestMethod]
        public void TestMinorScale() {

            TestKey<MIN>("A ", "", "a ", "b ", "c ", "d ", "e ", "f ", "g ", "a ");

            TestKey<MIN>("E ", "f#", "e ", "f#", "g ", "a ", "b ", "c ", "d ", "e ");
            TestKey<MIN>("B ", "f#c#", "b ", "c#", "d ", "e ", "f#", "g ", "a ", "b ");
            TestKey<MIN>("F#", "f#c#g#", "f#", "g#", "a ", "b ", "c#", "d ", "e ", "f#");
            TestKey<MIN>("C#", "f#c#g#d#", "c#", "d#", "e ", "f#", "g#", "a ", "b ", "c#");
            TestKey<MIN>("G#", "f#c#g#d#a#", "g#", "a#", "b ", "c#", "d#", "e ", "f#", "g#");
            TestKey<MIN>("D#", "f#c#g#d#a#e#", "d#", "e#", "f#", "g#", "a#", "b ", "c#", "d#");
            TestKey<MIN>("A#", "f#c#g#d#a#e#b#", "a#", "b#", "c#", "d#", "e#", "f#", "g#", "a#");

            TestKey<MIN>("D ", "bb", "d ", "e ", "f ", "g ", "a ", "bb", "c ", "d ");
            TestKey<MIN>("G ", "bbeb", "g ", "a ", "bb", "c ", "d ", "eb", "f ", "g ");
            TestKey<MIN>("C ", "bbebab", "c ", "d ", "eb", "f ", "g ", "ab", "bb", "c ");
            TestKey<MIN>("F ", "bbebabdb", "f ", "g ", "ab", "bb", "c ", "db", "eb", "f ");
            TestKey<MIN>("Bb", "bbebabdbgb", "bb", "c ", "db", "eb", "f ", "gb", "ab", "bb");
            TestKey<MIN>("Eb", "bbebabdbgbcb", "eb", "f ", "gb", "ab", "bb", "cb", "db", "eb");
            TestKey<MIN>("Ab", "bbebabdbgbcbfb", "ab", "bb", "cb", "db", "eb", "fb", "gb", "ab");

        }

    }
}
