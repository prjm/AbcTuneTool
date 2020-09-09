using System.Collections.Generic;
using System.Collections.Immutable;
using AbcTuneTool.Common;

namespace AbcTuneTool.Model {

    /// <summary>
    ///     standard tone system
    /// </summary>
    public class StandardToneSystem : ToneSystem {
        private readonly string modeString;
        private readonly ToneInterval[] parsedMode;

        /// <summary>
        ///     create a standard tone system
        /// </summary>
        public StandardToneSystem(string mode, int rotate = 0) {
            modeString = mode;
            parsedMode = ParseMode(mode);
            Accidentals = ImmutableArray<Tone>.Empty;

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


        private StandardToneSystem(StandardToneSystem system) {
            parsedMode = system.parsedMode;
            modeString = system.modeString;
        }

        /// <summary>
        ///     main tones
        /// </summary>
        public IEnumerable<Tone> MainTones {
            get {
                var index = 0;
                foreach (var step in parsedMode) {
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
        /// <param name="accidental"></param>
        /// <param name="allowedAccidentals"></param>
        /// <returns></returns>
        public StandardToneSystem DefineKey(Accidental accidental, List<Tone> allowedAccidentals) {
            var accidentals = new List<Tone>();
            var result = new StandardToneSystem(this);

            accidentals.AddRange(Accidentals);

            foreach (var tone in Tones) {

                var newTone = accidental switch
                {
                    Accidental.Sharp => AddFifth(this, tone, allowedAccidentals),
                    Accidental.Flat => RemoveFifth(this, tone, allowedAccidentals),
                    _ => tone,
                };
                result.AddTone(newTone);
            }

            if (accidental != Accidental.Undefined)
                foreach (var tone in result.MainTones)
                    if (tone.Accidental == accidental && accidentals.IndexOf(tone) < 0)
                        accidentals.Add(tone);

            result.Accidentals = accidentals.ToImmutableArray();
            return result;
        }

        internal bool AddAccidental(Tone addTone) {
            var acc = new List<Tone>();
            var wasCombined = false;

            foreach (var existing in Accidentals) {

                var newAcc = existing;

                if (!wasCombined && existing.Name == addTone.Name) {
                    var newTone = new Tone(existing.Name, existing.Accidental.Combine(addTone.Accidental));

                    if (newTone.Accidental != Accidental.Undefined)
                        acc.Add(newTone);

                    wasCombined = true;
                    continue;
                }

                acc.Add(newAcc);
            }

            if (!wasCombined)
                acc.Add(addTone);

            Accidentals = acc.ToImmutableArray();
            return true;
        }

        private static Tone AddFifth(ToneSystem system, Tone tone, List<Tone> allowedAccidentals) {
            var index = system.Tones.IndexOf(tone);
            var newIndex = (index + 7) % system.Tones.Count;
            tone = system.Tones[newIndex];

            if (!(tone.UpperAlternative is null) && allowedAccidentals.Contains(tone.UpperAlternative))
                tone = tone.UpperAlternative;

            return new Tone(tone.Name, tone.Accidental, tone.LowerAlternative, tone.UpperAlternative);
        }

        private static Tone RemoveFifth(ToneSystem system, Tone tone, List<Tone> allowedAccidentals) {
            var index = system.Tones.IndexOf(tone);
            var newIndex = (index + system.Tones.Count - 7) % system.Tones.Count;
            tone = system.Tones[newIndex];

            if (!(tone.LowerAlternative is null) && allowedAccidentals.Contains(tone.LowerAlternative))
                tone = tone.LowerAlternative;

            return new Tone(tone.Name, tone.Accidental, tone.LowerAlternative, tone.UpperAlternative);
        }

    }
}
