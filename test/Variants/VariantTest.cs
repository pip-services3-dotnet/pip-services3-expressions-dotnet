using System;
using Xunit;

namespace PipServices3.Expressions.Variants
{
    public class VariantTest
    {
        [Fact]
        public void TestVariants()
        {
            Variant a = new Variant((int)123);
            Assert.Equal(VariantType.Integer, a.Type);
            Assert.Equal(123, a.AsInteger);
            Assert.Equal(123, a.AsObject);

            Variant b = new Variant("xyz");
            Assert.Equal(VariantType.String, b.Type);
            Assert.Equal("xyz", b.AsString);
            Assert.Equal("xyz", b.AsObject);
        }
    }
}
