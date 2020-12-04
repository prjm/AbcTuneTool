using System;
using System.Collections.Immutable;

using AbcTuneTool.Common;
using AbcTuneTool.Model;
using AbcTuneTool.Model.Fields;
using AbcTuneTool.Model.KeyTables;
using AbcTuneTool.Model.Symbolic;
using AbcTuneTool.Model.TuneElements;
using AbcTuneTool.src.Model.Fields;

namespace AbcTuneTool.FileIo {

    /// <summary>
    ///     parser for ABC files
    /// </summary>
    public class Parser : IDisposable {
        bool disposedValue;

        /// <summary>
        ///     create a new ABC file parser
        /// </summary>
        /// <param name="tokenizer"></param>
        /// <param name="listPools">list pools</param>
        public Parser(BufferedAbcTokenizer tokenizer, ListPools listPools) {
            Tokenizer = tokenizer;
            ListPools = listPools;
            Symbols = new DecorationRegistry();
            Shortcuts = new SymbolShortcuts();
        }

        /// <summary>
        ///     tokenizer
        /// </summary>
        public BufferedAbcTokenizer Tokenizer { get; }

        /// <summary>
        ///     list pools
        /// </summary>
        public ListPools ListPools { get; }

        /// <summary>
        ///     symbols
        /// </summary>
        public DecorationRegistry Symbols { get; }

        /// <summary>
        ///     shortcuts
        /// </summary>
        public SymbolShortcuts Shortcuts { get; }

        private Token CurrentToken
            => Tokenizer.Lookahead(0);

        /// <summary>
        ///     parse an information field
        /// </summary>
        /// <returns></returns>
        public InformationField? ParseInformationField() {
            if (Matches(TokenKind.InformationFieldHeader)) {
                var field = CurrentToken;
                NextToken();

                using var values = ListPools.GetTokenList();
                while (!Matches(TokenKind.Eof, TokenKind.Linebreak))
                    values.Add(GetCurrentTokenAndFetchNext());

                if (Matches(TokenKind.Linebreak))
                    values.Add(GetCurrentTokenAndFetchNext());

                var header = new Terminal(field);
                var kind = InformationField.GetKindFor(header.FirstChar);
                var cache = Tokenizer.Tokenizer.Cache;
                var pool = Tokenizer.Tokenizer.StringBuilderPool;
                var fieldValues = new Terminal(values);

                return kind switch {
                    InformationFieldKind.Instruction
                        => new InstructionField(header, fieldValues, cache, pool),

                    InformationFieldKind.Key
                        => ParseKeyField(header, fieldValues),

                    InformationFieldKind.UnitNoteLength
                        => new LengthField(header, fieldValues),

                    InformationFieldKind.Meter
                        => new MeterField(header, fieldValues),

                    InformationFieldKind.Macro
                        => new MacroField(header, fieldValues),

                    InformationFieldKind.Parts
                        => new PartsField(header, fieldValues),

                    InformationFieldKind.Tempo
                        => new TempoField(header, fieldValues),

                    InformationFieldKind.SymbolLine
                        => new SymbolLineField(header, fieldValues, ParseTuneElements(fieldValues)),

                    InformationFieldKind.UserDefined
                        => ParseUserDefinedField(header, fieldValues),

                    InformationFieldKind.Voice
                        => ParseVoiceField(header, fieldValues),

                    InformationFieldKind.RefNumber
                        => new ReferenceNumberField(header, fieldValues),

                    InformationFieldKind.Transcription
                        => new TranscriptionField(header, fieldValues),

                    _ => new InformationField(header, fieldValues, kind),
                };
            }

            return default;
        }

