﻿using System;
using System.Diagnostics;

namespace AbcTuneTool.Model {

    /// <summary>
    ///     tone definition
    /// </summary>
    [DebuggerDisplay(KnownStrings.DebuggerDisplay)]
    public class Tone : IEquatable<Tone> {

        /// <summary>
        ///     create a new tone
        /// </summary>
        /// <param name="name"></param>
        /// <param name="accidental"></param>
        public Tone(char name, Accidental accidental) {
            Name = name;
            Accidental = accidental;
        }

        /// <summary>
        ///     create a new tone
        /// </summary>
        /// <param name="name"></param>
        /// <param name="accidental"></param>
        /// <param name="alternative"></param>
        public Tone(char name, Accidental accidental, Tone alternative) : this(name, accidental)
            => LowerAlternative = alternative;

        /// <summary>
        ///     create a new tone
        /// </summary>
        /// <param name="name"></param>
        /// <param name="accidental"></param>
        /// <param name="lowerAlternative"></param>
        /// <param name="upperAlternative"></param>
        public Tone(char name, Accidental accidental, Tone lowerAlternative, Tone upperAlternative) : this(name, accidental, lowerAlternative)
            => UpperAlternative = upperAlternative;


        /// <summary>
        ///     tone name
        /// </summary>
        public char Name { get; }

        /// <summary>
        ///     accidental
        /// </summary>
        public Accidental Accidental { get; }

        /// <summary>
        ///     debugger display helper
        /// </summary>
        public string DebuggerDisplay {
            get {
                Span<char> m = stackalloc char[2];
                m[0] = Name;
                m[1] = Accidental.AsChar();
                return new string(m).Trim();
            }
        }

        /// <summary>
        ///     lower alternative
        /// </summary>
        public Tone? LowerAlternative { get; }

        /// <summary>
        ///     upper alternative
        /// </summary>
        public Tone? UpperAlternative { get; }

        /// <summary>
        ///     check for equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Tone? other)
            => !(other is null) && other.Name == Name && other.Accidental == Accidental;

        /// <summary>
        ///     check for equality
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
            => Equals(obj as Tone);

        /// <summary>
        ///     generate hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
            => HashCode.Combine(Name, Accidental);

        /// <summary>
        ///     string display
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => DebuggerDisplay;

    }
}
