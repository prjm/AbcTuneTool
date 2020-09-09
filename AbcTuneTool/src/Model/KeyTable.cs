using System;
using System.Collections.Generic;
using AbcTuneTool.Common;

namespace AbcTuneTool.Model {

    /// <summary>
    ///     key definition
    /// </summary>
    public class KeyDefinition {

        /// <summary>
        ///     create a new key definition
        /// </summary>
        /// <param name="level"></param>
        /// <param name="prevKey"></param>
        /// <param name="toneName"></param>
        /// <param name="accidentals"></param>
        public KeyDefinition(int level, KeyDefinition? prevKey, Tone toneName, Tone[] accidentals) {
            AllowedAccidental = accidentals;
            Name = toneName;
            Level = level;
            PrevKey = prevKey;
        }

        /// <summary>
        ///     key name
        /// </summary>
        public Tone Name { get; }

        /// <summary>
        ///     allowed accidentals
        /// </summary>
        public Tone[] AllowedAccidental { get; }

        /// <summary>
        ///     key level
        /// </summary>
        public int Level { get; }

        /// <summary>
        ///     previous key
        /// </summary>
        public KeyDefinition? PrevKey { get; }

        /// <summary>
        ///     accidental
        /// </summary>
        public Accidental Accidental
            => (Level > 0, Level < 0) switch
            {
                (true, _) => Accidental.Sharp,
                (_, true) => Accidental.Flat,
                _ => Accidental.Undefined,
            };
    }

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

        private readonly Dictionary<int, KeyDefinition> keys = new Dictionary<int, KeyDefinition>();

        private KeyDefinition? lastKey = default;

        /// <summary>
        ///     add a key
        /// </summary>
        /// <param name="name"></param>
        /// <param name="accidental"></param>
        /// <param name="allowedAccidentals"></param>
        /// <param name="key"></param>
        protected KeyDefinition AddKey(int key, char name, char accidental, params Tone[] allowedAccidentals) {
            if (key == 0)
                lastKey = default;

            if (lastKey != default && Math.Abs(lastKey.Level) > Math.Abs(key))
                throw new InvalidOperationException();

            var keyDefinition = new KeyDefinition(key, lastKey, new Tone(name, accidental.AsAccidental()), allowedAccidentals);
            keys[key] = keyDefinition;
            lastKey = keyDefinition;
            return keyDefinition;
        }

        /// <summary>
        ///     used tone system
        /// </summary>
        public StandardToneSystem Tones { get; protected set; }

        /// <summary>
        ///     find a key in this key table
        /// </summary>
        /// <param name="name"></param>
        /// <param name="accidental"></param>
        /// <param name="def">key definition</param>
        /// <returns></returns>
        public bool TryFindKey(char name, Accidental accidental, out KeyDefinition? def) {
            foreach (var entry in keys)
                if (entry.Value.Name.Name == name && entry.Value.Name.Accidental == accidental) {
                    def = entry.Value;
                    return true;
                }


            def = default;
            return false;
        }

        /// <summary>
        ///     define a key
        /// </summary>
        /// <param name="tone"></param>
        /// <param name="accidental"></param>
        /// <returns></returns>
        public bool DefineKey(char tone, Accidental accidental) {
            if (TryFindKey(tone, accidental, out var def)) {
                var keys = new Stack<KeyDefinition>();
                while (def != default) {
                    keys.Push(def);
                    def = def.PrevKey;
                }

                var allowedAccs = new List<Tone>();

                while (keys.Count > 0) {
                    var key = keys.Pop();

                    allowedAccs.AddRange(key.AllowedAccidental);
                    Tones = Tones.DefineKey(key.Accidental, allowedAccs);
                }

                return true;
            }

            return false;
        }


    }

}
