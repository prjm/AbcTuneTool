using System.Collections.Generic;
using System.Linq;
using AbcTuneTool.Common;
using AbcTuneTool.Model;
using AbcTuneTool.Model.KeyTables;
using AbcTuneTool.Model.Symbolic;

namespace AbcTuneToolTests {


    public class ToneSystemTest : CommonTest {

        private class MAJ : MajorKeyTable { }
        private class MIN : MinorKeyTable { }
        private class MIX : MixolydianKeyTable { }
        private class DOR : DorianKeyTable { }

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
        public void TestTone()
            => Assert.AreEqual(" ".AsTonePrefixAccidentals("a"), new Tone('a', Accidental.Undefined));

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

        [TestMethod]
        public void TestMixolydianScale() {

            TestKey<MIX>("G ", "", "g ", "a ", "b ", "c ", "d ", "e ", "f ", "g ");

            TestKey<MIX>("D ", "f#", "d ", "e ", "f#", "g ", "a ", "b ", "c ", "d ");
            TestKey<MIX>("A ", "f#c#", "a ", "b ", "c#", "d ", "e ", "f#", "g ", "a ");
            TestKey<MIX>("E ", "f#c#g#", "e ", "f#", "g#", "a ", "b ", "c#", "d ", "e ");
            TestKey<MIX>("B ", "f#c#g#d#", "b ", "c#", "d#", "e ", "f#", "g#", "a ", "b ");
            TestKey<MIX>("F#", "f#c#g#d#a#", "f#", "g#", "a#", "b ", "c#", "d#", "e ", "f#");
            TestKey<MIX>("C#", "f#c#g#d#a#e#", "c#", "d#", "e#", "f#", "g#", "a#", "b ", "c#");
            TestKey<MIX>("G#", "f#c#g#d#a#e#b#", "g#", "a#", "b#", "c#", "d#", "e#", "f#", "g#");

            TestKey<MIX>("C ", "bb", "c ", "d ", "e ", "f ", "g ", "a ", "bb", "c ");
            TestKey<MIX>("F ", "bbeb", "f ", "g ", "a ", "bb", "c ", "d ", "eb", "f ");
            TestKey<MIX>("Bb", "bbebab", "bb", "c ", "d ", "eb", "f ", "g ", "ab", "bb");
            TestKey<MIX>("Eb", "bbebabdb", "eb", "f ", "g ", "ab", "bb", "c ", "db", "eb");
            TestKey<MIX>("Ab", "bbebabdbgb", "ab", "bb", "c ", "db", "eb", "f ", "gb", "ab");
            TestKey<MIX>("Db", "bbebabdbgbcb", "db", "eb", "f ", "gb", "ab", "bb", "cb", "db");
            TestKey<MIX>("Gb", "bbebabdbgbcbfb", "gb", "ab", "bb", "cb", "db", "eb", "fb", "gb");

        }

        [TestMethod]
        public void TestDorianScale() {

            TestKey<DOR>("D ", "", "d ", "e ", "f ", "g ", "a ", "b ", "c ", "d ");

            TestKey<DOR>("A ", "f#", "a ", "b ", "c ", "d ", "e ", "f#", "g ", "a ");
            TestKey<DOR>("E ", "f#c#", "e ", "f#", "g ", "a ", "b ", "c#", "d ", "e ");
            TestKey<DOR>("E ", "f#c#", "e ", "f#", "g ", "a ", "b ", "c#", "d ", "e ");
            TestKey<DOR>("B ", "f#c#g#", "b ", "c#", "d ", "e ", "f#", "g#", "a ", "b ");
            TestKey<DOR>("F#", "f#c#g#d#", "f#", "g#", "a ", "b ", "c#", "d#", "e ", "f#");
            TestKey<DOR>("C#", "f#c#g#d#a#", "c#", "d#", "e ", "f#", "g#", "a#", "b ", "c#");
            TestKey<DOR>("G#", "f#c#g#d#a#e#", "g#", "a#", "b ", "c#", "d#", "e#", "f#", "g#");
            TestKey<DOR>("D#", "f#c#g#d#a#e#b#", "d#", "e#", "f#", "g#", "a#", "b#", "c#", "d#");

            TestKey<DOR>("G ", "bb", "g ", "a ", "bb", "c ", "d ", "e ", "f ", "g ");
            TestKey<DOR>("C ", "bbeb", "c ", "d ", "eb", "f ", "g ", "a ", "bb", "c ");
            TestKey<DOR>("F ", "bbebab", "f ", "g ", "ab", "bb", "c ", "d ", "eb", "f ");
            TestKey<DOR>("Bb", "bbebabdb", "bb", "c ", "db", "eb", "f ", "g ", "ab", "bb");
            TestKey<DOR>("Eb", "bbebabdbgb", "eb", "f ", "gb", "ab", "bb", "c ", "db", "eb");
            TestKey<DOR>("Ab", "bbebabdbgbcb", "ab", "bb", "cb", "db", "eb", "f ", "gb", "ab");
            TestKey<DOR>("Db", "bbebabdbgbcbfb", "db", "eb", "fb", "gb", "ab", "bb", "cb", "db");


        }

    }
}
