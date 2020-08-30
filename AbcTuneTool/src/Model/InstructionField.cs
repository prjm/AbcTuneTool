using AbcTuneTool.Common;

namespace AbcTuneTool.Model {

    /// <summary>
    ///     special information field for instructions
    /// </summary>
    public class InstructionField : InformationField {

        /// <summary>
        ///     create a new instruction field
        /// </summary>
        /// <param name="header"></param>
        /// <param name="values"></param>
        /// <param name="cache"></param>
        /// <param name="pool"></param>
        public InstructionField(Terminal header, Terminal values, StringCache cache, StringBuilderPool pool) :
                base(header, values, InformationFieldKind.Instruction) {

            if (values.Matches(KnownStrings.Linebreak))
                InstrKind = InstructionKind.Linebreak;
            else if (values.Matches(KnownStrings.AbcVersion))
                InstrKind = InstructionKind.Version;
            else if (values.Matches(KnownStrings.AbcCharset))
                InstrKind = InstructionKind.Charset;
            else if (values.Matches(KnownStrings.AbcInclude))
                InstrKind = InstructionKind.Include;
            else if (values.Matches(KnownStrings.AbcCreator))
                InstrKind = InstructionKind.Creator;
            else if (values.Matches(KnownStrings.Decoration))
                InstrKind = InstructionKind.Decoration;
            else
                InstrKind = InstructionKind.Undefied;

            InstrValue = ReadInstructionValue(cache, pool);
        }

        private string ReadInstructionValue(StringCache cache, StringBuilderPool pool) {
            var startPos = InstrKind switch
            {
                InstructionKind.Version => KnownStrings.AbcVersion.Length + 1,
                InstructionKind.Charset => KnownStrings.AbcCharset.Length + 1,
                InstructionKind.Include => KnownStrings.AbcInclude.Length + 1,
                InstructionKind.Creator => KnownStrings.AbcCreator.Length + 1,
                InstructionKind.Decoration => KnownStrings.Decoration.Length + 1,
                InstructionKind.Linebreak => KnownStrings.Linebreak.Length + 1,
                _ => -1,
            };

            if (startPos < 0)
                return string.Empty;

            using var sb = pool.GetItem();
            Value.ToStringBuilder(startPos, sb.Item);
            return cache.ForStringBuilder(sb.Item);
        }


        /// <summary>
        ///     instruction kind
        /// </summary>
        public InstructionKind InstrKind { get; }

        /// <summary>
        ///     instruction value
        /// </summary>
        public string InstrValue { get; }

    }
}
