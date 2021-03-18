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

            var scanner = new StringScanner("# Comment \r# Comment ");
            var token = state.NextToken(scanner, null);
            Assert.Equal("# Comment ", token.Value);
            Assert.Equal(TokenType.Comment, token.Type);

            scanner = new StringScanner("# Comment \n# Comment ");
            token = state.NextToken(scanner, null);
            Assert.Equal("# Comment ", token.Value);
            Assert.Equal(TokenType.Comment, token.Type);
        }
    }
}
