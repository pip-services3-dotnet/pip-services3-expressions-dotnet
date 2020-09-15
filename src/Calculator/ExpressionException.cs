using System;
using System.Runtime.Serialization;
using PipServices3.Commons.Errors;

namespace PipServices3.Expressions.Calculator
{
    /// <summary>
    /// Exception that can be thrown by Expression Calculator.
    /// </summary>
    [DataContract]
    public class ExpressionException : BadRequestException
    {
        public ExpressionException(string correlationId = null, string code = null,
            string message = null, Exception innerException = null)
            : base(correlationId, code, message, innerException) { }
    }
}
