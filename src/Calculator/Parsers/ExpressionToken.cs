using PipServices3.Expressions.Variants;

namespace PipServices3.Expressions.Calculator.Parsers
{
    /// <summary>
    /// Defines an expression token holder.
    /// </summary>
    public class ExpressionToken
    {
        private ExpressionTokenType _type;
        private Variant _value;

        /// <summary>
        /// Creates an instance of this token and initializes it with specified values.
        /// </summary>
        /// <param name="type">The type of this token.</param>
        /// <param name="value">The value of this token.</param>
        public ExpressionToken(ExpressionTokenType type, Variant value)
        {
            _type = type;
            _value = value;
        }

        /// <summary>
        /// Creates an instance of this class with specified type and Null value.
        /// </summary>
        /// <param name="type">The type of this token.</param>
        public ExpressionToken(ExpressionTokenType type)
        {
            _type = type;
            _value = Variant.Empty;
        }

        /// <summary>
        /// The type of this token.
        /// </summary>
        public ExpressionTokenType Type
        {
            get { return _type; }
        }

        /// <summary>
        /// The value of this token.
        /// </summary>
        public Variant Value
        {
            get { return _value; }
        }
    }
}
