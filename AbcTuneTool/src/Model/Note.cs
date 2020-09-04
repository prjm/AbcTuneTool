using System;
using System.Diagnostics.CodeAnalysis;

namespace AbcTuneTool.Model {

    /// <summary>
    ///     note definition
    /// </summary>
    public class Note : IEquatable<Note> {

        /// <summary>
        ///     create a new note
        /// </summary>
        /// <param name="noteValue"></param>
        public Note(char noteValue) {
            Value = noteValue;
            Accidental = Accidental.Undefined;
        }

        /// <summary>
        ///     create a new note
        /// </summary>
        /// <param name="noteValue"></param>
        /// <param name="accidental"></param>
        public Note(char noteValue, Accidental accidental) : this(noteValue)
            => Accidental = accidental;

        /// <summary>
        ///     undefined note
        /// </summary>
        public static Note Unknown { get; }
            = new Note('\0', Accidental.Undefined);

        /// <summary>
        ///     no note
        /// </summary>
        public static Note None { get; }
            = new Note('x', Accidental.Undefined);

        /// <summary>
        ///     note value
        /// </summary>
        public char Value { get; }

        /// <summary>
        ///     accidental value
        /// </summary>
        public Accidental Accidental { get; }

        /// <summary>
        ///     check for equality
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
            => Equals(obj as Note);

        /// <summary>
        ///     compute the hash code of this note
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            var hashCode = new HashCode();
            hashCode.Add(Value);
            hashCode.Add(Accidental);
            return hashCode.ToHashCode();
        }

        /// <summary>
        ///     check for equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals([AllowNull] Note other)
            => other?.Value == Value && other?.Accidental == Accidental;
    }
}
