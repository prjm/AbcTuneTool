namespace AbcTuneTool.Model.KeyTables {

    /// <summary>
    ///     empty key table
    /// </summary>
    public class EmptyKeyTable : KeyTable {

        /// <summary>
        ///     create a new empty key table
        /// </summary>
        public EmptyKeyTable() : base(new StandardToneSystem(KnownStrings.MajorMode)) {

        }

    }
}
