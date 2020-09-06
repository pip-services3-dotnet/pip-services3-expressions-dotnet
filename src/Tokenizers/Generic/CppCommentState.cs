using System;
using System.Text;

using PipServices3.Expressions.IO;
using PipServices3.Expressions.Tokenizers.Utilities;

namespace PipServices3.Expressions.Tokenizers.Generic
{
    /// <summary>
    /// This state will either delegate to a comment-handling state, or return a token with just a slash in it.
    /// </summary>
    public class CppCommentState : GenericCommentState
    {
        /// <summary>
        /// Ignore everything up to a closing star and slash, and then return the tokenizer's next token.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected static string GetMultiLineComment(IPushbackReader reader)
        {
            StringBuilder result = new StringBuilder();
            char lastSymbol = '\0';
            for (char nextSymbol = reader.Read(); !CharValidator.IsEof(nextSymbol); nextSymbol = reader.Read())
            {
                result.Append(nextSymbol);
                if (lastSymbol == '*' && nextSymbol == '/')
                {
                    break;
                }
                lastSymbol = nextSymbol;
            }
            return result.ToString();
        }

        /// <summary>
        /// Ignore everything up to an end-of-line and return the tokenizer's next token.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected static string GetSingleLineComment(IPushbackReader reader)
        {
            StringBuilder result = new StringBuilder();
            char nextSymbol;
            for (nextSymbol = reader.Read();
                !CharValidator.IsEof(nextSymbol) && !CharValidator.IsEol(nextSymbol);
                nextSymbol = reader.Read())
            {
                result.Append(nextSymbol);
            }
            if (CharValidator.IsEol(nextSymbol))
            {
                reader.Pushback(nextSymbol);
            }
            return result.ToString();
        }

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
                throw new InvalidProgramException("Incorrect usage of CppCommentState.");
            }

            char secondSymbol = reader.Read();
            if (secondSymbol == '*')
            {
                return new Token(TokenType.Comment, "/*" + GetMultiLineComment(reader));
            }
            else if (secondSymbol == '/')
            {
                return new Token(TokenType.Comment, "//" + GetSingleLineComment(reader));
            }
            else
            {
                reader.Pushback(secondSymbol);
                reader.Pushback(firstSymbol);
                return tokenizer.SymbolState.NextToken(reader, tokenizer);
            }
        }
    }
}
