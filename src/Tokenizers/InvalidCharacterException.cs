using System;

namespace PipServices3.Expressions.Tokenizers
{
    /// <summary>
    /// Exception thrown when incorrect character is detected input stream. 
    /// </summary>
#if CORE_NET
    [DataContract]
#else
    [Serializable]
#endif
    public class InvalidCharacterException : TokenizerException
    {
        public InvalidCharacterException(string correlationId = null, string code = null,
            string message = null, Exception innerException = null)
            : base(correlationId, code, message, innerException) { }
    }
}