        private VoiceField ParseVoiceField(Terminal header, Terminal fieldValues) {
            var id = fieldValues.GetValueAfterWhitespace(0, out var index);
            var name = string.Empty;
            var subname = string.Empty;
            var stem = StemDirection.Unknown;
            var clef = new ClefSettings(ClefMode.Undefined, 5, ClefTranspose.Undefined, '\0', 0, 0, 0);

            for (index++; index + 2 < fieldValues.Length; index++) {

                var property = fieldValues[index];
                var eq = fieldValues[index + 1];
                var value = fieldValues[index + 2];

                if (string.Equals(property, KnownStrings.Name, StringComparison.OrdinalIgnoreCase))
                    name = value[1..^1];

                else if (string.Equals(property, KnownStrings.Nm, StringComparison.OrdinalIgnoreCase))
                    name = value[1..^1];

                else if (string.Equals(property, KnownStrings.Subname, StringComparison.OrdinalIgnoreCase))
                    subname = value[1..^1];

                else if (string.Equals(property, KnownStrings.Snm, StringComparison.OrdinalIgnoreCase))
                    subname = value[1..^1];

                else if (string.Equals(property, KnownStrings.Stem, StringComparison.OrdinalIgnoreCase)) {

                    if (string.Equals(value, KnownStrings.Up, StringComparison.OrdinalIgnoreCase))
                        stem = StemDirection.Up;
                    else if (string.Equals(value, KnownStrings.Down, StringComparison.OrdinalIgnoreCase))
                        stem = StemDirection.Down;

                }

                else if (string.Equals(property, KnownStrings.Clef, StringComparison.OrdinalIgnoreCase)) {
                    index += 2;
                    clef = GetClefForValue(fieldValues, ref index);
                    if (index < 0)
                        break;
                }

            }

            return new VoiceField(header, fieldValues, id, name, subname, stem, clef);
        }

        private static (KeyStatus isValid, KeyTable table, int offset) GetModeForValue(Terminal value) {
            var tone = value.FirstChar;
            var tone2 = value.SecondChar;
            var accidental = tone2.AsAccidental();
            var mode = value.GetValueAfterWhitespace(1, out var offset2);
            var offset1 = 0;
            bool isValid;
            var describedMode = true;
            var allowsAddAcc = true;
            var hasKey = false;
            KeyTable result;

            if (tone == 'H' && (tone2 == 'P' || tone2 == 'p')) {
                result = new MajorKeyTable();
                isValid = true;
                describedMode = false;
                allowsAddAcc = true;
                hasKey = true;

                if (tone2 == 'p') {
                    result.Tones.AddAccidental(new Tone('f', '#'));
                    result.Tones.AddAccidental(new Tone('c', '#'));
                    result.Tones.AddAccidental(new Tone('g', '='));
                }
                offset1 = offset2 + 1;
            }

            else if (tone.IsKeyNoteLetter() && string.IsNullOrWhiteSpace(mode) || mode.StartsWith(KnownStrings.Maj, StringComparison.OrdinalIgnoreCase)) {
                result = new MajorKeyTable();
                describedMode = !string.IsNullOrWhiteSpace(mode);
                isValid = result.DefineKey(tone, accidental);
                hasKey = true;
                offset1 = offset2 + 1;
            }

            else if (mode.StartsWith(KnownStrings.Min, StringComparison.OrdinalIgnoreCase) || string.Equals(mode, KnownStrings.M, StringComparison.Ordinal)) {
                result = new MinorKeyTable();
                isValid = result.DefineKey(tone, accidental);
                hasKey = true;
                offset1 = offset2 + 1;
            }

            else if (mode.StartsWith(KnownStrings.Mix, StringComparison.OrdinalIgnoreCase) || string.Equals(mode, KnownStrings.M, StringComparison.Ordinal)) {
                result = new MixolydianKeyTable();
                isValid = result.DefineKey(tone, accidental);
                hasKey = true;
                offset1 = offset2 + 1;
            }

            else if (mode.StartsWith(KnownStrings.Dor, StringComparison.OrdinalIgnoreCase)) {
                result = new DorianKeyTable();
                isValid = result.DefineKey(tone, accidental);
                hasKey = true;
                offset1 = offset2 + 1;
            }

            else if (mode.StartsWith(KnownStrings.Phr, StringComparison.OrdinalIgnoreCase)) {
                result = new PhrygianKeyTable();
                isValid = result.DefineKey(tone, accidental);
                hasKey = true;
                offset1 = offset2 + 1;
            }

            else if (mode.StartsWith(KnownStrings.Lyd, StringComparison.OrdinalIgnoreCase)) {
                result = new LydianKeyTable();
                isValid = result.DefineKey(tone, accidental);
                hasKey = true;
                offset1 = offset2 + 1;
            }

            else if (mode.StartsWith(KnownStrings.Loc, StringComparison.OrdinalIgnoreCase)) {
                result = new LocrianKeyTable();
                isValid = result.DefineKey(tone, accidental);
                hasKey = true;
                offset1 = offset2 + 1;
            }

            else if (value.IsEmpty
                || value.Matches(KnownStrings.None)
                || value.IsWhitespace
                || mode.StartsWith(KnownStrings.Exp, StringComparison.OrdinalIgnoreCase)) {
                result = new EmptyKeyTable();
                describedMode = !value.IsEmpty && !value.IsWhitespace;
                isValid = true;
                hasKey = true;
                allowsAddAcc = describedMode && mode.StartsWith(KnownStrings.Exp, StringComparison.OrdinalIgnoreCase);
            }

            else {
                result = new EmptyKeyTable();
                describedMode = false;
                isValid = false;
            }

            if (describedMode)
                offset1++;

            if (isValid && allowsAddAcc) {

                for (var i = 2 + (describedMode ? 1 : 0); i < value.Length && isValid; i++) {
                    var additionalAccidental = value.GetValueAfterWhitespace(i, out var offset3);
                    i = offset3;

                    if (additionalAccidental.Length < 1)
                        break;

                    var acc = additionalAccidental[0].AsAccidental(true);

                    if (acc == Accidental.Invalid)
                        break;

                    var name = additionalAccidental;
                    if (acc == Accidental.Natural && i + 1 < value.Length && value[i + 1].Length > 0) {
                        name = value[i + 1];
                        i++;
                    }

                    var addTone = additionalAccidental.AsTonePrefixAccidentals(name);
                    isValid = isValid && result.Tones.AddAccidental(addTone);
                    offset1 = offset3 + 1;
                }
            }

            var status = (hasKey, isValid) switch {
                (false, _) => KeyStatus.NoKey,
                (true, false) => KeyStatus.Invalidkey,
                (true, true) => KeyStatus.ValidKey
            };


            return (status, result, offset1);
        }

