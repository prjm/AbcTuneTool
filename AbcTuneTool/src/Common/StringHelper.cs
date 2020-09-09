using AbcTuneTool.Model;

namespace AbcTuneTool.Common {

    /// <summary>
    ///     helper functions for strings
    /// </summary>
    public static class StringHelper {

        /// <summary>
        ///     get the accidental defined by this string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Accidental AsAccidental(this string data) {

            if (string.IsNullOrWhiteSpace(data))
                return Accidental.Undefined;

            return data switch
            {
                "^" => Accidental.Sharp,
                "#" => Accidental.Sharp,
                "_" => Accidental.Flat,
                "b" => Accidental.Flat,
                "=" => Accidental.Natural,
                "^^" => Accidental.DoubleSharp,
                "##" => Accidental.DoubleSharp,
                "__" => Accidental.DoubleFlat,
                "bb" => Accidental.DoubleFlat,
                _ => Accidental.Invalid,
            };
        }

        /// <summary>
        ///     converts this string to a tone
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Tone AsTonePrefixAccidentals(this string data) {
            if (data.Length < 1)
                return new Tone(' ', ' ');

            var name = data[^1];
            var a = Accidental.Undefined;

            if (data.Length > 1) {
                var a1 = data[0].AsAccidental();
                var a2 = Accidental.Undefined;

                if (a1 == Accidental.Invalid)
                    return new Tone(' ', ' ');

                if (data.Length > 2) {
                    a2 = data[1].AsAccidental();
                }

                if (a1 == Accidental.Sharp && a2 == Accidental.Sharp)
                    a = Accidental.DoubleSharp;

                else if (a2 == Accidental.Flat && a2 == Accidental.Flat)
                    a = Accidental.DoubleFlat;

                else
                    a = a1;
            }

            return new Tone(name, a);
        }

    }
}
