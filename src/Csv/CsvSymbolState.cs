using System;

using PipServices3.Expressions.IO;
using PipServices3.Expressions.Tokenizers;
using PipServices3.Expressions.Tokenizers.Generic;

namespace PipServices3.Expressions.Csv
{
    /// <summary>
    /// Implements a symbol state to tokenize delimiters in CSV streams.
    /// </summary>
    public class CsvSymbolState : GenericSymbolState
    {
        /// <summary>
        /// Constructs this object with specified parameters.
        /// </summary>
        public CsvSymbolState()
        {
            Add("\n", TokenType.Eol);
            Add("\r", TokenType.Eol);
            Add("\r\n", TokenType.Eol);
            Add("\n\r", TokenType.Eol);
        }

        public override Token NextToken(IScanner scanner, ITokenizer tokenizer)
        {
            // Optimization...
            char nextSymbol = scanner.Read();
            if (nextSymbol != '\n' && nextSymbol != '\r')
            {
                return new Token(TokenType.Symbol, nextSymbol.ToString());
            }
            else
            {
                scanner.Unread();
                return base.NextToken(scanner, tokenizer);
            }
        }

    }
}
