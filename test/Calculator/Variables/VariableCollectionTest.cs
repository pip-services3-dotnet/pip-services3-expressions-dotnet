﻿using System;
using Xunit;

namespace PipServices3.Expressions.Calculator.Variables
{
    public class VariableCollectionTest
    {
        [Fact]
        public void TestAddRemoveVariables()
        {
            var collection = new VariableCollection();

            var var1 = new Variable("ABC");
            collection.Add(var1);
            Assert.Equal(1, collection.Length);

            var var2 = new Variable("XYZ");
            collection.Add(var2);
            Assert.Equal(2, collection.Length);

            var index = collection.FindIndexByName("abc");
            Assert.Equal(0, index);

            var v = collection.FindByName("Xyz");
            Assert.Equal(var2, v);

            var var3 = collection.Locate("ghi");
            Assert.NotNull(var3);
            Assert.Equal("ghi", var3.Name);
            Assert.Equal(3, collection.Length);

            collection.Remove(0);
            Assert.Equal(2, collection.Length);

            collection.RemoveByName("GHI");
            Assert.Equal(1, collection.Length);
        }
    }
}