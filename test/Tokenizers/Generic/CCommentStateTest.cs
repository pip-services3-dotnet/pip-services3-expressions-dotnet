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

            var reader = new StringPushbackReader("// Comment \n Comment ");
            var failed = false;
            try
            {
                state.NextToken(reader, null);                
            }
            catch
            {
                failed = true;
            }
            Assert.True(failed);

            reader = new StringPushbackReader("/* Comment \n Comment */#");
            var token = state.NextToken(reader, null);
            Assert.Equal("/* Comment \n Comment */", token.Value);
            Assert.Equal(TokenType.Comment, token.Type);
        }
    }
}
