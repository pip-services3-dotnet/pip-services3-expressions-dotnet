using System;
using PipServices3.Commons.Errors;

namespace PipServices3.Expressions.Calculator
{
    /// <summary>
    /// Exception that can be thrown by Expression Calculator.
    /// </summary>
#if CORE_NET
    [DataContract]
#else
    [Serializable]
#endif
    public class ExpressionException : BadRequestException
    {
        public ExpressionException(string correlationId = null, string code = null,
            string message = null, Exception innerException = null)
            : base(correlationId, code, message, innerException) { }
    }
}
