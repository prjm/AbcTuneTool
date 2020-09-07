using System;
using AbcTuneTool.Common;
using AbcTuneTool.src.Model;

namespace AbcTuneTool.Model {

    /// <summary>
    ///     key field
    /// </summary>
    public class KeyField : InformationField {

        /// <summary>
        ///     create a new key field
        /// </summary>
        /// <param name="header"></param>
        /// <param name="value"></param>
        public KeyField(Terminal header, Terminal value) : base(header, value, InformationFieldKind.Key) {
            var tone = value.FirstChar;
            var accidental = value.SecondChar.AsAccidental();

            KeyValue2 = GetModeForValue(tone, accidental, value);
        }

        private static KeyTable GetModeForValue(char tone, Accidental accidental, Terminal value) {
            var mode = value.GetValueAfterWhitespace(1);

            if (string.IsNullOrWhiteSpace(mode) || mode.StartsWith(KnownStrings.Maj, StringComparison.OrdinalIgnoreCase)) {
                var result = new MajorKeyTable();
                result.DefineKey(tone, accidental);
                return result;
            }

            if (mode.StartsWith(KnownStrings.Min, StringComparison.OrdinalIgnoreCase) || string.Equals(mode, KnownStrings.M, StringComparison.Ordinal)) {
                var result = new MinorKeyTable();
                result.DefineKey(tone, accidental);
                return result;
            }

            if (mode.StartsWith(KnownStrings.Mix, StringComparison.OrdinalIgnoreCase) || string.Equals(mode, KnownStrings.M, StringComparison.Ordinal)) {
                var result = new MixolydianKeyTable();
                result.DefineKey(tone, accidental);
                return result;
            }

            if (mode.StartsWith(KnownStrings.Dor, StringComparison.OrdinalIgnoreCase)) {
                var result = new DorianKeyTable();
                result.DefineKey(tone, accidental);
                return result;
            }

            if (mode.StartsWith(KnownStrings.Phr, StringComparison.OrdinalIgnoreCase)) {
                var result = new PhrygianKeyTable();
                result.DefineKey(tone, accidental);
                return result;
            }

            if (mode.StartsWith(KnownStrings.Lyd, StringComparison.OrdinalIgnoreCase)) {
                var result = new LydianKeyTable();
                result.DefineKey(tone, accidental);
                return result;
            }

            if (mode.StartsWith(KnownStrings.Loc, StringComparison.OrdinalIgnoreCase)) {
                var result = new LocrianKeyTable();
                result.DefineKey(tone, accidental);
                return result;
            }

            if (value.IsEmpty || value.Matches(KnownStrings.None, StringComparison.OrdinalIgnoreCase) || value.IsWhitespace)
                return new EmptyKeyTable();

            return default!;
        }

        /// <summary>
        ///     key value
        /// </summary>
        public KeyTable KeyValue2 { get; }
    }
}
