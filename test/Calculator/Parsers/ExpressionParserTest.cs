using System;
using PipServices3.Expressions.Variants;
using Xunit;

namespace PipServices3.Expressions.Calculator.Parsers
{
    public class ExpressionParserTest
    {
        [Fact]
        public void TestParseString()
        {
            var parser = new ExpressionParser();
            parser.Expression = "(2+2)*ABS(-2)";
            var expectedTokens = new ExpressionToken[] {
                new ExpressionToken(ExpressionTokenType.Constant, Variant.FromInteger(2)),
                new ExpressionToken(ExpressionTokenType.Constant, Variant.FromInteger(2)),
                new ExpressionToken(ExpressionTokenType.Plus, Variant.Empty),
                new ExpressionToken(ExpressionTokenType.Constant, Variant.FromInteger(2)),
                new ExpressionToken(ExpressionTokenType.Unary, Variant.Empty),
                new ExpressionToken(ExpressionTokenType.Constant, Variant.FromInteger(1)),
                new ExpressionToken(ExpressionTokenType.Function, Variant.FromString("ABS")),
                new ExpressionToken(ExpressionTokenType.Star, Variant.Empty),
            };

            var tokens = parser.ResultTokens;
            Assert.Equal(expectedTokens.Length, tokens.Count);

            for (var i = 0; i < tokens.Count; i++)
            {
                Assert.Equal(expectedTokens[i].Type, tokens[i].Type);
                Assert.Equal(expectedTokens[i].Value.Type, tokens[i].Value.Type);
                Assert.Equal(expectedTokens[i].Value.AsObject, tokens[i].Value.AsObject);
            }
        }
    }
}
