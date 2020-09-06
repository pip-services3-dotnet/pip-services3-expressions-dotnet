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

            var reader = new StringPushbackReader("AB_CD=");
            var token = state.NextToken(reader, null);
            Assert.Equal("AB_CD", token.Value);
            Assert.Equal(TokenType.Word, token.Type);
        }
    }
}
