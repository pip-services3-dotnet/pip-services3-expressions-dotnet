using System;
using System.Threading.Tasks;
using PipServices3.Expressions.Variants;

namespace PipServices3.Expressions.Calculator.Functions
{
    /// <summary>
    /// Defines an interface for expression function.
    /// </summary>
    public interface IFunction
    {
        /// <summary>
        /// The function name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The function calculation method.
        /// </summary>
        /// <param name="stack">The stack to get function parameters and place</param>
        /// <param name="variantOperations">Variants operations manager.</param>
        Task<Variant> CalculateAsync(CalculationStack stack, IVariantOperations variantOperations);
    }
}
