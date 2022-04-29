using System;
using PipServices3.Expressions.IO;
using Xunit;

namespace PipServices3.Expressions.Tokenizers.Generic
{
    public class CCommentStateTest
    {
        [Fact]
        public void TestNextToken()
        {
            var state = new CCommentState();

            var scanner = new StringScanner("// Comment \n Comment ");
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

            scanner = new StringScanner("/* Comment \n Comment */#");
            var token = state.NextToken(scanner, null);
            Assert.Equal("/* Comment \n Comment */", token.Value);
            Assert.Equal(TokenType.Comment, token.Type);
        }
    }
}
