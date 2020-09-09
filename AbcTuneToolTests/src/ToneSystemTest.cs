using System.Collections.Generic;
using System.Linq;
using AbcTuneTool.Common;
using AbcTuneTool.Model;

namespace AbcTuneToolTests {


    public class ToneSystemTest : CommonTest {

        private class MAJ : MajorKeyTable { }

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

            TestKey<MAJ>("C ", "", "C ", "D ", "E ", "F ", "G ", "A ", "B ", "C ");
            TestKey<MAJ>("G ", "F#", "G ", "A ", "B ", "C ", "D ", "E ", "F#", "G ");
            TestKey<MAJ>("D ", "F#C#", "D ", "E ", "F#", "G ", "A ", "B ", "C#", "D ");
            TestKey<MAJ>("A ", "F#C#G#", "A ", "B ", "C#", "D ", "E ", "F#", "G#", "A ");
            TestKey<MAJ>("E ", "F#C#G#D#", "E ", "F#", "G#", "A ", "B ", "C#", "D#", "E ");
            TestKey<MAJ>("B ", "F#C#G#D#A#", "B ", "C#", "D#", "E ", "F#", "G#", "A#", "B ");
            TestKey<MAJ>("F#", "F#C#G#D#A#E#", "F#", "G#", "A#", "B ", "C#", "D#", "E#", "F#");
            TestKey<MAJ>("C#", "F#C#G#D#A#E#B#", "C#", "D#", "E#", "F#", "G#", "A#", "B#", "C#");

            TestKey<MAJ>("F ", "Bb", "F ", "G ", "A ", "Bb", "C ", "D ", "E ", "F ");
            TestKey<MAJ>("Bb", "BbEb", "Bb", "C ", "D ", "Eb", "F ", "G ", "A ", "Bb");
            TestKey<MAJ>("Eb", "BbEbAb", "Eb", "F ", "G ", "Ab", "Bb", "C ", "D ", "Eb");
            TestKey<MAJ>("Ab", "BbEbAbDb", "Ab", "Bb", "C ", "Db", "Eb", "F ", "G ", "Ab");
            TestKey<MAJ>("Db", "BbEbAbDbGb", "Db", "Eb", "F ", "Gb", "Ab", "Bb", "C ", "Db");
            TestKey<MAJ>("Gb", "BbEbAbDbGbCb", "Gb", "Ab", "Bb", "Cb", "Db", "Eb", "F ", "Gb");
            TestKey<MAJ>("Cb", "BbEbAbDbGbCbFb", "Cb", "Db", "Eb", "Fb", "Gb", "Ab", "Bb", "Cb");

        }

    }
}
