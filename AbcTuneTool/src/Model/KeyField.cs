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
            var keyValue = GetModeForValue(value);

            IsValidKey = keyValue.isValid;
            KeyValue = keyValue.table;
        }

        private static (bool isValid, KeyTable table) GetModeForValue(Terminal value) {
            var tone = value.FirstChar;
            var tone2 = value.SecondChar;
            var accidental = tone2.AsAccidental();
            var mode = value.GetValueAfterWhitespace(1);
            bool isValid;
            var describedMode = true;
            var allowsAddAcc = true;
            KeyTable result;

            if (tone == 'H' && (tone2 == 'P' || tone2 == 'p')) {
                result = new MajorKeyTable();
                isValid = true;
                describedMode = false;
                allowsAddAcc = true;

                if (tone2 == 'p') {
                    result.Tones.AddAccidental(new Tone('f', '#'));
                    result.Tones.AddAccidental(new Tone('c', '#'));
                    result.Tones.AddAccidental(new Tone('g', '='));
                }
            }

            else if (tone.IsKeyNoteLetter() && string.IsNullOrWhiteSpace(mode) || mode.StartsWith(KnownStrings.Maj, StringComparison.OrdinalIgnoreCase)) {
                result = new MajorKeyTable();
                describedMode = !string.IsNullOrWhiteSpace(mode);
                isValid = result.DefineKey(tone, accidental);
            }

            else if (mode.StartsWith(KnownStrings.Min, StringComparison.OrdinalIgnoreCase) || string.Equals(mode, KnownStrings.M, StringComparison.Ordinal)) {
                result = new MinorKeyTable();
                isValid = result.DefineKey(tone, accidental);
            }

            else if (mode.StartsWith(KnownStrings.Mix, StringComparison.OrdinalIgnoreCase) || string.Equals(mode, KnownStrings.M, StringComparison.Ordinal)) {
                result = new MixolydianKeyTable();
                isValid = result.DefineKey(tone, accidental);
            }

            else if (mode.StartsWith(KnownStrings.Dor, StringComparison.OrdinalIgnoreCase)) {
                result = new DorianKeyTable();
                isValid = result.DefineKey(tone, accidental);
            }

            else if (mode.StartsWith(KnownStrings.Phr, StringComparison.OrdinalIgnoreCase)) {
                result = new PhrygianKeyTable();
                isValid = result.DefineKey(tone, accidental);
            }

            else if (mode.StartsWith(KnownStrings.Lyd, StringComparison.OrdinalIgnoreCase)) {
                result = new LydianKeyTable();
                isValid = result.DefineKey(tone, accidental);
            }

            else if (mode.StartsWith(KnownStrings.Loc, StringComparison.OrdinalIgnoreCase)) {
                result = new LocrianKeyTable();
                isValid = result.DefineKey(tone, accidental);
            }

            else if (value.IsEmpty
                || value.Matches(KnownStrings.None, StringComparison.OrdinalIgnoreCase)
                || value.IsWhitespace
                || mode.StartsWith(KnownStrings.Exp, StringComparison.OrdinalIgnoreCase)) {
                result = new EmptyKeyTable();
                describedMode = !value.IsEmpty && !value.IsWhitespace;
                isValid = true;
                allowsAddAcc = describedMode && mode.StartsWith(KnownStrings.Exp, StringComparison.OrdinalIgnoreCase);
            }

            else {
                result = new EmptyKeyTable();
                describedMode = false;
                isValid = false;
            }

            if (isValid && allowsAddAcc) {

                for (var i = 2 + (describedMode ? 1 : 0); i < value.Length && isValid; i++) {
                    var additionalAccidental = value.GetValueAfterWhitespace(i, out var offset);
                    i = offset;

                    var addTone = additionalAccidental.AsTonePrefixAccidentals();
                    isValid = isValid && result.Tones.AddAccidental(addTone);
                }

            }

            return (isValid, result);
        }

        /// <summary>
        ///     <c>true</c> if this is a valid key
        /// </summary>
        public bool IsValidKey { get; }

        /// <summary>
        ///     key value
        /// </summary>
        public KeyTable KeyValue { get; }
    }
}
