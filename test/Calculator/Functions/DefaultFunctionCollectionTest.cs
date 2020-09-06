using System;
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
            var stack = new CalculationStack();
            stack.Push(new Variant(1));
            stack.Push(new Variant(2));
            stack.Push(new Variant(3));
            stack.Push(new Variant(3));
            var operations = new TypeUnsafeVariantOperations();

            var func = collection.FindByName("sum");
            Assert.NotNull(func);

            var result = await func.CalculateAsync(stack, operations);
            Assert.Equal(VariantType.Integer, result.Type);
            Assert.Equal(6, result.AsInteger);
        }
    }
}
