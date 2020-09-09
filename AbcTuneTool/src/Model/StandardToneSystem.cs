using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using AbcTuneTool.Common;

namespace AbcTuneTool.Model {

    /// <summary>
    ///     standard tone system
    /// </summary>
    public class StandardToneSystem : ToneSystem {
        private readonly ToneInterval[] mode;

        /// <summary>
        ///     create a standard tone system
        /// </summary>
        public StandardToneSystem(string mode, int rotate = 0) {
            this.mode = ParseMode(mode);

            AddTone('C', ' ', ' ', ' ', 'B', '#');
            AddTone('C', '#', 'D', 'b');
            AddTone('D', ' ');
            AddTone('D', '#', 'E', 'b');
            AddTone('E', ' ', 'F', 'b');
            AddTone('F', ' ', ' ', ' ', 'E', '#');
            AddTone('F', '#', 'G', 'b');
            AddTone('G', ' ');
            AddTone('G', '#', 'A', 'b');
            AddTone('A', ' ');
            AddTone('A', '#', 'B', 'b');
            AddTone('B', ' ', 'C', 'b');

            if (rotate != 0)
                Tones = Tones.Rotate(rotate);
        }


        private StandardToneSystem(StandardToneSystem system)
            => mode = system.mode;

        /// <summary>
        ///     main tones
        /// </summary>
        public IEnumerable<Tone> MainTones {
            get {
                var index = 0;
                foreach (var step in mode) {
                    index = (index + step.AsHalfTones()) % Tones.Count;
                    yield return Tones[index];
                }
            }
        }

        /// <summary>
        ///     accidentals
        /// </summary>
        public ImmutableArray<Tone> Accidentals { get; private set; }

        /// <summary>
        ///     parse a given mode description string
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static ToneInterval[] ParseMode(string mode) {
            var result = new ToneInterval[mode.Length];

            for (var i = 0; i < mode.Length; i++)
                result[i] = mode[i].AsToneInterval();

            return result;
        }

        /// <summary>
        ///     define a key in a tone system
        /// </summary>
        /// <param name="level"></param>
        /// <param name="useAlternative"></param>
        /// <returns></returns>
        public StandardToneSystem DefineKey(int level, bool useAlternative) {
            var old = this;
            var accidentals = new List<Tone>();
            var accidental = level <= 0 ? Accidental.Flat : Accidental.Sharp;

            for (var i = 0; i < Math.Abs(level); i++) {
                var result = new StandardToneSystem(this);

                foreach (var tone in old.Tones) {
                    var newtone = level > 0 ? AddFifth(old, tone, useAlternative) : RemoveFifth(old, tone, useAlternative);
                    result.AddTone(newtone);
                }

                foreach (var tone in result.MainTones)
                    if (tone.Accidental == accidental && accidentals.IndexOf(tone) < 0)
                        accidentals.Add(tone);

                old = result;
            }

            old.Accidentals = accidentals.ToImmutableArray();
            return old;
        }

        private static Tone AddFifth(ToneSystem system, Tone tone, bool useAlt) {
            var index = system.Tones.IndexOf(tone);
            var newIndex = (index + 7) % system.Tones.Count;
            tone = system.Tones[newIndex];

            if (useAlt && !(tone.UpperAlternative is null) && tone.UpperAlternative.Name != ' ')
                tone = tone.UpperAlternative;

            return new Tone(tone.Name, tone.Accidental);
        }

        private static Tone RemoveFifth(ToneSystem system, Tone tone, bool useAlt) {
            var index = system.Tones.IndexOf(tone);
            var newIndex = (index + system.Tones.Count - 7) % system.Tones.Count;
            tone = system.Tones[newIndex];

            if (useAlt && !(tone.LowerAlternative is null) && tone.LowerAlternative.Name != ' ')
                tone = tone.LowerAlternative;

            return new Tone(tone.Name, tone.Accidental);
        }

    }
}
