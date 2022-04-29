using System;
using PipServices3.Expressions.IO;
using Xunit;

namespace PipServices3.Expressions.Tokenizers.Generic
{
    public class GenericWordStateTest
    {
        [Fact]
        public void TestNextToken()
        {
            var state = new GenericWordState();

            var scanner = new StringScanner("AB_CD=");
            var token = state.NextToken(scanner, null);
            Assert.Equal("AB_CD", token.Value);
            Assert.Equal(TokenType.Word, token.Type);
        }
    }
}
