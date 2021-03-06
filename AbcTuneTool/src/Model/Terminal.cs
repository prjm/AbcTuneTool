﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

using AbcTuneTool.Common;
using AbcTuneTool.Model.TuneElements;

namespace AbcTuneTool.Model {

    /// <summary>
    ///     terminal syntax part
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Terminal : ISyntaxTreeElement {

        /// <summary>
        ///     terminal values
        /// </summary>
        private ImmutableArray<Token> tokens;

        /// <summary>
        ///     create a new terminal
        /// </summary>
        /// <param name="tokenValues"></param>
        public Terminal(ImmutableArray<Token> tokenValues)
            => tokens = tokenValues;

        /// <summary>
        ///     check if a character is an whitespace
        /// </summary>
        public bool IsWhitespace {
            get {
                if (tokens.Length < 1)
                    return false;

                for (var i = 0; i < tokens.Length; i++)
                    if (!string.IsNullOrWhiteSpace(tokens[i].Value))
                        return false;


                return true;
            }
        }

        /// <summary>
        ///     test if this terminal matches a string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Matches(string input) {
            var tokenIndex = 0;
            var tokenCharIndex = 0;

            if (tokens.Length < 1)
                return false;

            if (input.Length < 1)
                throw new ArgumentOutOfRangeException();

            ref readonly var token = ref tokens.ItemRef(0);

            for (var charIndex = 0; charIndex < input.Length && tokenIndex < tokens.Length; charIndex++, tokenCharIndex++) {

                if (token.OriginalValue.Length <= tokenCharIndex) {
                    tokenCharIndex = 0;
                    tokenIndex++;

                    if (tokenIndex >= tokens.Length)
                        return false;

                    token = ref tokens.ItemRef(tokenIndex);
                }

                if (input[charIndex] != token.OriginalValue[tokenCharIndex])
                    return false;
            }

            return true;
        }

        /// <summary>
        ///     get the value after a whitespace token
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetValueAfterWhitespace(int index)
            => GetValueAfterWhitespace(index, out _);

        /// <summary>
        ///     get the value after a semicolon token
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetValueAfterSemicolon(int index)
            => GetValueAfterSemicolon(index, out _);



        /// <summary>
        ///     get a token value after a whitespace token
        /// </summary>
        /// <param name="index"></param>
        /// <param name="offset">selected token offset</param>
        /// <returns></returns>
        public string GetValueAfterWhitespace(int index, out int offset) {
            if (tokens.Length <= index || index < 0) {
                offset = -1;
                return string.Empty;
            }

            for (; index < tokens.Length; index++) {
                ref readonly var token = ref tokens.ItemRef(index);

                if (token.Value.Length < 1)
                    continue;

                var c = token.Value[0];
                if (!c.IsWhitespace()) {
                    offset = index;
                    return token.Value;
                }
            }

            offset = -1;
            return string.Empty;
        }

        /// <summary>
        ///     get a token value after a whitespace token
        /// </summary>
        /// <param name="index"></param>
        /// <param name="offset">selected token offset</param>
        /// <returns></returns>
        public string GetValueAfterSemicolon(int index, out int offset) {
            if (tokens.Length <= index || index < 0) {
                offset = -1;
                return string.Empty;
            }

            for (; index < tokens.Length; index++) {
                ref readonly var token = ref tokens.ItemRef(index);

                if (token.Value.Length < 1)
                    continue;

                var c = token.Value[0];
                if (c != ';') {
                    offset = index;
                    return token.Value;
                }
            }

            offset = -1;
            return string.Empty;
        }

        /// <summary>
        ///     display this terminal in the debugger
        /// </summary>
        /// <returns></returns>
        public string DebuggerDisplay
            => string.Concat(tokens.Select(t => t.DebuggerDisplay + ", "));

        /// <summary>
        ///     add the content of this terminal to a string builder
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="cache">string cache</param>
        /// <param name="pool">string builder pool</param>
        public string ToString(int startPos, StringBuilderPool pool, StringCache cache) {
            var tokenCharIndex = 0;
            using var target = pool.Rent();

            if (tokens.Length < 1)
                return string.Empty;

            for (var tokenIndex = 0; tokenIndex < tokens.Length; tokenIndex++) {
                ref readonly var token = ref tokens.ItemRef(tokenIndex);

                for (var charIndex = 0; charIndex < token.Value.Length; charIndex++) {

                    if (tokenCharIndex >= startPos) {
                        if (charIndex == 0 && tokenIndex == tokens.Length - 1)
                            return token.Value;

                        target.Item.Append(token.Value[charIndex]);
                    }

                    tokenCharIndex++;
                }
            }

            return cache.ForStringBuilder(target);
        }

        /// <summary>
        ///     create a new terminal
        /// </summary>
        /// <param name="token"></param>
        public Terminal(Token token)
            => tokens = ImmutableArray.Create(token);

        /// <summary>
        ///     create a new terminal
        /// </summary>
        /// <param name="values"></param>
        public Terminal(ObjectPoolItem<List<Token>> values)
            : this(values.ToImmutableArray()) { }

        /// <summary>
        ///     get the value of the terminal
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string this[int index]
            => tokens[index].Value;

        /// <summary>
        ///     get the first char of this terminal
        /// </summary>
        public char FirstChar {
            get {
                if (tokens.Length < 1 || tokens[0].Value.Length < 1)
                    return '\0';
                return tokens[0].Value[0];
            }
        }

        /// <summary>
        ///     get the second char
        /// </summary>
        public char SecondChar {
            get {
                if (tokens.Length < 1)
                    return '\0';

                if (tokens[0].Value.Length >= 2)
                    return tokens[0].Value[1];

                if (tokens.Length < 2)
                    return '\0';

                if (tokens[1].Value.Length >= 1)
                    return tokens[1].Value[0];
                return '\0';
            }
        }

        /// <summary>
        ///     <c>true</c> if there a no tokens in this terminal
        /// </summary>
        public bool IsEmpty
            => tokens.Length < 1;

        /// <summary>
        ///     terminal sequence length
        /// </summary>
        public int Length
            => tokens.Length;

        /// <summary>
        ///     create a new string from the source tokens
        /// </summary>
        /// <returns></returns>
        public string ToNewString()
            => string.Concat(tokens);

        /// <summary>
        ///     accept an visitor
        /// </summary>
        /// <param name="visitor"></param>
        public bool Accept(ISyntaxTreeVisitor visitor)
            => visitor.StartVisitNode(this) &&
               visitor.EndVisitNode(this);
    }
}