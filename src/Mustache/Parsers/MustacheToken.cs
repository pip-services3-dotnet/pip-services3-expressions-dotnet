using System;
using System.Collections.Generic;
using System.Text;

namespace PipServices3.Expressions.Mustache.Parsers
{
    /// <summary>
    /// Defines a mustache token holder.
    /// </summary>
    public class MustacheToken
    {
        private MustacheTokenType _type;
        private string _value;
        private IList<MustacheToken> _tokens = new List<MustacheToken>();

        /// <summary>
        /// Creates an instance of a mustache token.
        /// </summary>
        /// <param name="type">a token type.</param>
        /// <param name="value">a token value.</param>
        public MustacheToken(MustacheTokenType type, string value)
        {
            _type = type;
            _value = value;
        }

        /// <summary>
        /// Gets the token type.
        /// </summary>
        public MustacheTokenType Type
        {
            get { return _type; }
        }

        /// <summary>
        /// Gets the token value or variable name.
        /// </summary>
        public string Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Gets a list of subtokens is this token a section.
        /// </summary>
        public IList<MustacheToken> Tokens
        {
            get { return _tokens; }
        }
    }
}
