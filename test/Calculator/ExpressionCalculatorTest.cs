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

            calculator.Expression = "NOT(7 IN ARRAY(1,2,3))";
            result = await calculator.EvaluateAsync();
            Assert.Equal(VariantType.Boolean, result.Type);
            Assert.True(result.AsBoolean);

            calculator.Expression = "A = 123 AND b = 'abc'";
            Assert.Equal("A", calculator.DefaultVariables.FindByName("a").Name);
            Assert.Equal("b", calculator.DefaultVariables.FindByName("b").Name);
            calculator.DefaultVariables.FindByName("a").Value = new Variant(123);
            calculator.DefaultVariables.FindByName("b").Value = new Variant("abc");
            result = await calculator.EvaluateAsync();
            Assert.True(result.AsBoolean);

            calculator.Expression = "A = 2 OR b = 'abc'";
            result = await calculator.EvaluateAsync();
            Assert.True(result.AsBoolean);

            calculator.Expression = "x IS NOT NULL";
            calculator.DefaultVariables.FindByName("x").Value = new Variant(123);
            result = await calculator.EvaluateAsync();
            Assert.True(result.AsBoolean);

            calculator.Expression = "y IS NULL";
            result = await calculator.EvaluateAsync();
            Assert.True(result.AsBoolean);

            calculator.Expression = "x = 123 AND (y != '' OR z IS NULL)";
            calculator.DefaultVariables.FindByName("x").Value = new Variant(123);
            calculator.DefaultVariables.FindByName("y").Value = new Variant("");
            result = await calculator.EvaluateAsync();
            Assert.True(result.AsBoolean);

            
            var date = DateTime.UtcNow;

            calculator.Expression = "date1 = date2";
            calculator.DefaultVariables.FindByName("date1").Value = new Variant(date);
            calculator.DefaultVariables.FindByName("date2").Value = new Variant(date);
            result = await calculator.EvaluateAsync();
            Assert.True(result.AsBoolean);

            calculator.Expression = "date1 > date2";
            calculator.DefaultVariables.FindByName("date1").Value = new Variant(date.AddDays(1));
            calculator.DefaultVariables.FindByName("date2").Value = new Variant(date);
            result = await calculator.EvaluateAsync();
            Assert.True(result.AsBoolean);

            calculator.Expression = "date1 < date2 AND date2 <= date3";
            calculator.DefaultVariables.FindByName("date1").Value = new Variant(date.AddDays(-1));
            calculator.DefaultVariables.FindByName("date2").Value = new Variant(date);
            calculator.DefaultVariables.FindByName("date3").Value = new Variant(date);
            result = await calculator.EvaluateAsync();
            Assert.True(result.AsBoolean);
        }

    }
}