        private KeyField ParseKeyField(Terminal header, Terminal fieldValues) {
            var (isValid, table, offset) = GetModeForValue(fieldValues);
            var clef = GetClefForValue(fieldValues, ref offset);

            return new KeyField(header, fieldValues, clef, table, isValid);

        }

        private UserDefinedField ParseUserDefinedField(Terminal header, Terminal fieldValues) {
            var alias = fieldValues.GetValueAfterWhitespace(0, out var index);
            _ = fieldValues.GetValueAfterWhitespace(1 + index, out index);
            var symbol = fieldValues.GetValueAfterWhitespace(1 + index, out _);
            return new UserDefinedField(header, fieldValues, alias, ParseTuneElement(symbol));
        }

        private ImmutableArray<TuneElement> ParseTuneElements(Terminal fieldValues) {
            using var list = ListPools.ObjectLists.Rent();

            for (var i = 0; i < fieldValues.Length; i++) {

                var text = fieldValues[i];

                if (text.Length < 1) continue;

                var element = ParseTuneElement(text);
                if (element != default)
                    list.Add(element);
            }

            return list.ToImmutableArray<TuneElement>();
        }

        private TuneElement? ParseTuneElement(string text) {
            var element = default(TuneElement);

            if (text[0] == '"' && text.Length > 3) {
                element = new Annotation(text[1].AsPosition(), text[2..^1]);
            }

            else if (text.Length == 1 && text[0].IsDecorationShortcut() && Shortcuts.Shortcuts.TryGetValue(text[0], out var symbol2)) {
                element = new TuneSymbol(symbol2);
            }

            else if (text[0] == '!' && text[^1] == '!' && text.Length > 2) {
                var symbols = text[1..^1];
                if (Symbols.Symbols.TryGetValue(symbols, out var symbol))
                    element = new TuneSymbol(symbol);
                else if (string.Equals(symbols, KnownStrings.None, StringComparison.OrdinalIgnoreCase))
                    element = new UndefinedTuneSymbol();
                else if (string.Equals(symbols, KnownStrings.Nil, StringComparison.OrdinalIgnoreCase))
                    element = new UndefinedTuneSymbol();
            }

            else if (text[0].IsNoteLetter()) {
                var firstNote = text[0];
                var accidental = text[1].AsAccidental(false, Accidental.Undefined);
                var slashIndex = text.IndexOf('/');
                var bassNote = '\0';
                var bassAccidental = Accidental.Undefined;
                var type = string.Empty;
                var startIndex = accidental != Accidental.Undefined ? 2 : 1;

                if (slashIndex >= 0 && slashIndex + 1 < text.Length) {
                    if (slashIndex > startIndex & text.Length - startIndex - slashIndex > 0)
                        type = text.Substring(startIndex, text.Length - startIndex - slashIndex - 1);

                    bassNote = text[slashIndex + 1];
                    if (slashIndex + 2 < text.Length)
                        bassAccidental = text[slashIndex + 2].AsAccidental();
                }
                else {
                    type = text.Substring(startIndex);
                }

                element = new ChordSymbol(firstNote, accidental, type, bassNote, bassAccidental);
            }

            return element;
        }

