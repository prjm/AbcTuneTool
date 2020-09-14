using System;

namespace AbcTuneTool.Model {

    /// <summary>
    ///     field with clefs
    /// </summary>
    public class ClefField : InformationField {

        /// <summary>
        ///     create a new clef field
        /// </summary>
        /// <param name="fieldHeader"></param>
        /// <param name="fieldValues"></param>
        /// <param name="kind"></param>
        protected ClefField(Terminal fieldHeader, Terminal fieldValues, InformationFieldKind kind) : base(fieldHeader, fieldValues, kind) { }

        /// <summary>
        ///     get a clef definition for a given field
        /// </summary>
        /// <param name="value"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        protected static ClefSettings GetClefForValue(Terminal value, int offset) {
            var clef = ClefMode.Undefined;
            var clefTranspose = ClefTranspose.Undefined;
            var name = value.GetValueAfterWhitespace(offset, out offset);
            var p1 = value.GetValueAfterWhitespace(offset + 1, out offset);
            var hasClef = false;
            var clefLine = 0;
            var middle = '\0';
            var tranpose = 0;
            var octaves = 0;
            var stafflines = 5;

            ParseClef(ref clef, ref name, ref hasClef, ref clefLine);
            clefTranspose = ParseClefTranspose(clefTranspose, name);
            clefLine = ParseClefLine(clefTranspose, name, hasClef, clefLine);
            ParseClefMiddle(value, ref offset, ref p1, ref middle);
            ParseTranspose(value, ref offset, ref p1, ref tranpose);
            ParseOctave(value, ref offset, ref p1, ref octaves);
            ParseStafflines(value, ref offset, ref p1, ref stafflines);

            if (!hasClef)
                clef = ClefMode.NoClef;

            return new ClefSettings(clef, clefLine, clefTranspose, middle, tranpose, octaves, stafflines);
        }

        private static void ParseTranspose(Terminal value, ref int offset, ref string p1, ref int tranpose) {
            if (p1.StartsWith(KnownStrings.Transpose, StringComparison.OrdinalIgnoreCase)) {
                var eq = p1.IndexOf('=') + 1;
                if (eq == KnownStrings.Transpose.Length + 1 && p1.Length >= eq) {
                    int.TryParse(p1.Substring(eq), out tranpose); ;
                }

                p1 = value.GetValueAfterWhitespace(offset + 1, out offset);
            }
        }

        private static void ParseOctave(Terminal value, ref int offset, ref string p1, ref int octave) {
            if (p1.StartsWith(KnownStrings.Octave, StringComparison.OrdinalIgnoreCase)) {
                var eq = p1.IndexOf('=') + 1;
                if (eq == KnownStrings.Octave.Length + 1 && p1.Length >= eq) {
                    int.TryParse(p1.Substring(eq), out octave); ;
                }

                p1 = value.GetValueAfterWhitespace(offset + 1, out offset);
            }
        }

        private static void ParseStafflines(Terminal value, ref int offset, ref string p1, ref int octave) {
            if (p1.StartsWith(KnownStrings.Stafflines, StringComparison.OrdinalIgnoreCase)) {
                var eq = p1.IndexOf('=') + 1;
                if (eq == KnownStrings.Stafflines.Length + 1 && p1.Length >= eq) {
                    int.TryParse(p1.Substring(eq), out octave); ;
                }

                p1 = value.GetValueAfterWhitespace(offset + 1, out offset);
            }
        }


        private static void ParseClefMiddle(Terminal value, ref int offset, ref string p1, ref char middle) {
            if (p1.StartsWith(KnownStrings.Middle, StringComparison.OrdinalIgnoreCase)) {
                var eq = p1.IndexOf('=') + 1;
                if (eq == KnownStrings.Middle.Length + 1 && p1.Length >= eq && p1[eq] >= 'a' && p1[eq] <= 'g') {
                    middle = p1[eq];
                }
                else if (eq == KnownStrings.Middle.Length + 1 && p1.Length >= eq && p1[eq] >= 'A' && p1[eq] <= 'G') {
                    middle = p1[eq];
                }

                p1 = value.GetValueAfterWhitespace(offset + 1, out offset);
            }
        }

        private static int ParseClefLine(ClefTranspose clefTranspose, string name, bool hasClef, int clefLine) {
            if (hasClef) {
                var lineIndex = name.Length - 1;
                if (clefTranspose != ClefTranspose.Undefined)
                    lineIndex -= 2;
                if (lineIndex < name.Length)
                    clefLine = name[lineIndex] switch
                    {
                        '1' => 1,
                        '2' => 2,
                        '3' => 3,
                        '4' => 4,
                        '5' => 5,
                        _ => clefLine,
                    };
            }

            return clefLine;
        }

        private static ClefTranspose ParseClefTranspose(ClefTranspose clefTranspose, string name) {
            if (name.Length > 0 && name.EndsWith(KnownStrings.AddEight, StringComparison.Ordinal)) {
                clefTranspose = ClefTranspose.AddEight;
            }

            else if (name.Length > 0 && name.EndsWith(KnownStrings.SubtractEight, StringComparison.Ordinal)) {
                clefTranspose = ClefTranspose.SubtractEight;
            }

            return clefTranspose;
        }

        private static void ParseClef(ref ClefMode clef, ref string name, ref bool hasClef, ref int clefLine) {
            var eq = 0;

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
        }
    }
}
