using System;
using System.Runtime.Serialization;

using PipServices3.Commons.Errors;


namespace PipServices3.Expressions.Mustache
{
    /// <summary>
    /// Exception that can be thrown by Mustache Template.
    /// </summary>
    public class MustacheException : BadRequestException
    {

        public MustacheException(string correlationId = null, string code = null, string message = null) : base(correlationId, code, message)
        {

        }
    }
}