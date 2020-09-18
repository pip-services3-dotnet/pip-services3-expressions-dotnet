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
        /// <param name="reader"></param>
        /// <param name="tokenizer"></param>
        /// <returns>Either just a slash token, or the results of delegating to a comment-handling state.</returns>
        public override Token NextToken(IPushbackReader reader, ITokenizer tokenizer)
        {
            char firstSymbol = reader.Read();
            if (firstSymbol != '/')
            {
                reader.Pushback(firstSymbol);
                throw new IncorrectStateException(null, "INCORRECT_USAGE", "Incorrect usage of CppCommentState.");
            }

            char secondSymbol = reader.Read();
            if (secondSymbol == '*')
            {
                return new Token(TokenType.Comment, "/*" + GetMultiLineComment(reader));
            }
            else
            {
                if (!CharValidator.IsEof(secondSymbol))
                {
                    reader.Pushback(secondSymbol);
                }
                if (!CharValidator.IsEof(firstSymbol))
                {
                    reader.Pushback(firstSymbol);
                }
                return tokenizer.SymbolState.NextToken(reader, tokenizer);
            }
        }
    }
}
