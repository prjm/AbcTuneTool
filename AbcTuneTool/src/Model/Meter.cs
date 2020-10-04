using System;
using System.Collections.Immutable;
using AbcTuneTool.Model.Symbolic;

namespace AbcTuneTool.Model {

    /// <summary>
    ///     meter definition
    /// </summary>
    public class Meter : IEquatable<Meter> {


        /// <summary>
        ///     create a new meter
        /// </summary>
        /// <param name="fraction"></param>
        public Meter(Fraction fraction)
            => Fractions = ImmutableArray.Create(fraction);


        /// <summary>
        ///     create a new meter
        /// </summary>
        /// <param name="fractions"></param>
        public Meter(params Fraction[] fractions)
            => Fractions = ImmutableArray.Create(fractions);

        /// <summary>
        ///     fractions
        /// </summary>
        public ImmutableArray<Fraction> Fractions { get; }

        /// <summary>
        ///     check for equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Meter? other) {
            if (!(other is Meter m))
                return false;

            if (m.Fractions.Length != Fractions.Length)
                return false;

            for (var i = 0; i < Fractions.Length; i++)
                if (!Fractions[i].Equals(m.Fractions[i]))
                    return false;

            return true;
        }

        /// <summary>
        ///     check for equality
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
            => Equals(obj as Meter);

        /// <summary>
        ///     compute a hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            var hc = new HashCode();

            for (var i = 0; i < Fractions.Length; i++)
                hc.Add(Fractions[i]);

            return hc.ToHashCode();
        }
    }
}