using System;
using System.Collections.Generic;

namespace AbcTuneTool.FileIo {

    internal class MnemonicLookup {
        public Dictionary<(char, char), string> lookup = new Dictionary<(char, char), string>(79);
        public Dictionary<string, (char, char)> inverseLookup = new Dictionary<string, (char, char)>(79);
    }

    /// <summary>
    ///     ABC mnemonics
    /// </summary>
    public static class Mnemonics {

        private static MnemonicLookup GetValues() {
            var result = new MnemonicLookup();
            Add(result, "À", '`', 'A');
            Add(result, "à", '`', 'a');
            Add(result, "È", '`', 'E');
            Add(result, "è", '`', 'e');
            Add(result, "Ì", '`', 'I');
            Add(result, "ì", '`', 'i');
            Add(result, "Ò", '`', 'O');
            Add(result, "ò", '`', 'o');
            Add(result, "Ù", '`', 'U');
            Add(result, "ù", '`', 'u');
            Add(result, "Á", '\'', 'A');
            Add(result, "á", '\'', 'a');
            Add(result, "É", '\'', 'E');
            Add(result, "é", '\'', 'e');
            Add(result, "Í", '\'', 'I');
            Add(result, "í", '\'', 'i');
            Add(result, "Ó", '\'', 'O');
            Add(result, "ó", '\'', 'o');
            Add(result, "Ú", '\'', 'U');
            Add(result, "ú", '\'', 'u');
            Add(result, "Ý", '\'', 'Y');
            Add(result, "ý", '\'', 'y');
            Add(result, "Â", '^', 'A');
            Add(result, "â", '^', 'a');
            Add(result, "Ê", '^', 'E');
            Add(result, "ê", '^', 'e');
            Add(result, "Î", '^', 'I');
            Add(result, "î", '^', 'i');
            Add(result, "Ô", '^', 'O');
            Add(result, "ô", '^', 'o');
            Add(result, "Û", '^', 'U');
            Add(result, "û", '^', 'u');
            Add(result, "Ŷ", '^', 'Y');
            Add(result, "ŷ", '^', 'y');
            Add(result, "Ã", '~', 'A');
            Add(result, "ã", '~', 'a');
            Add(result, "Ñ", '~', 'N');
            Add(result, "ñ", '~', 'n');
            Add(result, "Õ", '~', 'O');
            Add(result, "õ", '~', 'o');
            Add(result, "Ä", '"', 'A');
            Add(result, "ä", '"', 'a');
            Add(result, "Ë", '"', 'E');
            Add(result, "ë", '"', 'e');
            Add(result, "Ï", '"', 'I');
            Add(result, "ï", '"', 'i');
            Add(result, "Ö", '"', 'O');
            Add(result, "ö", '"', 'o');
            Add(result, "Ü", '"', 'U');
            Add(result, "ü", '"', 'u');
            Add(result, "Ÿ", '"', 'Y');
            Add(result, "ÿ", '"', 'y');
            Add(result, "Ç", 'c', 'C');
            Add(result, "ç", 'c', 'c');
            Add(result, "Å", 'A', 'A');
            Add(result, "å", 'a', 'a');
            Add(result, "Ø", '/', 'O');
            Add(result, "ø", '/', 'o');
            Add(result, "Ă", 'u', 'A');
            Add(result, "ă", 'u', 'a');
            Add(result, "Ĕ", 'u', 'E');
            Add(result, "ĕ", 'u', 'e');
            Add(result, "Š", 'v', 'S');
            Add(result, "š", 'v', 's');
            Add(result, "Ž", 'v', 'Z');
            Add(result, "ž", 'v', 'z');
            Add(result, "Ő", 'H', 'O');
            Add(result, "ő", 'H', 'o');
            Add(result, "Ű", 'H', 'U');
            Add(result, "ű", 'H', 'u');
            Add(result, "Æ", 'A', 'E');
            Add(result, "æ", 'a', 'e');
            Add(result, "Œ", 'O', 'E');
            Add(result, "œ", 'o', 'e');
            Add(result, "ß", 's', 's');
            Add(result, "Ð", 'D', 'H');
            Add(result, "ð", 'd', 'h');
            Add(result, "Þ", 'T', 'H');
            Add(result, "þ", 't', 'h');
            return result;
        }

        private static void Add(MnemonicLookup result, string v1, char v2, char v3) {
            result.lookup.Add((v2, v3), v1);
            result.inverseLookup.Add(v1, (v2, v3));
        }

        private static readonly Lazy<MnemonicLookup> values
            = new Lazy<MnemonicLookup>(GetValues, System.Threading.LazyThreadSafetyMode.PublicationOnly);

        internal static string Decode(char decorator, char decoratedElement) {
            if (!values.Value.lookup.TryGetValue((decorator, decoratedElement), out var result)) {
                result = string.Empty;
            }
            return result;
        }

        internal static (char decorator, char decoratedELement) Encode(string v) {
            if (!values.Value.inverseLookup.TryGetValue(v, out var result)) {
                result = ('\0', '\0');
            }
            return result;
        }
    }
}