        private Token GetCurrentTokenAndFetchNext() {
            var result = CurrentToken;
            NextToken();
            return result;
        }

        /// <summary>
        ///     get a clef definition for a given field
        /// </summary>
        /// <param name="value"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        protected static ClefSettings GetClefForValue(Terminal value, ref int offset) {
            var clef = ClefMode.Undefined;
            var clefTranspose = ClefTranspose.Undefined;
            var name = value.GetValueAfterWhitespace(offset, out offset);

            if (string.Equals(name, KnownStrings.Clef, StringComparison.OrdinalIgnoreCase)) {
                var eq = value.GetValueAfterWhitespace(offset + 1, out offset);
                name = value.GetValueAfterWhitespace(offset + 1, out offset);
            }

            var p1 = value.GetValueAfterWhitespace(offset + 1, out offset);
            var hasClef = false;
            var clefLine = 0;
            var middle = '\0';
            var tranpose = 0;
            var octaves = 0;
            var stafflines = 5;

            ParseClef(ref clef, ref name, ref hasClef, ref clefLine);
            clefTranspose = ParseClefTranspose(clefTranspose, name);
            clefLine = ParseClefLine(clefTranspose, name, hasClef, clefLine);
            ParseClefMiddle(value, ref offset, ref p1, ref middle);
            ParseClefTranspose(value, ref offset, ref p1, ref tranpose);
            ParseClefOctave(value, ref offset, ref p1, ref octaves);
            ParseCleffStafflines(value, ref offset, ref p1, ref stafflines);

            if (!hasClef)
                clef = ClefMode.NoClef;

            return new ClefSettings(clef, clefLine, clefTranspose, middle, tranpose, octaves, stafflines);
        }

        private static void ParseClefTranspose(Terminal value, ref int offset, ref string p1, ref int tranpose) {
            if (string.Equals(p1, KnownStrings.Transpose, StringComparison.OrdinalIgnoreCase)) {
                var eq = value.GetValueAfterWhitespace(offset + 1, out offset);
                p1 = value.GetValueAfterWhitespace(offset + 1, out offset);
                if (p1.Length >= 1) {
                    int.TryParse(p1, out tranpose); ;
                }

                p1 = value.GetValueAfterWhitespace(offset + 1, out offset);
            }
        }

        private static void ParseClefOctave(Terminal value, ref int offset, ref string p1, ref int octave) {
            if (string.Equals(p1, KnownStrings.Octave, StringComparison.OrdinalIgnoreCase)) {
                var eq = value.GetValueAfterWhitespace(offset + 1, out offset);
                p1 = value.GetValueAfterWhitespace(offset + 1, out offset);
                if (p1.Length >= 1) {
                    int.TryParse(p1, out octave);
                }

                p1 = value.GetValueAfterWhitespace(offset + 1, out offset);
            }
        }

