using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using AbcTuneTool.Common;
using AbcTuneTool.Model.Fields;

namespace AbcTuneTool.Model {

    /// <summary>
    ///     parts field
    /// </summary>
    public class PartsField : InformationField {

        /// <summary>
        ///     create a new parts field
        /// </summary>
        /// <param name="fieldHeader"></param>
        /// <param name="fieldValues"></param>
        public PartsField(Terminal fieldHeader, Terminal fieldValues) : base(fieldHeader, fieldValues, InformationFieldKind.Parts) {
            var _ = 0;
            Items = ParsePartItems(fieldValues.GetValueAfterWhitespace(0), ref _, 0);
        }

        /// <summary>
        ///     part items
        /// </summary>
        public ImmutableArray<PartItem> Items { get; }

        private ImmutableArray<PartItem> ParsePartItems(string data, ref int offset, int level) {
            var list = new List<PartItem>();

            while (offset < data.Length) {
                var c = data[offset];

                if (c == '.')
                    list.Add(new PartSpace());

                else if (c.IsAsciiLetter())
                    list.Add(new PartName(c));

                else if (c.IsNumber()) {
                    var sb = new StringBuilder();
                    sb.Append(c);
                    offset++;
                    while (offset < data.Length) {
                        c = data[offset];

                        if (!c.IsNumber()) {
                            offset--;
                            break;
                        }

                        sb.Append(c);
                        offset++;
                    }

                    if (int.TryParse(sb.ToString(), out var repeat))
                        list.Add(new PartRepeat(repeat));
                }

                if (c == '(') {
                    offset++;
                    list.Add(new PartGroup(ParsePartItems(data, ref offset, level + 1)));
                }

                else if (c == ')' && level > 0)
                    break;

                offset++;
            }
            return list.ToImmutableArray();
        }
    }
}
