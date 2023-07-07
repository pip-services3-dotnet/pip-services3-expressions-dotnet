using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PipServices3.Expressions.Variants;
using Xunit;

namespace PipServices3.Expressions.Calculator.Functions
{
    public class DefaultFunctionCollectionTest
    {
        [Fact]
        public async Task TestCalculateFunctionsAsync()
        {
            var collection = new DefaultFunctionCollection();
            var parameters = new List<Variant>();
            parameters.Add(new Variant(1));
            parameters.Add(new Variant(2));
            parameters.Add(new Variant(3));
            var operations = new TypeUnsafeVariantOperations();

            var func = collection.FindByName("sum");
            Assert.NotNull(func);

            var result = await func.CalculateAsync(parameters, operations);
            Assert.Equal(VariantType.Integer, result.Type);
            Assert.Equal(6, result.AsInteger);
        }

        [Fact]
        public async Task TestDateFunctionsAsync()
        {
            var collection = new DefaultFunctionCollection();
            var parameters = new List<Variant>();
            var operations = new TypeUnsafeVariantOperations();

            var func = collection.FindByName("now");
            Assert.NotNull(func);

            var result = await func.CalculateAsync(parameters, operations);
            Assert.Equal(VariantType.DateTime, result.Type);

            parameters.Clear();
            parameters.Add(new Variant(1975));
            parameters.Add(new Variant(4));
            parameters.Add(new Variant(8));

            func = collection.FindByName("date");
            Assert.NotNull(func);

            result = await func.CalculateAsync(parameters, operations);
            Assert.Equal(VariantType.DateTime, result.Type);
            Assert.Equal(new DateTime(1975,4,8), result.AsDateTime);

            parameters.Clear();
            parameters.Add(new Variant(new DateTime(2020, 09, 18)));

            func = collection.FindByName("DayOfWeek");
            Assert.NotNull(func);

            result = await func.CalculateAsync(parameters, operations);
            Assert.Equal(VariantType.Integer, result.Type);
            Assert.Equal(5, result.AsInteger);
        }

        [Fact]
        public async Task TestTimeSpanFunctionAsync()
        {
            var collection = new DefaultFunctionCollection();
            var parameters = new List<Variant>();
            parameters.Add(Variant.FromLong(123));
            var operations = new TypeUnsafeVariantOperations();

            var func = collection.FindByName("TimeSpan");
            Assert.NotNull(func);

            var result = await func.CalculateAsync(parameters, operations);
            Assert.Equal(VariantType.TimeSpan, result.Type);
            Assert.Equal(new TimeSpan(123), result.AsTimeSpan);

            parameters.Clear();
            parameters.Add(new Variant(0));
            parameters.Add(new Variant(13));
            parameters.Add(new Variant(48));
            parameters.Add(new Variant(52));

            func = collection.FindByName("TimeSpan");
            Assert.NotNull(func);

            result = await func.CalculateAsync(parameters, operations);
            Assert.Equal(VariantType.TimeSpan, result.Type);
            Assert.Equal(new TimeSpan(0, 13, 48, 52), result.AsTimeSpan);

            // WRONG_PARAM_COUNT exception
            parameters.Clear();
            parameters.Add(new Variant(1));
            parameters.Add(new Variant(1));
            parameters.Add(new Variant(1));
            parameters.Add(new Variant(1));
            parameters.Add(new Variant(1));
            parameters.Add(new Variant(1));

            func = collection.FindByName("TimeSpan");
            Assert.NotNull(func);

            await Assert.ThrowsAsync<ExpressionException>(() => func.CalculateAsync(parameters, operations));
        }

        [Fact]
        public async Task TestAvgFunctionAsync()
        {
            var collection = new DefaultFunctionCollection();
            var parameters = new List<Variant>();
            var operations = new TypeUnsafeVariantOperations();
            
            var func = collection.FindByName("avg");
            Assert.NotNull(func);

            // AsInteger Avg(1, 2, 3)=2
            parameters.Clear();
            parameters.Add(new Variant(1));
            parameters.Add(new Variant(2));
            parameters.Add(new Variant(3));
            
            var result = await func.CalculateAsync(parameters, operations);
            Assert.Equal(VariantType.Integer, result.Type);
            Assert.Equal(2, result.AsInteger);

            // AsInteger Avg(1, 2, 4)=2.
            parameters.Clear();
            parameters.Add(new Variant(1));
            parameters.Add(new Variant(2));
            parameters.Add(new Variant(4));

            result = await func.CalculateAsync(parameters, operations);
            Assert.Equal(VariantType.Integer, result.Type);
            Assert.Equal(2, result.AsInteger);

            // AsDouble Avg(2, 3)=2.5
            func = collection.FindByName("Average");
            Assert.NotNull(func);

            parameters.Clear();
            parameters.Add(new Variant(2.0));
            parameters.Add(new Variant(3.0));

            result = await func.CalculateAsync(parameters, operations);
            Assert.Equal(VariantType.Double, result.Type);
            Assert.Equal(2.5, result.AsDouble);

            // AsDecimal Avg(3.3, 4.4, 5.5, 6.6)=4.95
            parameters.Clear();
            parameters.Add(new Variant(3.3m));
            parameters.Add(new Variant(4.4m));
            parameters.Add(new Variant(5.5m));
            parameters.Add(new Variant(6.6m));

            result = await func.CalculateAsync(parameters, operations);
            Assert.Equal(VariantType.Decimal, result.Type);
            Assert.Equal(4.95m, result.AsDecimal);

            // WRONG_PARAM_COUNT exception
            parameters.Clear();
            parameters.Add(new Variant(1));

            await Assert.ThrowsAsync<ExpressionException>(() => func.CalculateAsync(parameters, operations));
        }
    }
}
