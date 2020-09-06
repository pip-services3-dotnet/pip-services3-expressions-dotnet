using System;
using System.Threading.Tasks;
using PipServices3.Expressions.Variants;
using Xunit;

namespace PipServices3.Expressions.Calculator.Functions
{
    public class FunctionCollectionTest
    {
        private Task<Variant> TestFunc(CalculationStack stack, IVariantOperations variantOperations)
        {
            return Task.FromResult(new Variant());
        }

        [Fact]
        public void TestAddRemoveFunctions()
        {
            var collection = new FunctionCollection();

            var func1 = new DelegatedFunction("ABC", TestFunc);
            collection.Add(func1);
            Assert.Equal(1, collection.Length);

            var func2 = new DelegatedFunction("XYZ", TestFunc);
            collection.Add(func2);
            Assert.Equal(2, collection.Length);

            var index = collection.FindIndexByName("abc");
            Assert.Equal(0, index);

            var func = collection.FindByName("Xyz");
            Assert.Equal(func2, func);

            collection.Remove(0);
            Assert.Equal(1, collection.Length);

            collection.RemoveByName("XYZ");
            Assert.Equal(0, collection.Length);
        }
    }
}
