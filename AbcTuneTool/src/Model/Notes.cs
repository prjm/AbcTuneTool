using System.Collections.Generic;

namespace AbcTuneTool.Model {

    /// <summary>
    ///      a set of notes
    /// </summary>
    public class Notes {

        private readonly List<Note> notes =
            new List<Note>();

        /// <summary>
        ///     add a note
        /// </summary>
        /// <param name="value"></param>
        /// <param name="accidental"></param>
        public void AddNote(char value, Accidental accidental)
            => notes.Add(new Note(value, accidental));

        /// <summary>
        ///     get a defined note
        /// </summary>
        /// <param name="value"></param>
        /// <param name="accidental"></param>
        /// <returns></returns>
        public Note GetNote(char value, Accidental accidental) {
            foreach (var note in notes)
                if (note.Value == value && note.Accidental == accidental)
                    return note;

            return Note.Unknown;
        }

    }
}
