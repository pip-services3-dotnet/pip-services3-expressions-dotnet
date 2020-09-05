﻿using System;
using System.Text;

using PipServices3.Expressions.IO;
using PipServices3.Expressions.Tokenizers.Utilities;

namespace PipServices3.Expressions.Tokenizers.Csv
{
    /// <summary>
    /// Implements a quote string state object for CSV streams.
    /// </summary>
    internal class CsvQuoteState : IQuoteState
    {
        /// <summary>
        /// Gets the next token from the stream started from the character linked to this state.
        /// </summary>
        /// <param name="reader">A textual string to be tokenized.</param>
        /// <param name="tokenizer">A tokenizer class that controls the process.</param>
        /// <returns>The next token from the top of the stream.</returns>
        public Token NextToken(IPushbackReader reader, ITokenizer tokenizer)
        {
            char firstSymbol = reader.Read();
            StringBuilder tokenValue = new StringBuilder();
            tokenValue.Append(firstSymbol);

            for (char nextSymbol = reader.Read(); !CharValidator.IsEof(nextSymbol); nextSymbol = reader.Read())
            {
                tokenValue.Append(nextSymbol);
                if (nextSymbol == firstSymbol)
                {
                    if (reader.Peek() == firstSymbol)
                    {
                        nextSymbol = reader.Read();
                        tokenValue.Append(nextSymbol);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return new Token(TokenType.Quoted, tokenValue.ToString());
        }

        /// <summary>
        /// Encodes a string value.
        /// </summary>
        /// <param name="value">A string value to be encoded.</param>
        /// <param name="quoteSymbol">A string quote character.</param>
        /// <returns>An encoded string.</returns>
        public string EncodeString(string value, char quoteSymbol)
        {
            if (value != null)
            {
                StringBuilder result = new StringBuilder();
                string quoteString = Char.ToString(quoteSymbol);
                result.Append(quoteSymbol);
                result.Append(value.Replace(quoteString, quoteString + quoteString));
                result.Append(quoteSymbol);
                return result.ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Decodes a string value.
        /// </summary>
        /// <param name="value">A string value to be decoded.</param>
        /// <param name="quoteSymbol">A string quote character.</param>
        /// <returns>An decoded string.</returns>
        public string DecodeString(string value, char quoteSymbol)
        {
            if (value != null)
            {
                if (value.Length >= 2
                    && value[0] == quoteSymbol
                    && value[value.Length - 1] == quoteSymbol)
                {
                    string quoteString = Char.ToString(quoteSymbol);
                    return value.Substring(1, value.Length - 2).Replace(quoteString + quoteString, quoteString);
                }
            }
            return value;
        }
    }
}
