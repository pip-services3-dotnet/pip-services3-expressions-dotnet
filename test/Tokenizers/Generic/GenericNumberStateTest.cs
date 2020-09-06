using System;
using PipServices3.Expressions.IO;
using Xunit;

namespace PipServices3.Expressions.Tokenizers.Generic
{
    public class GenericNumberStateTest
    {
        [Fact]
        public void TestNextToken()
        {
            var state = new GenericNumberState();

            var reader = new StringPushbackReader("ABC");
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

            reader = new StringPushbackReader("123#");
            var token = state.NextToken(reader, null);
            Assert.Equal("123", token.Value);
            Assert.Equal(TokenType.Integer, token.Type);

            reader = new StringPushbackReader("-123#");
            token = state.NextToken(reader, null);
            Assert.Equal("-123", token.Value);
            Assert.Equal(TokenType.Integer, token.Type);
            
            reader = new StringPushbackReader("123.#");
            token = state.NextToken(reader, null);
            Assert.Equal("123.", token.Value);
            Assert.Equal(TokenType.Float, token.Type);

            reader = new StringPushbackReader("123.456#");
            token = state.NextToken(reader, null);
            Assert.Equal("123.456", token.Value);
            Assert.Equal(TokenType.Float, token.Type);

            reader = new StringPushbackReader("-123.456#");
            token = state.NextToken(reader, null);
            Assert.Equal("-123.456", token.Value);
            Assert.Equal(TokenType.Float, token.Type);
        }
    }
}