        private static void ParseCleffStafflines(Terminal value, ref int offset, ref string p1, ref int stafflines) {
            if (p1.StartsWith(KnownStrings.Stafflines, StringComparison.OrdinalIgnoreCase)) {
                var eq = value.GetValueAfterWhitespace(offset + 1, out offset);
                p1 = value.GetValueAfterWhitespace(offset + 1, out offset);
                if (p1.Length >= 1) {
                    int.TryParse(p1, out stafflines); ;
                }

                p1 = value.GetValueAfterWhitespace(offset + 1, out offset);
            }
        }


        private static void ParseClefMiddle(Terminal value, ref int offset, ref string p1, ref char middle) {
            if (string.Equals(p1, KnownStrings.Middle, StringComparison.OrdinalIgnoreCase)) {
                var eq = value.GetValueAfterWhitespace(offset + 1, out offset);
                p1 = value.GetValueAfterWhitespace(offset + 1, out offset);
                if (p1.Length >= 1 && p1[0] >= 'a' && p1[0] <= 'g') {
                    middle = p1[0];
                }
                else if (p1.Length >= 1 && p1[0] >= 'A' && p1[0] <= 'G') {
                    middle = p1[0];
                }

                p1 = value.GetValueAfterWhitespace(offset + 1, out offset);
            }
        }

        private static int ParseClefLine(ClefTranspose clefTranspose, string name, bool hasClef, int clefLine) {
            if (hasClef) {
                var lineIndex = name.Length - 1;
                if (clefTranspose != ClefTranspose.Undefined)
                    lineIndex -= 2;
                if (lineIndex < name.Length)
                    clefLine = name[lineIndex] switch {
                        '1' => 1,
                        '2' => 2,
                        '3' => 3,
                        '4' => 4,
                        '5' => 5,
                        _ => clefLine,
                    };
            }

            return clefLine;
        }

        private static ClefTranspose ParseClefTranspose(ClefTranspose clefTranspose, string name) {
            if (name.Length > 0 && name.EndsWith(KnownStrings.AddEight, StringComparison.Ordinal)) {
                clefTranspose = ClefTranspose.AddEight;
            }

            else if (name.Length > 0 && name.EndsWith(KnownStrings.SubtractEight, StringComparison.Ordinal)) {
                clefTranspose = ClefTranspose.SubtractEight;
            }

            return clefTranspose;
        }

        private static void ParseClef(ref ClefMode clef, ref string name, ref bool hasClef, ref int clefLine) {
            var eq = 0;

            if (name.StartsWith(KnownStrings.Clef, StringComparison.OrdinalIgnoreCase) && (eq = name.IndexOf('=')) > 0) {
                name = name.Substring(1 + eq);
            }

            if (name.StartsWith(KnownStrings.Treble, StringComparison.Ordinal)) {
                clef = ClefMode.Treble;
                clefLine = 2;
                hasClef = true;
            }

            else if (name.StartsWith(KnownStrings.Alto, StringComparison.OrdinalIgnoreCase)) {
                clef = ClefMode.Alto;
                clefLine = 3;
                hasClef = true;
            }

            else if (name.StartsWith(KnownStrings.Tenor, StringComparison.OrdinalIgnoreCase)) {
                clef = ClefMode.Tenor;
                clefLine = 4;
                hasClef = true;
            }

            else if (name.StartsWith(KnownStrings.Bass, StringComparison.OrdinalIgnoreCase)) {
                clef = ClefMode.Bass;
                clefLine = 4;
                hasClef = true;
            }
        }

        /// <summary>
        ///     parse a set of information fields
        /// </summary>
        /// <returns></returns>
        public InformationFields ParseInformationFields() {
            using var values = ListPools.GetObjectList();
            while (Matches(TokenKind.InformationFieldHeader)) {
                var field = ParseInformationField();
                if (!(field is null))
                    values.Add(field);
            }

            return new InformationFields(values.ToImmutableArray<InformationField>());
        }

        /// <summary>
        ///     parse the lines before the header
        /// </summary>
        /// <returns></returns>
        public OtherLines ParseLinesBeforeHeader() {
            using var values = ListPools.GetTokenList();

            while (!Matches(TokenKind.Eof, TokenKind.InformationFieldHeader)) {
                values.Add(GetCurrentTokenAndFetchNext());
            }
            return new OtherLines(new Terminal(values.Item.ToImmutableArray<Token>()));
        }

