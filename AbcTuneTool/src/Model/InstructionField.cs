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
        public InstructionField(Terminal header, Terminal values, StringCache cache, StringBuilderPool pool) :
                base(header, values, InformationFieldKind.Instruction) {

            if (values.Matches(KnownStrings.AbcVersion))
                InstrKind = InstructionKind.Version;
            else
                InstrKind = InstructionKind.Undefied;

            InstrValue = ReadInstructionValue(cache, pool);
        }

        private string ReadInstructionValue(StringCache cache, StringBuilderPool pool) {
            var startPos = InstrKind switch
            {
                InstructionKind.Version => KnownStrings.AbcVersion.Length + 1,
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
