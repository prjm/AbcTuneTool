namespace AbcTuneTool.Model {

    /// <summary>
    ///     a set of key mode notes
    /// </summary>
    public class KeyNotesMode : Notes {

        /// <summary>
        ///     create a set of key notes
        /// </summary>
        /// <param name="name">mode name</param>
        public KeyNotesMode(string name)
            => Name = name;

        /// <summary>
        ///     mode name
        /// </summary>
        public string Name { get; }
    }
}
