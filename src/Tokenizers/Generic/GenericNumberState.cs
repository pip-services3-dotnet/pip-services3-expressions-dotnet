using System;
using System.Text;

using PipServices3.Expressions.IO;
using PipServices3.Expressions.Tokenizers.Utilities;

namespace PipServices3.Expressions.Tokenizers.Generic
{
    /// <summary>
    /// A NumberState object returns a number from a reader. This state's idea of a number allows
    /// an optional, initial minus sign, followed by one or more digits. A decimal point and another string
    /// of digits may follow these digits.
    /// </summary>
    public class GenericNumberState : INumberState
    {
        /// <summary>
        /// Gets the next token from the stream started from the character linked to this state.
        /// </summary>
        /// <param name="reader">A textual string to be tokenized.</param>
        /// <param name="tokenizer">A tokenizer class that controls the process.</param>
        /// <returns>The next token from the top of the stream.</returns>
        public virtual Token NextToken(IPushbackReader reader, ITokenizer tokenizer)
        {
            bool absorbedDot = false;
            bool gotADigit = false;
            StringBuilder tokenValue = new StringBuilder("");
            char nextSymbol = reader.Read();

            // Parses leading minus.
            if (nextSymbol == '-')
            {
                tokenValue.Append('-');
                nextSymbol = reader.Read();
            }

            // Parses digits before decimal separator.
            for (; CharValidator.IsDigit(nextSymbol)
                && !CharValidator.IsEof(nextSymbol); nextSymbol = reader.Read())
            {
                gotADigit = true;
                tokenValue.Append(nextSymbol);
            }

            // Parses part after the decimal separator.
            if (nextSymbol == '.')
            {
                absorbedDot = true;
                tokenValue.Append('.');
                nextSymbol = reader.Read();

                // Absorb all digits.
                for (; CharValidator.IsDigit(nextSymbol)
                    && !CharValidator.IsEof(nextSymbol); nextSymbol = reader.Read())
                {
                    gotADigit = true;
                    tokenValue.Append(nextSymbol);
                }
            }

            // Pushback last unprocessed symbol.
            if (!CharValidator.IsEof(nextSymbol))
            {
                reader.Pushback(nextSymbol);
            }

            // Process the result.
            if (!gotADigit)
            {
                reader.PushbackString(tokenValue.ToString());
                if (tokenizer != null && tokenizer.SymbolState != null)
                {
                    return tokenizer.SymbolState.NextToken(reader, tokenizer);
                }
                else
                {
                    throw new IncorrectStateException("Tokenizer must have an assigned symbol state.");
                }
            }

            return new Token(absorbedDot ? TokenType.Float : TokenType.Integer, tokenValue.ToString());
        }
    }
}
