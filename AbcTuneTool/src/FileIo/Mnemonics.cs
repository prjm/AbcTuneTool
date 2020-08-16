using System;
using System.Collections.Generic;

namespace AbcTuneTool.FileIo
{

    internal struct Mnenomic
    {
        //public Mnenomic(char decorator, char decoratedElement, char value)
    }

    internal class MnemonicLookup
    {
        public Dictionary<int, char> lookup = new Dictionary<int, char>();
        public Dictionary<char, (char, char)> inverseLookup = new Dictionary<char, (char, char)>();
    }

    /// <summary>
    ///     ABC mnemonics
    /// </summary>
    public static class Mnemonics
    {

        private static MnemonicLookup GetValues()
        {
            var result = new MnemonicLookup();
            Add(result, 'Æ', 'A', 'E');
            return result;
        }

        private static void Add(MnemonicLookup result, char v1, char v2, char v3)
        {
            var key = GetHashCode(v2, v3);
            result.lookup.Add(key, v1);
            result.inverseLookup.Add(v1, (v2, v3));
        }

        private static Lazy<MnemonicLookup> values
            = new Lazy<MnemonicLookup>(GetValues, System.Threading.LazyThreadSafetyMode.PublicationOnly);


        /// <summary>
        ///     compute a search hash code
        /// </summary>
        /// <param name="decorator"></param>
        /// <param name="decoratedElement"></param>
        /// <returns></returns>
        internal static int GetHashCode(char decorator, char decoratedElement)
            => HashCode.Combine(decoratedElement, decoratedElement);

        internal static char Decode(char decorator, char decoratedElement)
        {
            var code = GetHashCode(decorator, decoratedElement);
            if (!values.Value.lookup.TryGetValue(code, out var result))
            {
                result = '\x0000';
            }
            return result;
        }

        internal static (char decorator, char decoratedELement) Encode(char v)
        {
            if (!values.Value.inverseLookup.TryGetValue(v, out var result))
            {
                result = ('\x0000', '\x0000');
            }
            return result;
        }
    }
}
