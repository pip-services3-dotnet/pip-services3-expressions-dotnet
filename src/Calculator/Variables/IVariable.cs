using System;

using PipServices3.Expressions.Variants;

namespace PipServices3.Expressions.Calculator.Variables
{
    /// <summary>
    /// Defines a variable interface.
    /// </summary>
    public interface IVariable
    {
        /// <summary>
        /// The variable name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The variable value.
        /// </summary>
        Variant Value { get; set; }
    }
}
