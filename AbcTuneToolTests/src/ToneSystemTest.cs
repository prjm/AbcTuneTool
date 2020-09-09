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

        private static Tone[] StringToTones(IEnumerable<string> tones) {
            var t = new List<Tone>();
            foreach (var tone in tones) {
                Assert.AreEqual(2, tone.Length);
                var n = tone[0];
                var a = tone[1].AsAccidental();
                t.Add(new Tone(n, a));
            }

            return t.ToArray();
        }

        [TestMethod]
        public void TestMajor() {

            TestKey<MAJ>("C ", "", "C ", "D ", "E ", "F ", "G ", "A ", "B ", "C ");
            TestKey<MAJ>("G ", "F#", "G ", "A ", "B ", "C ", "D ", "E ", "F#", "G ");
            TestKey<MAJ>("D ", "F#C#", "D ", "E ", "F#", "G ", "A ", "B ", "C#", "D ");
            TestKey<MAJ>("A ", "F#C#G#", "A ", "B ", "C#", "D ", "E ", "F#", "G#", "A ");
            TestKey<MAJ>("E ", "F#C#G#D#", "E ", "F#", "G#", "A ", "B ", "C#", "D#", "E ");
            TestKey<MAJ>("B ", "F#C#G#D#A#", "B ", "C#", "D#", "E ", "F#", "G#", "A#", "B ");
            TestKey<MAJ>("F#", "F#C#G#D#A#E#", "F#", "G#", "A#", "B ", "C#", "D#", "E#", "F#");

        }

    }
}
