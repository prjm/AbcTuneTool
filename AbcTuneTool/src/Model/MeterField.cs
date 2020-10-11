using System;
using System.Collections.Generic;
using AbcTuneTool.Model.Symbolic;

namespace AbcTuneTool.Model {

    /// <summary>
    ///     meter definition field
    /// </summary>
    public class MeterField : InformationField {

        /// <summary>
        ///     create a new meter field
        /// </summary>
        /// <param name="fieldHeader"></param>
        /// <param name="fieldValues"></param>
        public MeterField(Terminal fieldHeader, Terminal fieldValues) : base(fieldHeader, fieldValues, InformationFieldKind.Meter) {

            var value = fieldValues.GetValueAfterWhitespace(0);

            if (value.StartsWith(KnownStrings.CLine, StringComparison.OrdinalIgnoreCase))
                MeterValue = new Meter(new Fraction(2, 2));

            else if (value.StartsWith(KnownStrings.C, StringComparison.OrdinalIgnoreCase))
                MeterValue = new Meter(new Fraction(4, 4));


            else {

                if (value.Contains('+')) {

                    var firstIndex = value.IndexOf('/');
                    var lastIndex = value.LastIndexOf('/');

                    if (firstIndex == lastIndex && firstIndex > 0 && int.TryParse(value.Substring(firstIndex + 1), out var d)) {

                        var data = value.Substring(0, firstIndex).Split(new char[] { '(', ')', '+', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        var list = new List<int>();
                        foreach (var item in data) {
                            if (int.TryParse(item, out var n))
                                list.Add(n);
                        }

                        if (list.Count == data.Length) {
                            var f = new Fraction[list.Count];
                            for (var i = 0; i < list.Count; i++)
                                f[i] = new Fraction(list[i], d);

                            MeterValue = new Meter(f);
                        }
                        else
                            MeterValue = new Meter(new Fraction(0, 0));

                    }
                    else
                        MeterValue = new Meter(new Fraction(0, 0));

                }

                else
                    MeterValue = new Meter(new Fraction(value));

            }

        }

        /// <summary>
        ///     meter
        /// </summary>
        public Meter MeterValue { get; }
    }
}
