using System;

using PipServices3.Commons.Errors;

namespace PipServices3.Expressions.Tokenizers
{
    /// <summary>
    /// Exception that can be thrown by Tokenizer.
    /// </summary>
#if CORE_NET
    [DataContract]
#else
    [Serializable]
#endif
    public class TokenizerException : BadRequestException
    {
        public TokenizerException(string correlationId = null, string code = null,
            string message = null, Exception innerException = null)
            : base(correlationId, code, message, innerException) { }
    }

}

