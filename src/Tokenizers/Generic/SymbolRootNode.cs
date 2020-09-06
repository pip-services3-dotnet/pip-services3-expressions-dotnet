using System;

using PipServices3.Expressions.IO;

namespace PipServices3.Expressions.Tokenizers.Generic
{
    /// <summary>
    /// This class is a special case of a <code>SymbolNode</code>. A <code>SymbolRootNode</code>
    /// object has no symbol of its own, but has children that represent all possible symbols.
    /// </summary>
    public class SymbolRootNode : SymbolNode
    {
        /// <summary>
        /// Creates and initializes a root node.
        /// </summary>
        public SymbolRootNode()
            : base(null, '\0')
        {
        }

        /// <summary>
        /// Add the given string as a symbol.
        /// </summary>
        /// <param name="value">The character sequence to add.</param>
        public void Add(string value, TokenType tokenType)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Value must have at least 1 character");
            }
            SymbolNode childNode = EnsureChildWithChar(value[0]);
            if (childNode.TokenType == TokenType.Unknown)
            {
                childNode.Valid = true;
                childNode.TokenType = TokenType.Symbol;
            }
            childNode.AddDescendantLine(value.Substring(1), tokenType);
        }

        /// <summary>
        /// Return a symbol string from a reader.
        /// </summary>
        /// <param name="reader">A reader to read from</param>
        /// <param name="firstChar">The first character of this symbol, already read from the reader.</param>
        /// <returns>A symbol string from a reader</returns>
        public Token NextToken(IPushbackReader reader)
        {
            char nextSymbol = reader.Read();
            SymbolNode childNode = FindChildWithChar(nextSymbol);
            if (childNode != null)
            {
                childNode = childNode.DeepestRead(reader);
                childNode = childNode.UnreadToValid(reader);
                return new Token(childNode.TokenType, childNode.Ancestry());
            }
            else
            {
                return new Token(TokenType.Symbol, nextSymbol.ToString());
            }
        }
    }
}
