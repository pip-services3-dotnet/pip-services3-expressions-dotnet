using System;
using PipServices3.Expressions.IO;
using Xunit;

namespace PipServices3.Expressions.Tokenizers.Generic
{
    public class SymbolRootNodeTest
    {
        [Fact]
        public void TestNextToken()
        {
            var node = new SymbolRootNode();
            node.Add("<", TokenType.Symbol);
            node.Add("<<", TokenType.Symbol);
            node.Add("<>", TokenType.Symbol);

            var scanner = new StringScanner("<A<<<>");

            var token = node.NextToken(scanner);
            Assert.Equal("<", token.Value);

            token = node.NextToken(scanner);
            Assert.Equal("A", token.Value);

            token = node.NextToken(scanner);
            Assert.Equal("<<", token.Value);

            token = node.NextToken(scanner);
            Assert.Equal("<>", token.Value);
        }

        [Fact]
        public void TestSingleToken()
        {
            var node = new SymbolRootNode();

            var scanner = new StringScanner("<A");

            var token = node.NextToken(scanner);
            Assert.Equal("<", token.Value);
            Assert.Equal(TokenType.Symbol, token.Type);
        }
    }
}
