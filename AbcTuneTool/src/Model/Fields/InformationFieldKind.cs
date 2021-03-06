﻿namespace AbcTuneTool.Model.Fields {

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
        ///     origin
        /// </summary>
        Origin = 14,

        /// <summary>
        ///     parts
        /// </summary>
        Parts = 15,

        /// <summary>
        ///     tempo
        /// </summary>
        Tempo = 16,

        /// <summary>
        ///     rhythm kind
        /// </summary>
        Rythm = 17,

        /// <summary>
        ///     remark
        /// </summary>
        Remark = 18,

        /// <summary>
        ///     tune source
        /// </summary>
        Source = 19,

        /// <summary>
        ///     symbol line
        /// </summary>
        SymbolLine = 20,

        /// <summary>
        ///     tune tile
        /// </summary>
        TuneTitle = 21,

        /// <summary>
        ///     user defined field
        /// </summary>
        UserDefined = 22,

        /// <summary>
        ///     voice
        /// </summary>
        Voice = 23,

        /// <summary>
        ///     words after tune
        /// </summary>
        WordsAfterTune = 24,


        /// <summary>
        ///     words in tune
        /// </summary>
        WordsInTune = 25,


        /// <summary>
        ///     reference number
        /// </summary>
        RefNumber = 26,

        /// <summary>
        ///     transcription by
        /// </summary>
        Transcription = 27,

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
                InformationFieldKind.Instruction => true,
                InformationFieldKind.Key => false,
                InformationFieldKind.UnitNoteLength => true,
                InformationFieldKind.Meter => true,
                InformationFieldKind.Macro => true,
                InformationFieldKind.Notes => true,
                InformationFieldKind.Origin => true,
                InformationFieldKind.Parts => false,
                InformationFieldKind.Tempo => false,
                InformationFieldKind.Rythm => true,
                InformationFieldKind.Remark => true,
                InformationFieldKind.Source => true,
                InformationFieldKind.SymbolLine => false,
                InformationFieldKind.TuneTitle => false,
                InformationFieldKind.UserDefined => true,
                InformationFieldKind.Voice => false,
                InformationFieldKind.WordsAfterTune => false,
                InformationFieldKind.WordsInTune => false,
                InformationFieldKind.RefNumber => false,
                InformationFieldKind.Transcription => true,
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
                InformationFieldKind.Instruction => true,
                InformationFieldKind.Key => true,
                InformationFieldKind.UnitNoteLength => true,
                InformationFieldKind.Meter => true,
                InformationFieldKind.Macro => true,
                InformationFieldKind.Notes => true,
                InformationFieldKind.Origin => true,
                InformationFieldKind.Parts => true,
                InformationFieldKind.Tempo => true,
                InformationFieldKind.Rythm => true,
                InformationFieldKind.Remark => true,
                InformationFieldKind.Source => true,
                InformationFieldKind.SymbolLine => false,
                InformationFieldKind.TuneTitle => true,
                InformationFieldKind.UserDefined => true,
                InformationFieldKind.Voice => true,
                InformationFieldKind.WordsAfterTune => true,
                InformationFieldKind.WordsInTune => false,
                InformationFieldKind.RefNumber => true,
                InformationFieldKind.Transcription => true,
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
                InformationFieldKind.Instruction => true,
                InformationFieldKind.Key => true,
                InformationFieldKind.UnitNoteLength => true,
                InformationFieldKind.Meter => true,
                InformationFieldKind.Macro => true,
                InformationFieldKind.Notes => true,
                InformationFieldKind.Origin => false,
                InformationFieldKind.Parts => true,
                InformationFieldKind.Tempo => true,
                InformationFieldKind.Rythm => true,
                InformationFieldKind.Remark => true,
                InformationFieldKind.Source => false,
                InformationFieldKind.SymbolLine => true,
                InformationFieldKind.TuneTitle => true,
                InformationFieldKind.UserDefined => true,
                InformationFieldKind.Voice => true,
                InformationFieldKind.WordsAfterTune => true,
                InformationFieldKind.WordsInTune => true,
                InformationFieldKind.RefNumber => false,
                InformationFieldKind.Transcription => false,
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
                InformationFieldKind.Instruction => true,
                InformationFieldKind.Key => true,
                InformationFieldKind.UnitNoteLength => true,
                InformationFieldKind.Meter => true,
                InformationFieldKind.Macro => true,
                InformationFieldKind.Notes => true,
                InformationFieldKind.Origin => false,
                InformationFieldKind.Parts => true,
                InformationFieldKind.Tempo => true,
                InformationFieldKind.Rythm => true,
                InformationFieldKind.Remark => true,
                InformationFieldKind.Source => false,
                InformationFieldKind.SymbolLine => false,
                InformationFieldKind.TuneTitle => false,
                InformationFieldKind.UserDefined => true,
                InformationFieldKind.Voice => true,
                InformationFieldKind.WordsAfterTune => false,
                InformationFieldKind.WordsInTune => false,
                InformationFieldKind.RefNumber => false,
                InformationFieldKind.Transcription => false,
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
                InformationFieldKind.Instruction => InformationFieldContent.Instruction,
                InformationFieldKind.Key => InformationFieldContent.Key,
                InformationFieldKind.UnitNoteLength => InformationFieldContent.NoteLength,
                InformationFieldKind.Meter => InformationFieldContent.Meter,
                InformationFieldKind.Macro => InformationFieldContent.Macro,
                InformationFieldKind.Notes => InformationFieldContent.StringContent,
                InformationFieldKind.Origin => InformationFieldContent.Origin,
                InformationFieldKind.Parts => InformationFieldContent.Parts,
                InformationFieldKind.Tempo => InformationFieldContent.Tempo,
                InformationFieldKind.Rythm => InformationFieldContent.StringContent,
                InformationFieldKind.Remark => InformationFieldContent.StringContent,
                InformationFieldKind.Source => InformationFieldContent.StringContent,
                InformationFieldKind.SymbolLine => InformationFieldContent.Symbols,
                InformationFieldKind.TuneTitle => InformationFieldContent.StringContent,
                InformationFieldKind.UserDefined => InformationFieldContent.UserDefined,
                InformationFieldKind.Voice => InformationFieldContent.Voice,
                InformationFieldKind.WordsAfterTune => InformationFieldContent.Words,
                InformationFieldKind.WordsInTune => InformationFieldContent.Words,
                InformationFieldKind.RefNumber => InformationFieldContent.RefNumber,
                InformationFieldKind.Transcription => InformationFieldContent.Transcription,
                _ => InformationFieldContent.Undefined,
            };

    }

}
