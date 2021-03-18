using System;
using PipServices3.Expressions.IO;
using Xunit;

namespace PipServices3.Expressions.Tokenizers.Generic
{
    public class GenericSymbolStateTest
    {
        [Fact]
        public void TestNextToken()
        {
            var state = new GenericSymbolState();
            state.Add("<", TokenType.Symbol);
            state.Add("<<", TokenType.Symbol);
            state.Add("<>", TokenType.Symbol);

            var scanner = new StringScanner("<A<<<>");

            var token = state.NextToken(scanner, null);
            Assert.Equal("<", token.Value);
            Assert.Equal(TokenType.Symbol, token.Type);

            token = state.NextToken(scanner, null);
            Assert.Equal("A", token.Value);
            Assert.Equal(TokenType.Symbol, token.Type);

            token = state.NextToken(scanner, null);
            Assert.Equal("<<", token.Value);
            Assert.Equal(TokenType.Symbol, token.Type);

            token = state.NextToken(scanner, null);
            Assert.Equal("<>", token.Value);
            Assert.Equal(TokenType.Symbol, token.Type);
        }
    }
}
