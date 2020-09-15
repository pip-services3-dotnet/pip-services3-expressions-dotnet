using System;
using System.Threading.Tasks;
using PipServices3.Expressions.Variants;
using Xunit;

namespace PipServices3.Expressions.Calculator
{
    /// <summary>
    /// Contains unit tests for Expression package classes.
    /// </summary>
    public class ExpressionCalculatorTest
    {
        [Fact]
        public async Task TestExpressionCalculatorAsync()
        {
            ExpressionCalculator calculator = new ExpressionCalculator();

            calculator.Expression = "2 + 2";
            Variant result = await calculator.EvaluateAsync();
            Assert.Equal(VariantType.Integer, result.Type);
            Assert.Equal(4, result.AsInteger);

            calculator.Expression = "A + b / (3 - Max(-123, 1)*2)";
            Assert.Equal("A", calculator.DefaultVariables.FindByName("a").Name);
            Assert.Equal("b", calculator.DefaultVariables.FindByName("b").Name);
            calculator.DefaultVariables.FindByName("a").Value = new Variant("xyz");
            calculator.DefaultVariables.FindByName("b").Value = new Variant(123);
            result = await calculator.EvaluateAsync();
            Assert.Equal(VariantType.String, result.Type);
            Assert.Equal("xyz123", result.AsString);

            calculator.Expression = "'abc'[1]";
            result = await calculator.EvaluateAsync();
            Assert.Equal(VariantType.String, result.Type);
            Assert.Equal("b", result.AsString);

            calculator.Expression = "1 > 2";
            result = await calculator.EvaluateAsync();
            Assert.Equal(VariantType.Boolean, result.Type);
            Assert.False(result.AsBoolean);

            calculator.Expression = "2 IN ARRAY(1,2,3)";
            result = await calculator.EvaluateAsync();
            Assert.Equal(VariantType.Boolean, result.Type);
            Assert.True(result.AsBoolean);

            calculator.Expression = "5 NOT IN ARRAY(1,2,3)";
            result = await calculator.EvaluateAsync();
            Assert.Equal(VariantType.Boolean, result.Type);
            Assert.True(result.AsBoolean);
        }

    }
}
