using System.Collections.Generic;
using AbcTuneTool.Common;

namespace AbcTuneTool.Model {

    /// <summary>
    ///     create a new tone system
    /// </summary>
    public class ToneSystem {

        /// <summary>
        ///     adds a tone
        /// </summary>
        /// <param name="name"></param>
        /// <param name="accidental"></param>
        /// <returns></returns>
        public Tone AddTone(char name, char accidental) {
            var tone = new Tone(name, accidental.AsAccidental());
            Tones.Add(tone);
            return tone;
        }


        /// <summary>
        ///     adds a tone
        /// </summary>
        /// <param name="name"></param>
        /// <param name="accidental"></param>
        /// <param name="lowerAlternative">lower alternative name</param>
        /// <param name="lowerAlternativeAccidental">lower alternative accidental</param>
        /// <returns></returns>
        public Tone AddTone(char name, char accidental, char lowerAlternative, char lowerAlternativeAccidental) {
            var tone = new Tone(name, accidental.AsAccidental(), new Tone(lowerAlternative, lowerAlternativeAccidental.AsAccidental()));
            Tones.Add(tone);
            return tone;
        }

        /// <summary>
        ///     adds a tone
        /// </summary>
        /// <param name="name"></param>
        /// <param name="accidental"></param>
        /// <param name="lowerAlternative">lower alternative name</param>
        /// <param name="lowerAlternativeAccidental">lower alternative accidental</param>
        /// <param name="upperAlternative"></param>
        /// <param name="upperAlternativeAccidental"></param>
        /// <returns></returns>
        public Tone AddTone(char name, char accidental, char lowerAlternative, char lowerAlternativeAccidental, char upperAlternative, char upperAlternativeAccidental) {
            var tone = new Tone(
                name, accidental.AsAccidental(),
                new Tone(lowerAlternative, lowerAlternativeAccidental.AsAccidental()),
                new Tone(upperAlternative, upperAlternativeAccidental.AsAccidental()));

            Tones.Add(tone);
            return tone;
        }

        /// <summary>
        ///     add a tone
        /// </summary>
        /// <param name="newtone"></param>
        public void AddTone(Tone newtone)
            => Tones.Add(newtone);

        /// <summary>
        ///     list of tones
        /// </summary>
        public List<Tone> Tones { get; protected set; }
            = new List<Tone>();

    }
}
