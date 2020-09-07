using System.Collections.Generic;
using AbcTuneTool.Common;

namespace AbcTuneTool.Model {

    /// <summary>
    ///     a table of keys
    /// </summary>
    public class KeyTable {

        /// <summary>
        ///     create a new key table
        /// </summary>
        /// <param name="standardToneSystem"></param>
        protected KeyTable(StandardToneSystem standardToneSystem)
            => Tones = standardToneSystem;

        private readonly Dictionary<int, Tone> keys = new Dictionary<int, Tone>();

        /// <summary>
        ///     add a key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="accidental"></param>
        protected void AddKey(int key, char name, char accidental)
            => keys[key] = new Tone(name, accidental.AsAccidental());

        /// <summary>
        ///     used tone system
        /// </summary>
        public StandardToneSystem Tones { get; protected set; }

        /// <summary>
        ///     find a key in this key table
        /// </summary>
        /// <param name="name"></param>
        /// <param name="accidental"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool TryFindKey(char name, Accidental accidental, out int key) {
            foreach (var entry in keys)
                if (entry.Value.Name == name && entry.Value.Accidental == accidental) {
                    key = entry.Key;
                    return true;
                }


            key = 0;
            return false;
        }

        /// <summary>
        ///     define a key
        /// </summary>
        /// <param name="tone"></param>
        /// <param name="accidental"></param>
        /// <returns></returns>
        public bool DefineKey(char tone, Accidental accidental) {
            if (TryFindKey(tone, accidental, out var key)) {
                Tones = Tones.DefineKey(key, accidental != Accidental.Undefined);
                return true;
            }

            return false;
        }


    }

}
