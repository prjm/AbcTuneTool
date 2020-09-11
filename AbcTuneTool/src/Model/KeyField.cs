﻿using System;
using AbcTuneTool.Common;

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
            var (isValid, table, offset) = GetModeForValue(value);

            IsValidKey = isValid;
            KeyValue = table;

            Clef = GetClefForValue(value, offset);
        }

        private static ClefSettings GetClefForValue(Terminal value, int offset) {
            var clef = ClefMode.Undefined;
            var clefTranspose = ClefTranspose.Undefined;
            var name = value.GetValueAfterWhitespace(offset, out offset);
            var hasClef = false;
            var eq = 0;
            var clefLine = 0;

            if (name.StartsWith(KnownStrings.Clef, StringComparison.OrdinalIgnoreCase) && (eq = name.IndexOf('=')) > 0) {
                name = name.Substring(1 + eq);
            }

            if (name.StartsWith(KnownStrings.Treble, StringComparison.Ordinal)) {
                clef = ClefMode.Treble;
                clefLine = 2;
                hasClef = true;
            }

            else if (name.StartsWith(KnownStrings.Alto, StringComparison.OrdinalIgnoreCase)) {
                clef = ClefMode.Alto;
                clefLine = 3;
                hasClef = true;
            }

            else if (name.StartsWith(KnownStrings.Tenor, StringComparison.OrdinalIgnoreCase)) {
                clef = ClefMode.Tenor;
                clefLine = 4;
                hasClef = true;
            }

            else if (name.StartsWith(KnownStrings.Bass, StringComparison.OrdinalIgnoreCase)) {
                clef = ClefMode.Bass;
                clefLine = 4;
                hasClef = true;
            }

            if (name.EndsWith(KnownStrings.AddEight, StringComparison.Ordinal)) {
                clefTranspose = ClefTranspose.AddEight;
            }

            else if (name.EndsWith(KnownStrings.SubtractEight, StringComparison.Ordinal)) {
                clefTranspose = ClefTranspose.SubtractEight;
            }


            if (!hasClef)
                clef = ClefMode.NoClef;

            return new ClefSettings(clef, clefLine, clefTranspose);
        }

        private static (KeyStatus isValid, KeyTable table, int offset) GetModeForValue(Terminal value) {
            var tone = value.FirstChar;
            var tone2 = value.SecondChar;
            var accidental = tone2.AsAccidental();
            var mode = value.GetValueAfterWhitespace(1);
            bool isValid;
            var describedMode = true;
            var allowsAddAcc = true;
            var hasKey = false;
            var offset = 0;
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
            }

            else if (tone.IsKeyNoteLetter() && string.IsNullOrWhiteSpace(mode) || mode.StartsWith(KnownStrings.Maj, StringComparison.OrdinalIgnoreCase)) {
                result = new MajorKeyTable();
                describedMode = !string.IsNullOrWhiteSpace(mode);
                isValid = result.DefineKey(tone, accidental);
                hasKey = true;
            }

            else if (mode.StartsWith(KnownStrings.Min, StringComparison.OrdinalIgnoreCase) || string.Equals(mode, KnownStrings.M, StringComparison.Ordinal)) {
                result = new MinorKeyTable();
                isValid = result.DefineKey(tone, accidental);
                hasKey = true;
            }

            else if (mode.StartsWith(KnownStrings.Mix, StringComparison.OrdinalIgnoreCase) || string.Equals(mode, KnownStrings.M, StringComparison.Ordinal)) {
                result = new MixolydianKeyTable();
                isValid = result.DefineKey(tone, accidental);
                hasKey = true;
            }

            else if (mode.StartsWith(KnownStrings.Dor, StringComparison.OrdinalIgnoreCase)) {
                result = new DorianKeyTable();
                isValid = result.DefineKey(tone, accidental);
                hasKey = true;
            }

            else if (mode.StartsWith(KnownStrings.Phr, StringComparison.OrdinalIgnoreCase)) {
                result = new PhrygianKeyTable();
                isValid = result.DefineKey(tone, accidental);
                hasKey = true;
            }

            else if (mode.StartsWith(KnownStrings.Lyd, StringComparison.OrdinalIgnoreCase)) {
                result = new LydianKeyTable();
                isValid = result.DefineKey(tone, accidental);
                hasKey = true;
            }

            else if (mode.StartsWith(KnownStrings.Loc, StringComparison.OrdinalIgnoreCase)) {
                result = new LocrianKeyTable();
                isValid = result.DefineKey(tone, accidental);
                hasKey = true;
            }

            else if (value.IsEmpty
                || value.Matches(KnownStrings.None, StringComparison.OrdinalIgnoreCase)
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

            if (isValid && allowsAddAcc) {

                for (var i = 2 + (describedMode ? 1 : 0); i < value.Length && isValid; i++) {
                    var additionalAccidental = value.GetValueAfterWhitespace(i, out var offset1);
                    i = offset1;

                    var addTone = additionalAccidental.AsTonePrefixAccidentals();
                    isValid = isValid && result.Tones.AddAccidental(addTone);
                }

            }

            var status = (hasKey, isValid) switch
            {
                (false, _) => KeyStatus.NoKey,
                (true, false) => KeyStatus.Invalidkey,
                (true, true) => KeyStatus.ValidKey
            };


            return (status, result, offset);
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
