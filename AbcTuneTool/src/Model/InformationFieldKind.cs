namespace AbcTuneTool.Model {

    /// <summary>
    ///     information fields for headers
    /// </summary>
    public enum InformationFieldKind {


        /// <summary>
        ///     undefined field
        /// </summary>
        Undefined = 0,

        /// <summary>
        ///     area label, deprecated
        /// </summary>
        Area = 1,

        /// <summary>
        ///     book label
        /// </summary>
        Book = 2,

        /// <summary>
        ///     composer label
        /// </summary>
        Composer = 3,

        /// <summary>
        ///     discography label
        /// </summary>
        Discography = 4,

        /// <summary>
        ///     file url label
        /// </summary>
        FileUrl = 5,

        /// <summary>
        ///     instrument group
        /// </summary>
        Group = 6,

        /// <summary>
        ///     history info
        /// </summary>
        History = 7,

        /// <summary>
        ///     instruction
        /// </summary>
        Instruction = 8,

        /// <summary>
        ///     key of the tune
        /// </summary>
        Key = 9,

        /// <summary>
        ///     unit note length
        /// </summary>
        UnitNoteLength = 10,

        /// <summary>
        ///     meter
        /// </summary>
        Meter = 11,

        /// <summary>
        ///     macro
        /// </summary>
        Macro = 12,

        /// <summary>
        ///     notes
        /// </summary>
        Notes = 13,

        /// <summary>
        ///     parts
        /// </summary>
        Parts = 14,

        /// <summary>
        ///     tempo
        /// </summary>
        Tempo = 15,

        /// <summary>
        ///     rhythm kind
        /// </summary>
        Rythm = 16,

        /// <summary>
        ///     remark
        /// </summary>
        Remark = 17,

        /// <summary>
        ///     tune source
        /// </summary>
        Source = 18,

        /// <summary>
        ///     symbol ine
        /// </summary>
        SymbolLine = 19,

        /// <summary>
        ///     tune tile
        /// </summary>
        TuneTitle = 20,

        /// <summary>
        ///     user defined field
        /// </summary>
        UserDefined = 21,

        /// <summary>
        ///     voice
        /// </summary>
        Voice = 22,

        /// <summary>
        ///     words after tune
        /// </summary>
        WordsAfterTune = 23,


        /// <summary>
        ///     words in tune
        /// </summary>
        WordsInTune = 24,


        /// <summary>
        ///     reference number
        /// </summary>
        RefNumber = 25,

        /// <summary>
        ///     transcription by
        /// </summary>
        Transcription = 26,

    }

    /// <summary>
    ///     helper methods for information fields
    /// </summary>
    public static class InformationFieldKindHelper {

        /// <summary>
        ///     test if this symbol can occur in the file header
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static bool InFileHeader(this InformationFieldKind kind) =>
            kind switch
            {
                InformationFieldKind.Area => true,
                InformationFieldKind.Book => true,
                InformationFieldKind.Composer => true,
                InformationFieldKind.Discography => true,
                InformationFieldKind.FileUrl => true,
                InformationFieldKind.Group => true,
                InformationFieldKind.History => true,
                _ => false,
            };

        /// <summary>
        ///     test if this symbol can occur in the tune header
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static bool InTuneHeader(this InformationFieldKind kind) =>
            kind switch
            {
                InformationFieldKind.Area => true,
                InformationFieldKind.Book => true,
                InformationFieldKind.Composer => true,
                InformationFieldKind.Discography => true,
                InformationFieldKind.FileUrl => true,
                InformationFieldKind.Group => true,
                InformationFieldKind.History => true,

                _ => false,
            };

        /// <summary>
        ///     test if this symbol can occur in the tune body
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static bool InTuneBody(this InformationFieldKind kind) =>
            kind switch
            {
                InformationFieldKind.Area => false,
                InformationFieldKind.Book => false,
                InformationFieldKind.Composer => false,
                InformationFieldKind.Discography => false,
                InformationFieldKind.FileUrl => false,
                InformationFieldKind.Group => false,
                InformationFieldKind.History => false,
                _ => false,
            };

        /// <summary>
        ///     test if this symbol can occur inline
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static bool InInline(this InformationFieldKind kind) =>
            kind switch
            {
                InformationFieldKind.Area => false,
                InformationFieldKind.Book => false,
                InformationFieldKind.Composer => false,
                InformationFieldKind.Discography => false,
                InformationFieldKind.FileUrl => false,
                InformationFieldKind.Group => false,
                InformationFieldKind.History => false,

                _ => false,
            };

        /// <summary>
        ///     get the possible content type for an header
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static InformationFieldContent GetContentType(this InformationFieldKind kind) =>
            kind switch
            {
                InformationFieldKind.Area => InformationFieldContent.StringContent,
                InformationFieldKind.Book => InformationFieldContent.StringContent,
                InformationFieldKind.Composer => InformationFieldContent.StringContent,
                InformationFieldKind.Discography => InformationFieldContent.StringContent,
                InformationFieldKind.FileUrl => InformationFieldContent.StringContent,
                InformationFieldKind.Group => InformationFieldContent.StringContent,
                InformationFieldKind.History => InformationFieldContent.StringContent,

                _ => InformationFieldContent.Undefined,
            };

    }

}