        /// <summary>
        ///     parse a single tune
        /// </summary>
        /// <returns></returns>
        public Tune ParseTune(OtherLines otherLines, bool isFirst, InformationFields fileHeader, out bool hasHeader) {
            var header = InformationFields.Empty;

            if (Matches(TokenKind.InformationFieldHeader)) {
                header = ParseInformationFields();
                hasHeader = true;
            }
            else {
                header = fileHeader;
                hasHeader = false;
            }

            return new Tune(otherLines, header, ParseTuneBody());
        }

        /// <summary>
        ///     parse tune body
        /// </summary>
        /// <returns></returns>
        public TuneBody ParseTuneBody() {
            using var items = ListPools.ObjectLists.Rent();

            while (!Matches(TokenKind.Eof, TokenKind.EmptyLine)) {

                if (Matches(TokenKind.Char) && CurrentToken.Value[0].IsNoteLetter())
                    items.Add(ParseNote());

            }

            return new TuneBody(items.ToImmutableArray<TuneElement>());
        }

        private Note ParseNote() {
            var letter = CurrentToken.Value[0];
            var level = letter.IsLowercaseNoteLetter() ? 0 : -1;
            using var tokens = ListPools.TokenLists.Rent();
            tokens.Add(CurrentToken);
            NextToken();

            while (Matches(TokenKind.Comma, TokenKind.Apostrophe)) {
                tokens.Add(CurrentToken);
                level += CurrentToken.Kind switch {
                    TokenKind.Comma => -1,
                    TokenKind.Apostrophe => +1,
                    _ => 0
                };
                NextToken();
            }

            return new Note(new Terminal(tokens.ToImmutableArray()), letter, level);
        }

        private VersionComment ExtractVersion(Token token) {
            var dashIndex = token.OriginalValue.IndexOf("-", StringComparison.Ordinal) + 1;
            var terminal = new Terminal(token);

            if (dashIndex != KnownStrings.VersionComment.Length) {
                return new VersionComment(terminal, KnownStrings.UndefinedVersion);
            }

            NextToken();
            return new VersionComment(terminal, token.OriginalValue[dashIndex..]);
        }

        /// <summary>
        ///     parse a tune book
        /// </summary>
        /// <returns></returns>
        public TuneBook ParseTuneBook() {
            var hasFileHeader = false;
            var fileHeader = InformationFields.Empty;
            using var list = ListPools.ObjectLists.Rent();
            var version = ExtractVersion();

            while (!Matches(TokenKind.Eof)) {

                var otherLines = ParseLinesBeforeHeader();

                if (Matches(TokenKind.InformationFieldHeader) && !hasFileHeader) {
                    fileHeader = ParseInformationFields();
                    hasFileHeader = true;
                }

                list.Add(ParseTune(otherLines, list.Item.Count < 1, fileHeader, out var hasHeader));

                if (list.Item.Count == 1 && !hasHeader)
                    fileHeader = InformationFields.Empty;

            }

            return new TuneBook(version, fileHeader, list.ToImmutableArray<Tune>());
        }

        private VersionComment ExtractVersion() {
            if (Matches(TokenKind.Comment) && CurrentToken.OriginalValue.StartsWith(KnownStrings.VersionComment, StringComparison.OrdinalIgnoreCase)) {
                return ExtractVersion(CurrentToken);
            }

            return new VersionComment(new Terminal(new Token()), KnownStrings.UndefinedVersion);
        }

        private bool Matches(TokenKind kind1, TokenKind kind2)
            => CurrentToken.Kind == kind1 || CurrentToken.Kind == kind2;

        private bool Matches(TokenKind kind)
            => CurrentToken.Kind == kind;

        private void NextToken()
            => Tokenizer.NextToken();

        /// <summary>
        ///     dispose this parser
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    Tokenizer.Dispose();
                }
                disposedValue = true;
            }
        }

        /// <summary>
        ///     dispose this parser
        /// </summary>
        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
