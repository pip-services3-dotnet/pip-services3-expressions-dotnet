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

            date = new DateTime(2020, 1, 1);
            calculator.Expression = "date >= DATE(2019, 1, 1) AND date <= DATE(2021, 1, 1)";
            calculator.DefaultVariables.FindByName("date").Value = new Variant(date);
            result = await calculator.EvaluateAsync();
            Assert.True(result.AsBoolean);

            calculator.Expression = "DAYOFWEEK(DATE(2020, 09, 18)) = 5";
            result = await calculator.EvaluateAsync();
            Assert.True(result.AsBoolean);

            calculator.Expression = "TIMEOFDAY(DATE(2020, 1, 1, 10, 17, 0)) > TIMEOFDAY(10, 15, 0)";
            result = await calculator.EvaluateAsync();
            Assert.True(result.AsBoolean);

            calculator.Expression = "'abcdf' LIKE 'abc'";
            result = await calculator.EvaluateAsync();
            Assert.True(result.AsBoolean);

            calculator.Expression = "'abc' LIKE 'd'";
            result = await calculator.EvaluateAsync();
            Assert.False(result.AsBoolean);

            calculator.Expression = "'abc' NOT LIKE 'd'";
            result = await calculator.EvaluateAsync();
            Assert.True(result.AsBoolean);
        }

        [Fact]
        public async Task TestExpressionCalculatorExtAsync()
        {
            ExpressionCalculator calculator = new ExpressionCalculator();

            calculator.Expression = "A NOT IN ARRAY('A','B','C')";
            Assert.Equal("A", calculator.DefaultVariables.FindByName("a").Name);
            calculator.DefaultVariables.FindByName("A").Value = new Variant("d");
            var result = await calculator.EvaluateAsync();
            Assert.Equal(VariantType.Boolean, result.Type);
            Assert.True(result.AsBoolean);

            calculator.Expression = "A IN ARRAY('A','B','C')";
            Assert.Equal("A", calculator.DefaultVariables.FindByName("a").Name);
            calculator.DefaultVariables.FindByName("A").Value = new Variant("B");
            result = await calculator.EvaluateAsync();
            Assert.Equal(VariantType.Boolean, result.Type);
            Assert.True(result.AsBoolean);

            calculator.Expression = "B IN ARRAY('A','B','C')";
            Assert.Equal("B", calculator.DefaultVariables.FindByName("B").Name);
            calculator.DefaultVariables.FindByName("B").Value = new Variant(null);
            result = await calculator.EvaluateAsync();
            Assert.Equal(VariantType.Boolean, result.Type);
            Assert.False(result.AsBoolean);
        }

        [Fact]
        public async Task TestEvaluationOfComplexVariables()
        {
            var variableName = "A.B";
            var calculator = new ExpressionCalculator
            {
                Expression = $"{variableName} > 10"
            };
            Assert.Equal(variableName, calculator.DefaultVariables.FindByName(variableName).Name);

            calculator.DefaultVariables.FindByName(variableName).Value = new Variant(20);

            var result = await calculator.EvaluateAsync();
            Assert.Equal(VariantType.Boolean, result.Type);
            Assert.True(result.AsBoolean);
        }

        [Theory]
        [InlineData("+", 0, 0, 0)]
        [InlineData("+", 1, 2, 3)]
        [InlineData("+", 1.5, 2.5, 4)]
        [InlineData("+", 2.51, 2.52, 5.03)]
        [InlineData("-", 0, 0, 0)]
        [InlineData("-", 3, 2, 1)]
        [InlineData("-", 4, 1.5, 2.5)]
        [InlineData("-", 5.03, 2.51, 2.52)]
        [InlineData("*", 0, 0, 0)]
        [InlineData("*", 3, 2, 6)]
        [InlineData("*", 4, 1.2, 4.8)]
        [InlineData("*", 5.05, 2.51, 12.6755)]
        [InlineData("/", 6, 3, 2)]
        [InlineData("/", 4.8, 4, 1.2)]
        [InlineData("/", 12.6755, 5.05, 2.51)]
        public async Task TestEvaluationOfDecimalValues(string operation, decimal valueA, decimal valueB, decimal expectedResult)
        {
            var variableNameA = "A";
            var variableNameB = "B";
            var calculator = new ExpressionCalculator($"{variableNameA} {operation} {variableNameB}");

            Assert.Equal(variableNameA, calculator.DefaultVariables.FindByName(variableNameA).Name);
            Assert.Equal(variableNameB, calculator.DefaultVariables.FindByName(variableNameB).Name);

            calculator.DefaultVariables.FindByName(variableNameA).Value = new Variant(valueA);
            calculator.DefaultVariables.FindByName(variableNameB).Value = new Variant(valueB);

            var result = await calculator.EvaluateAsync();
            Assert.Equal(VariantType.Decimal, result.Type);
            Assert.Equal(expectedResult, result.AsDecimal);
        }

        [Fact]
        public async Task TestEvaluationOfDecimalValuesWithAnotherValues()
        {
            var variableNameA = "A";
            decimal valueA = 10;
            var calculator = new ExpressionCalculator();

            calculator.Expression = $"{variableNameA} + 5";
            Assert.Equal("A", calculator.DefaultVariables.FindByName("a").Name);
            calculator.DefaultVariables.FindByName("A").Value = new Variant(valueA);
            var result = await calculator.EvaluateAsync();
            Assert.Equal(VariantType.Decimal, result.Type);
            Assert.Equal(15, result.AsDecimal);

            calculator.Expression = $"{variableNameA} + 5.5";
            Assert.Equal("A", calculator.DefaultVariables.FindByName("a").Name);
            calculator.DefaultVariables.FindByName("A").Value = new Variant(valueA);
            result = await calculator.EvaluateAsync();
            Assert.Equal(VariantType.Decimal, result.Type);
            Assert.Equal((decimal)15.5, result.AsDecimal);

            calculator.Expression = $"{variableNameA} + 5.5555";
            Assert.Equal("A", calculator.DefaultVariables.FindByName("a").Name);
            calculator.DefaultVariables.FindByName("A").Value = new Variant(valueA);
            result = await calculator.EvaluateAsync();
            Assert.Equal(VariantType.Decimal, result.Type);
            Assert.Equal((decimal)15.5555, result.AsDecimal);
        }
    }
}
