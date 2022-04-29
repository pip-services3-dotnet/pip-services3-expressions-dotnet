using System;
using PipServices3.Expressions.IO;
using Xunit;

namespace PipServices3.Expressions.Tokenizers.Generic
{
    public class CppCommentStateTest
    {
        [Fact]
        public void TestNextToken()
        {
            var state = new CppCommentState();

            var scanner = new StringScanner("-- Comment \n Comment ");
            var failed = false;
            try
            {
                state.NextToken(scanner, null);
            }
            catch
            {
                failed = true;
            }
            Assert.True(failed);

            scanner = new StringScanner("// Comment \n Comment ");
            var token = state.NextToken(scanner, null);
            Assert.Equal("// Comment ", token.Value);
            Assert.Equal(TokenType.Comment, token.Type);

            scanner = new StringScanner("/* Comment \n Comment */#");
            token = state.NextToken(scanner, null);
            Assert.Equal("/* Comment \n Comment */", token.Value);
            Assert.Equal(TokenType.Comment, token.Type);
        }
    }
}
