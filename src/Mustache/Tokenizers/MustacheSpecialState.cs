using System;
using System.Text;

using PipServices3.Expressions.IO;
using PipServices3.Expressions.Tokenizers;
using PipServices3.Expressions.Tokenizers.Utilities;

namespace PipServices3.Mustache.Tokenizers
{

    /// <summary>
    /// Implements a quote string state object for Mustache templates.
    /// </summary>
    public class MustacheSpecialState : ITokenizerState
    {
        private const char Bracket = '{';

        /// <summary>
        /// Implements a quote string state object for Mustache templates.
        /// </summary>
        /// <param name="scanner">A textual string to be tokenized.</param>
        /// <param name="tokenizer">A tokenizer class that controls the process.</param>
        /// <returns>The next token from the top of the stream.</returns>
        public Token NextToken(IScanner scanner, ITokenizer tokenizer)
        {
            StringBuilder tokenValue = new StringBuilder();

            for (char nextSymbol = scanner.Read(); !CharValidator.IsEof(nextSymbol); nextSymbol = scanner.Read())
            {
                if (nextSymbol == MustacheSpecialState.Bracket)
                {
                    if (scanner.Peek() == MustacheSpecialState.Bracket)
                    {
                        scanner.Unread();
                        break;
                    }
                }

                tokenValue.Append(nextSymbol);
            }

            return new Token(TokenType.Special, tokenValue.ToString());
        }
    }
}
