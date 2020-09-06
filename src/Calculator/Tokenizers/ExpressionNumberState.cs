using System;
using System.Text;

using PipServices3.Expressions.IO;
using PipServices3.Expressions.Tokenizers;
using PipServices3.Expressions.Tokenizers.Generic;
using PipServices3.Expressions.Tokenizers.Utilities;

namespace PipServices3.Expressions.Calculator.Tokenizers
{
    /// <summary>
    /// Implements an Expression-specific number state object.
    /// </summary>
    internal class ExpressionNumberState : GenericNumberState
    {
        /// <summary>
        /// Gets the next token from the stream started from the character linked to this state.
        /// </summary>
        /// <param name="reader">A textual string to be tokenized.</param>
        /// <param name="tokenizer">A tokenizer class that controls the process.</param>
        /// <returns>The next token from the top of the stream.</returns>
        public override Token NextToken(IPushbackReader reader, ITokenizer tokenizer)
        {
            // Process leading minus.
            if (reader.Peek() == '-')
            {
                return tokenizer.SymbolState.NextToken(reader, tokenizer);
            }

            // Process numbers using base class algorithm.
            Token token = base.NextToken(reader, tokenizer);

            // Exit if number was not detected.
            if (token.Type != TokenType.Integer && token.Type != TokenType.Float)
            {
                return token;
            }

            // Exit if number is not in scientific format.
            char nextChar = reader.Peek();
            if (nextChar != 'e' && nextChar != 'E')
            {
                return token;
            }

            StringBuilder tokenValue = new StringBuilder();
            tokenValue.Append(reader.Read());

            // Process '-' or '+' in mantissa
            nextChar = reader.Peek();
            if (nextChar == '-' || nextChar == '+')
            {
                tokenValue.Append(reader.Read());
                nextChar = reader.Peek();
            }

            // Exit if mantissa has no digits.
            if (!CharValidator.IsDigit(nextChar))
            {
                reader.PushbackString(tokenValue.ToString());
                return token;
            }

            // Process matissa digits
            for (; CharValidator.IsDigit(nextChar); nextChar = reader.Peek())
            {
                tokenValue.Append(reader.Read());
            }

            return new Token(TokenType.Float, token.Value + tokenValue.ToString());
        }
    }
}
