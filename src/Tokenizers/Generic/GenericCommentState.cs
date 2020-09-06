using System;
using System.Text;

using PipServices3.Expressions.IO;
using PipServices3.Expressions.Tokenizers.Utilities;

namespace PipServices3.Expressions.Tokenizers.Generic
{
    /// <summary>
    /// A CommentState object returns a comment from a reader.
    /// </summary>
    public class GenericCommentState : ICommentState
    {
        /// <summary>
        ///  Either delegate to a comment-handling state, or return a token with just a slash in it.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="tokenizer"></param>
        /// <returns>Either just a slash token, or the results of delegating to a comment-handling state</returns>
        public virtual Token NextToken(IPushbackReader reader, ITokenizer tokenizer)
        {
            StringBuilder tokenValue = new StringBuilder();
            char nextSymbol;
            for (nextSymbol = reader.Read(); !CharValidator.IsEof(nextSymbol)
                && nextSymbol != '\n' && nextSymbol != '\r'; nextSymbol = reader.Read())
            {
                tokenValue.Append(nextSymbol);
            }
            if (!CharValidator.IsEof(nextSymbol))
            {
                reader.Pushback(nextSymbol);
            }

            return new Token(TokenType.Comment, tokenValue.ToString());
        }
    }
}
