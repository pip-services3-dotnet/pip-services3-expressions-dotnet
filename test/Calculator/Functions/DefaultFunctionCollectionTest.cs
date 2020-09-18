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
            parameters.Add(new Variant(new DateTime(2020, 1, 1, 12, 0, 0)));
            var operations = new TypeUnsafeVariantOperations();

            var func = collection.FindByName("TimeSpan");
            Assert.NotNull(func);

            var result = await func.CalculateAsync(parameters, operations);
            Assert.Equal(VariantType.TimeSpan, result.Type);
            Assert.Equal(new TimeSpan(12, 0, 0), result.AsTimeSpan);

            parameters.Clear();
            parameters.Add(new Variant(13));
            parameters.Add(new Variant(48));
            parameters.Add(new Variant(52));

            func = collection.FindByName("TimeSpan");
            Assert.NotNull(func);

            result = await func.CalculateAsync(parameters, operations);
            Assert.Equal(VariantType.TimeSpan, result.Type);
            Assert.Equal(new TimeSpan(13, 48, 52), result.AsTimeSpan);

            // WRONG_PARAM_COUNT exception
            parameters.Clear();
            parameters.Add(new Variant(1));
            parameters.Add(new Variant(1));
            parameters.Add(new Variant(1));
            parameters.Add(new Variant(1));

            func = collection.FindByName("TimeSpan");
            Assert.NotNull(func);

            await Assert.ThrowsAsync<ExpressionException>(() => func.CalculateAsync(parameters, operations));
        }
    }
}
