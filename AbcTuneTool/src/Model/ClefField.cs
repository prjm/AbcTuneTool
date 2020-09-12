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
            var hasClef = false;
            var clefLine = 0;
            var middle = '\0';

            ParseClef(ref clef, ref name, ref hasClef, ref clefLine);
            clefTranspose = ParseClefTranspose(clefTranspose, name);
            clefLine = ParseClefLine(clefTranspose, name, hasClef, clefLine);

            if (!hasClef)
                clef = ClefMode.NoClef;

            return new ClefSettings(clef, clefLine, clefTranspose);
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
