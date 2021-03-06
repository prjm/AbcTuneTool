﻿using System.Linq;
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
        private class PHR : PhrygianKeyTable { }
        private class LYD : LydianKeyTable { }
        private class LOC : LocrianKeyTable { }

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

        [TestMethod]
        public void TestPhrygianScale() {

            TestKey<PHR>("E ", "", "e ", "f ", "g ", "a ", "b ", "c ", "d ", "e ");

            TestKey<PHR>("B ", "f#", "b ", "c ", "d ", "e ", "f#", "g ", "a ", "b ");
            TestKey<PHR>("F#", "f#c#", "f#", "g ", "a ", "b ", "c#", "d ", "e ", "f#");
            TestKey<PHR>("C#", "f#c#g#", "c#", "d ", "e ", "f#", "g#", "a ", "b ", "c#");
            TestKey<PHR>("G#", "f#c#g#d#", "g#", "a ", "b ", "c#", "d#", "e ", "f#", "g#");
            TestKey<PHR>("D#", "f#c#g#d#a#", "d#", "e ", "f#", "g#", "a#", "b ", "c#", "d#");
            TestKey<PHR>("A#", "f#c#g#d#a#e#", "a#", "b ", "c#", "d#", "e#", "f#", "g#", "a#");
            TestKey<PHR>("E#", "f#c#g#d#a#e#b#", "e#", "f#", "g#", "a#", "b#", "c#", "d#", "e#");

            TestKey<PHR>("A ", "bb", "a ", "bb", "c ", "d ", "e ", "f ", "g ", "a ");
            TestKey<PHR>("D ", "bbeb", "d ", "eb", "f ", "g ", "a ", "bb", "c ", "d ");
            TestKey<PHR>("G ", "bbebab", "g ", "ab", "bb", "c ", "d ", "eb", "f ", "g ");
            TestKey<PHR>("C ", "bbebabdb", "c ", "db", "eb", "f ", "g ", "ab", "bb", "c ");
            TestKey<PHR>("F ", "bbebabdbgb", "f ", "gb", "ab", "bb", "c ", "db", "eb", "f ");
            TestKey<PHR>("Bb", "bbebabdbgbcb", "bb", "cb", "db", "eb", "f ", "gb", "ab", "bb");
            TestKey<PHR>("Eb", "bbebabdbgbcbfb", "eb", "fb", "gb", "ab", "bb", "cb", "db", "eb");
        }

        [TestMethod]
        public void TestLydianScale() {

            TestKey<LYD>("F ", "", "f ", "g ", "a ", "b ", "c ", "d ", "e ", "f ");

            TestKey<LYD>("C ", "f#", "c ", "d ", "e ", "f#", "g ", "a ", "b ", "c ");
            TestKey<LYD>("G ", "f#c#", "g ", "a ", "b ", "c#", "d ", "e ", "f#", "g ");
            TestKey<LYD>("D ", "f#c#g#", "d ", "e ", "f#", "g#", "a ", "b ", "c#", "d ");
            TestKey<LYD>("A ", "f#c#g#d#", "a ", "b ", "c#", "d#", "e ", "f#", "g#", "a ");
            TestKey<LYD>("E ", "f#c#g#d#a#", "e ", "f#", "g#", "a#", "b ", "c#", "d#", "e ");
            TestKey<LYD>("B ", "f#c#g#d#a#e#", "b ", "c#", "d#", "e#", "f#", "g#", "a#", "b ");
            TestKey<LYD>("F#", "f#c#g#d#a#e#b#", "f#", "g#", "a#", "b#", "c#", "d#", "e#", "f#");

            TestKey<LYD>("Bb", "bb", "bb", "c ", "d ", "e ", "f ", "g ", "a ", "bb");
            TestKey<LYD>("Eb", "bbeb", "eb", "f ", "g ", "a ", "bb", "c ", "d ", "eb");
            TestKey<LYD>("Ab", "bbebab", "ab", "bb", "c ", "d ", "eb", "f ", "g ", "ab");
            TestKey<LYD>("Db", "bbebabdb", "db", "eb", "f ", "g ", "ab", "bb", "c ", "db");
            TestKey<LYD>("Gb", "bbebabdbgb", "gb", "ab", "bb", "c ", "db", "eb", "f ", "gb");
            TestKey<LYD>("Cb", "bbebabdbgbcb", "cb", "db", "eb", "f ", "gb", "ab", "bb", "cb");
            TestKey<LYD>("Fb", "bbebabdbgbcbfb", "fb", "gb", "ab", "bb", "cb", "db", "eb", "fb");
        }

        [TestMethod]
        public void TestLocrianScale() {

            TestKey<LOC>("B ", "", "b ", "c ", "d ", "e ", "f ", "g ", "a ", "b ");

            TestKey<LOC>("F#", "f#", "f#", "g ", "a ", "b ", "c ", "d ", "e ", "f#");
            TestKey<LOC>("C#", "f#c#", "c#", "d ", "e ", "f#", "g ", "a ", "b ", "c#");
            TestKey<LOC>("G#", "f#c#g#", "g#", "a ", "b ", "c#", "d ", "e ", "f#", "g#");
            TestKey<LOC>("D#", "f#c#g#d#", "d#", "e ", "f#", "g#", "a ", "b ", "c#", "d#");
            TestKey<LOC>("A#", "f#c#g#d#a#", "a#", "b ", "c#", "d#", "e ", "f#", "g#", "a#");
            TestKey<LOC>("E#", "f#c#g#d#a#e#", "e#", "f#", "g#", "a#", "b ", "c#", "d#", "e#");
            TestKey<LOC>("B#", "f#c#g#d#a#e#b#", "b#", "c#", "d#", "e#", "f#", "g#", "a#", "b#");

            TestKey<LOC>("E ", "bb", "e ", "f ", "g ", "a ", "bb", "c ", "d ", "e ");
            TestKey<LOC>("A ", "bbeb", "a ", "bb", "c ", "d ", "eb", "f ", "g ", "a ");
            TestKey<LOC>("D ", "bbebab", "d ", "eb", "f ", "g ", "ab", "bb", "c ", "d ");
            TestKey<LOC>("G ", "bbebabdb", "g ", "ab", "bb", "c ", "db", "eb", "f ", "g ");
            TestKey<LOC>("C ", "bbebabdbgb", "c ", "db", "eb", "f ", "gb", "ab", "bb", "c ");
            TestKey<LOC>("F ", "bbebabdbgbcb", "f ", "gb", "ab", "bb", "cb", "db", "eb", "f ");
            TestKey<LOC>("Bb", "bbebabdbgbcbfb", "bb", "cb", "db", "eb", "fb", "gb", "ab", "bb");
        }

    }
}
