namespace AbcTuneTool.Model {

    /// <summary>
    ///     create a new part name
    /// </summary>
    public class PartName : PartItem {

        /// <summary>
        ///     create a new part name
        /// </summary>
        /// <param name="name"></param>
        public PartName(char name)
            => Name = name;

        /// <summary>
        ///     name
        /// </summary>
        public char Name { get; }
    }
}
