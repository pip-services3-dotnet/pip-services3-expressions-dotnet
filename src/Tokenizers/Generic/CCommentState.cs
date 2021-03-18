using System;

using PipServices3.Expressions.IO;
using PipServices3.Expressions.Tokenizers.Utilities;

namespace PipServices3.Expressions.Tokenizers.Generic
{
    /// <summary>
    /// This state will either delegate to a comment-handling state, or return a token with just a slash in it.
    /// </summary>
    public class CCommentState : CppCommentState
    {
        /// <summary>
        /// Either delegate to a comment-handling state, or return a token with just a slash in it.
        /// </summary>
        /// <param name="scanner"></param>
        /// <param name="tokenizer"></param>
        /// <returns>Either just a slash token, or the results of delegating to a comment-handling state.</returns>
        public override Token NextToken(IScanner scanner, ITokenizer tokenizer)
        {
            char firstSymbol = scanner.Read();
            if (firstSymbol != '/')
            {
                scanner.Unread();
                throw new IncorrectStateException(null, "INCORRECT_USAGE", "Incorrect usage of CppCommentState.");
            }

            char secondSymbol = scanner.Read();
            if (secondSymbol == '*')
            {
                return new Token(TokenType.Comment, "/*" + GetMultiLineComment(scanner));
            }
            else
            {
                if (!CharValidator.IsEof(secondSymbol))
                {
                    scanner.Unread();
                }
                if (!CharValidator.IsEof(firstSymbol))
                {
                    scanner.Unread();
                }
                return tokenizer.SymbolState.NextToken(scanner, tokenizer);
            }
        }
    }
}
