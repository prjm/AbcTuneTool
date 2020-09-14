using System;
using System.Diagnostics;

namespace AbcTuneTool.Model {

    /// <summary>
    ///     definition of a fraction
    /// </summary>
    [DebuggerDisplay(KnownStrings.DebuggerDisplay)]
    public class Fraction : IEquatable<Fraction> {

        /// <summary>
        ///     create a new fractional value
        /// </summary>
        /// <param name="value"></param>
        public Fraction(string value) {
            var firstIndex = value.IndexOf('/');
            var lastIndex = value.LastIndexOf('/');

            if (firstIndex == lastIndex && firstIndex > 0) {
                if (int.TryParse(value.Substring(0, firstIndex), out var n) && int.TryParse(value.Substring(1 + firstIndex), out var d)) {
                    Numerator = n;
                    Denominator = d;
                    return;
                }
            }

            Numerator = 0;
            Denominator = 0;
        }

        /// <summary>
        ///     create a new fractional value
        /// </summary>
        /// <param name="numerator"></param>
        /// <param name="denominator"></param>
        public Fraction(int numerator, int denominator) {
            Numerator = numerator;
            Denominator = denominator;
        }

        /// <summary>
        ///     display this fraction
        /// </summary>
        public string DebuggerDisplay
            => Valid ? $"{Numerator}/{Denominator}" : KnownStrings.Invalid;

        /// <summary>
        ///     numerator
        /// </summary>
        public int Numerator { get; }

        /// <summary>
        ///     denominator
        /// </summary>
        public int Denominator { get; }

        /// <summary>
        ///     check validity of this fraction
        /// </summary>
        public bool Valid
            => Denominator != 0;

        /// <summary>
        ///     check for equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Fraction? other)
            => other is Fraction f &&
                f.Denominator == Denominator &&
                f.Numerator == Numerator;


        /// <summary>
        ///     check for equality
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
            => Equals(obj as Fraction);

        /// <summary>
        ///     compute a hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
            => HashCode.Combine(Numerator, Denominator);

        /// <summary>
        ///     display this fraction
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => DebuggerDisplay;
    }
}
