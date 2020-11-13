using System;

namespace AbcTuneTool.Model.TuneElements {

    /// <summary>
    ///     musical note
    /// </summary>
    public class Note : TuneElement {

        /// <summary>
        ///     create a new note
        /// </summary>
        /// <param name="name"></param>
        /// <param name="level"></param>
        public Note(char name, int level) {
            Name = name;
            Level = level;
        }

        /// <summary>
        ///     note name
        /// </summary>
        public char Name { get; }

        /// <summary>
        ///     note level
        /// </summary>
        public int Level { get; }

        /// <summary>
        ///     check for equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(TuneElement? other)
            => other is Note n && n.Level == Level && n.Name == Name;

        /// <summary>
        ///     compute a hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
            => HashCode.Combine(Name, Level);

        /// <summary>
        ///     convert this note to a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => Name.ToString() +
                (Level < 0 ? new string(',', -Level) : new string('\'', Level));
    }
}
