using System;
using AbcTuneTool.Common;
using AbcTuneTool.Model.Fields;
using AbcTuneTool.Model.Symbolic;

namespace AbcTuneTool.Model {

    /// <summary>
    ///     key field
    /// </summary>
    public class KeyField : ClefField {

        /// <summary>
        ///     create a new key field
        /// </summary>
        /// <param name="header"></param>
        /// <param name="value"></param>
        public KeyField(Terminal header, Terminal value) : base(header, value, InformationFieldKind.Key) {
            var (isValid, table, offset) = GetModeForValue(value);

            IsValidKey = isValid;
            KeyValue = table;

            Clef = GetClefForValue(value, offset);
        }

        private static (KeyStatus isValid, KeyTable table, int offset) GetModeForValue(Terminal value) {
            var tone = value.FirstChar;
            var tone2 = value.SecondChar;
            var accidental = tone2.AsAccidental();
            var mode = value.GetValueAfterWhitespace(1, out var offset2);
            var offset1 = 0;
            bool isValid;
            var describedMode = true;
            var allowsAddAcc = true;
            var hasKey = false;
            KeyTable result;

            if (tone == 'H' && (tone2 == 'P' || tone2 == 'p')) {
                result = new MajorKeyTable();
                isValid = true;
                describedMode = false;
                allowsAddAcc = true;
                hasKey = true;

                if (tone2 == 'p') {
                    result.Tones.AddAccidental(new Tone('f', '#'));
                    result.Tones.AddAccidental(new Tone('c', '#'));
                    result.Tones.AddAccidental(new Tone('g', '='));
                }
                offset1 = offset2 + 1;
            }

            else if (tone.IsKeyNoteLetter() && string.IsNullOrWhiteSpace(mode) || mode.StartsWith(KnownStrings.Maj, StringComparison.OrdinalIgnoreCase)) {
                result = new MajorKeyTable();
                describedMode = !string.IsNullOrWhiteSpace(mode);
                isValid = result.DefineKey(tone, accidental);
                hasKey = true;
                offset1 = offset2 + 1;
            }

            else if (mode.StartsWith(KnownStrings.Min, StringComparison.OrdinalIgnoreCase) || string.Equals(mode, KnownStrings.M, StringComparison.Ordinal)) {
                result = new MinorKeyTable();
                isValid = result.DefineKey(tone, accidental);
                hasKey = true;
                offset1 = offset2 + 1;
            }

            else if (mode.StartsWith(KnownStrings.Mix, StringComparison.OrdinalIgnoreCase) || string.Equals(mode, KnownStrings.M, StringComparison.Ordinal)) {
                result = new MixolydianKeyTable();
                isValid = result.DefineKey(tone, accidental);
                hasKey = true;
                offset1 = offset2 + 1;
            }

            else if (mode.StartsWith(KnownStrings.Dor, StringComparison.OrdinalIgnoreCase)) {
                result = new DorianKeyTable();
                isValid = result.DefineKey(tone, accidental);
                hasKey = true;
                offset1 = offset2 + 1;
            }

            else if (mode.StartsWith(KnownStrings.Phr, StringComparison.OrdinalIgnoreCase)) {
                result = new PhrygianKeyTable();
                isValid = result.DefineKey(tone, accidental);
                hasKey = true;
                offset1 = offset2 + 1;
            }

            else if (mode.StartsWith(KnownStrings.Lyd, StringComparison.OrdinalIgnoreCase)) {
                result = new LydianKeyTable();
                isValid = result.DefineKey(tone, accidental);
                hasKey = true;
                offset1 = offset2 + 1;
            }

            else if (mode.StartsWith(KnownStrings.Loc, StringComparison.OrdinalIgnoreCase)) {
                result = new LocrianKeyTable();
                isValid = result.DefineKey(tone, accidental);
                hasKey = true;
                offset1 = offset2 + 1;
            }

            else if (value.IsEmpty
                || value.Matches(KnownStrings.None)
                || value.IsWhitespace
                || mode.StartsWith(KnownStrings.Exp, StringComparison.OrdinalIgnoreCase)) {
                result = new EmptyKeyTable();
                describedMode = !value.IsEmpty && !value.IsWhitespace;
                isValid = true;
                hasKey = true;
                allowsAddAcc = describedMode && mode.StartsWith(KnownStrings.Exp, StringComparison.OrdinalIgnoreCase);
            }

            else {
                result = new EmptyKeyTable();
                describedMode = false;
                isValid = false;
            }

            if (describedMode)
                offset1++;

            if (isValid && allowsAddAcc) {

                for (var i = 2 + (describedMode ? 1 : 0); i < value.Length && isValid; i++) {
                    var additionalAccidental = value.GetValueAfterWhitespace(i, out var offset3);
                    i = offset3;

                    if (additionalAccidental.Length < 1 || additionalAccidental[0].AsAccidental(true) == Accidental.Invalid)
                        break;

                    var addTone = additionalAccidental.AsTonePrefixAccidentals();
                    isValid = isValid && result.Tones.AddAccidental(addTone);
                    offset1 = offset3 + 1;
                }
            }

            var status = (hasKey, isValid) switch
            {
                (false, _) => KeyStatus.NoKey,
                (true, false) => KeyStatus.Invalidkey,
                (true, true) => KeyStatus.ValidKey
            };


            return (status, result, offset1);
        }

        /// <summary>
        ///     <c>true</c> if this is a valid key
        /// </summary>
        public KeyStatus IsValidKey { get; }

        /// <summary>
        ///     key value
        /// </summary>
        public KeyTable KeyValue { get; }

        /// <summary>
        ///     clef
        /// </summary>
        public ClefSettings Clef { get; }

    }
}
