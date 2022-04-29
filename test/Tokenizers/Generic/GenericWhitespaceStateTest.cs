using System;
using PipServices3.Expressions.IO;
using Xunit;

namespace PipServices3.Expressions.Tokenizers.Generic
{
    public class GenericWhitespaceStateTest
    {
        [Fact]
        public void TestNextToken()
        {
            var state = new GenericWhitespaceState();

            var scanner = new StringScanner(" \t\n\r ");
            var token = state.NextToken(scanner, null);
            Assert.Equal(" \t\n\r ", token.Value);
            Assert.Equal(TokenType.Whitespace, token.Type);
        }
    }
}
