using System;

namespace PipServices3.Expressions.Tokenizers
{
    /// <summary>
    /// Exception thrown when TokenizerState was called in incorrect state or for unsupported character.
    /// </summary>
#if CORE_NET
    [DataContract]
#else
    [Serializable]
#endif
    public class IncorrectStateException : TokenizerException
    {
        public IncorrectStateException(string correlationId = null, string code = null,
            string message = null, Exception innerException = null)
            : base(correlationId, code, message, innerException) { }
    }
}
