using System;

using PipServices3.Expressions.Tokenizers;
using PipServices3.Expressions.Tokenizers.Generic;

namespace PipServices3.Expressions.Calculator.Tokenizers
{
    /// <summary>
    /// Implements a symbol state object.
    /// </summary>
    internal class ExpressionSymbolState : GenericSymbolState
    {
        /// <summary>
        /// Constructs an instance of this class.
        /// </summary>
        public ExpressionSymbolState()
        {
            Add("<=", TokenType.Symbol);
            Add(">=", TokenType.Symbol);
            Add("<>", TokenType.Symbol);
            Add("!=", TokenType.Symbol);
            Add(">>", TokenType.Symbol);
            Add("<<", TokenType.Symbol);
        }
    }
}
