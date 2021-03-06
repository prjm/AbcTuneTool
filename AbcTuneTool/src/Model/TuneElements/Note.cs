﻿using System;

namespace AbcTuneTool.Model.TuneElements {

    /// <summary>
    ///     musical note
    /// </summary>
    public class Note : TuneElement {

        /// <summary>
        ///     create a new note
        /// </summary>
        /// <param name="terminal"></param>
        /// <param name="name"></param>
        /// <param name="level"></param>
        public Note(Terminal terminal, char name, int level) {
            Terminal = terminal;
            Name = name;
            Level = level;
        }

        /// <summary>
        ///     terminal
        /// </summary>
        public Terminal Terminal { get; }

        /// <summary>
        ///     note name
        /// </summary>
        public char Name { get; }

        /// <summary>
        ///     note level
        /// </summary>
        public int Level { get; }

        /// <summary>
        ///     accept a visitor
        /// </summary>
        /// <param name="visitor"></param>
        public override bool Accept(ISyntaxTreeVisitor visitor) =>
            visitor.StartVisitNode(this) &&
            Terminal.Accept(visitor) &&
            visitor.EndVisitNode(this);

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
