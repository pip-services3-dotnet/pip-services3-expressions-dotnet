using System;
using PipServices3.Expressions.IO;
using Xunit;

namespace PipServices3.Expressions.Tokenizers.Generic
{
    public class GenericQuoteStateTest
    {
        [Fact]
        public void TestNextToken()
        {
            var state = new GenericQuoteState();

            var scanner = new StringScanner("'ABC#DEF'#");
            var token = state.NextToken(scanner, null);
            Assert.Equal("'ABC#DEF'", token.Value);
            Assert.Equal(TokenType.Quoted, token.Type);

            scanner = new StringScanner("'ABC#DEF''");
            token = state.NextToken(scanner, null);
            Assert.Equal("'ABC#DEF'", token.Value);
            Assert.Equal(TokenType.Quoted, token.Type);
        }

        [Fact]
        public void TestEncodeAndDecodeString()
        {
            var state = new GenericQuoteState();

            var value = state.EncodeString("ABC", '\'');
            Assert.Equal("'ABC'", value);

            value = state.DecodeString(value, '\'');
            Assert.Equal("ABC", value);

            value = state.DecodeString("'ABC'DEF'", '\'');
            Assert.Equal("ABC'DEF", value);
        }
    }
}