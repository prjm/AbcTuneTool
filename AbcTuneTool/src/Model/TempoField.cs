using System.Collections.Generic;
using System.Collections.Immutable;

namespace AbcTuneTool.Model {

    /// <summary>
    ///     tempo field
    /// </summary>
    public class TempoField : InformationField {

        /// <summary>
        ///     create a new tempo field
        /// </summary>
        /// <param name="fieldHeader"></param>
        /// <param name="fieldValues"></param>
        public TempoField(Terminal fieldHeader, Terminal fieldValues) : base(fieldHeader, fieldValues, InformationFieldKind.Tempo) {

            Tempo = string.Empty;
            var fracts = new List<Fraction>();

            for (var offset = 0; offset < fieldValues.Length; offset++) {
                var value = fieldValues.GetValueAfterWhitespace(offset, out offset);

                if (offset < 0) break;
                if (value.Length < 1) continue;

                if (value[0] == '"' && value[^1] == '"' && value.Length > 2) {
                    Tempo = value[1..^1];
                    continue;
                }

                var index = value.IndexOf("=");
                if (index >= 0) {
                    var f1 = ParseFraction(value.Substring(0, index));
                    fracts.Add(f1);
                    if (int.TryParse(value.Substring(index + 1), out var bpm))
                        Bpm = bpm;
                }
                else {
                    var f2 = ParseFraction(value);
                    fracts.Add(f2);
                }
            }

            Fractions = fracts.ToImmutableArray();

        }

        /// <summary>
        ///     defined tempo
        /// </summary>
        public string Tempo { get; }

        /// <summary>
        ///     beats per minute
        /// </summary>
        public int Bpm { get; }

        /// <summary>
        ///     fractions
        /// </summary>
        public ImmutableArray<Fraction> Fractions { get; }
    }
}
