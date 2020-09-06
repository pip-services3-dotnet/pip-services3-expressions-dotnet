using System;
using PipServices3.Expressions.IO;
using Xunit;

namespace PipServices3.Expressions.Tokenizers.Generic
{
    public class GenericCommentStateTest
    {
        [Fact]
        public void TestNextToken()
        {
            var state = new GenericCommentState();

            var reader = new StringPushbackReader("# Comment \r# Comment ");
            var token = state.NextToken(reader, null);
            Assert.Equal("# Comment ", token.Value);
            Assert.Equal(TokenType.Comment, token.Type);

            reader = new StringPushbackReader("# Comment \n# Comment ");
            token = state.NextToken(reader, null);
            Assert.Equal("# Comment ", token.Value);
            Assert.Equal(TokenType.Comment, token.Type);
        }
    }
}
