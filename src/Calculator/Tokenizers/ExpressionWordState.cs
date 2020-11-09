﻿using System;
using PipServices3.Expressions.IO;
using PipServices3.Expressions.Tokenizers;
using PipServices3.Expressions.Tokenizers.Generic;

namespace PipServices3.Expressions.Calculator.Tokenizers
{
    /// <summary>
    /// Implements a word state object.
    /// </summary>
    internal class ExpressionWordState : GenericWordState
    {
        /// <summary>
        /// Supported expression keywords.
        /// </summary>
        public string[] Keywords = new string[]
        {
            "AND", "OR", "NOT", "XOR", "LIKE", "IS", "IN", "NULL", "TRUE", "FALSE"
        };

        /// <summary>
        /// Constructs an instance of this class.
        /// </summary>
        public ExpressionWordState()
        {
            ClearWordChars();
            SetWordChars('a', 'z', true);
            SetWordChars('A', 'Z', true);
            SetWordChars('0', '9', true);
            SetWordChars('_', '_', true);
            SetWordChars('.', '.', true);
            SetWordChars('\x00c0', '\x00ff', true);
            SetWordChars('\x0100', '\xffff', true);
        }

        /// <summary>
        /// Gets the next token from the stream started from the character linked to this state.
        /// </summary>
        /// <param name="reader">A textual string to be tokenized.</param>
        /// <param name="tokenizer">A tokenizer class that controls the process.</param>
        /// <returns>The next token from the top of the stream.</returns>
        public override Token NextToken(IPushbackReader reader, ITokenizer tokenizer)
        {
            Token token = base.NextToken(reader, tokenizer);

            foreach (string keyword in Keywords)
            {
                if (keyword.Equals(token.Value, StringComparison.OrdinalIgnoreCase))
                {
                    return new Token(TokenType.Keyword, token.Value);
                }
            }
            return token;
        }
    }
}
