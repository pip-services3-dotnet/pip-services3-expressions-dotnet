using System;
using System.Threading.Tasks;
using PipServices3.Expressions.Variants;

namespace PipServices3.Expressions.Calculator.Functions
{
    /// <summary>
    /// Implements a list filled with standard functions.
    /// </summary>
    public class DefaultFunctionCollection : FunctionCollection
    {
        private static Random _random = new Random();

        /// <summary>
        /// Constructs this list and fills it with the standard functions.
        /// </summary>
        public DefaultFunctionCollection()
        {
            Add(new DelegatedFunction("Time", (FunctionCalculator)TimeFunctionCalculatorAsync));
            Add(new DelegatedFunction("Now", (FunctionCalculator)TimeFunctionCalculatorAsync));
            Add(new DelegatedFunction("Min", (FunctionCalculator)MinFunctionCalculatorAsync));
            Add(new DelegatedFunction("Max", (FunctionCalculator)MaxFunctionCalculatorAsync));
            Add(new DelegatedFunction("Sum", (FunctionCalculator)SumFunctionCalculatorAsync));
            Add(new DelegatedFunction("If", (FunctionCalculator)IfFunctionCalculatorAsync));
            Add(new DelegatedFunction("Choose", (FunctionCalculator)ChooseFunctionCalculatorAsync));
            Add(new DelegatedFunction("E", (FunctionCalculator)EFunctionCalculatorAsync));
            Add(new DelegatedFunction("Pi", (FunctionCalculator)PiFunctionCalculatorAsync));
            Add(new DelegatedFunction("Rnd", (FunctionCalculator)RndFunctionCalculatorAsync));
            Add(new DelegatedFunction("Random", (FunctionCalculator)RndFunctionCalculatorAsync));
            Add(new DelegatedFunction("Abs", (FunctionCalculator)AbsFunctionCalculatorAsync));
            Add(new DelegatedFunction("Acos", (FunctionCalculator)AcosFunctionCalculatorAsync));
            Add(new DelegatedFunction("Asin", (FunctionCalculator)AsinFunctionCalculatorAsync));
            Add(new DelegatedFunction("Atan", (FunctionCalculator)AtanFunctionCalculatorAsync));
            Add(new DelegatedFunction("Exp", (FunctionCalculator)ExpFunctionCalculatorAsync));
            Add(new DelegatedFunction("Log", (FunctionCalculator)LogFunctionCalculatorAsync));
            Add(new DelegatedFunction("Ln", (FunctionCalculator)LogFunctionCalculatorAsync));
            Add(new DelegatedFunction("Log10", (FunctionCalculator)Log10FunctionCalculatorAsync));
            Add(new DelegatedFunction("Ceil", (FunctionCalculator)CeilFunctionCalculatorAsync));
            Add(new DelegatedFunction("Ceiling", (FunctionCalculator)CeilFunctionCalculatorAsync));
            Add(new DelegatedFunction("Floor", (FunctionCalculator)FloorFunctionCalculatorAsync));
            Add(new DelegatedFunction("Round", (FunctionCalculator)RoundFunctionCalculatorAsync));
            Add(new DelegatedFunction("Trunc", (FunctionCalculator)TruncFunctionCalculatorAsync));
            Add(new DelegatedFunction("Truncate", (FunctionCalculator)TruncFunctionCalculatorAsync));
            Add(new DelegatedFunction("Cos", (FunctionCalculator)CosFunctionCalculatorAsync));
            Add(new DelegatedFunction("Sin", (FunctionCalculator)SinFunctionCalculatorAsync));
            Add(new DelegatedFunction("Tan", (FunctionCalculator)TanFunctionCalculatorAsync));
            Add(new DelegatedFunction("Sqr", (FunctionCalculator)SqrtFunctionCalculatorAsync));
            Add(new DelegatedFunction("Sqrt", (FunctionCalculator)SqrtFunctionCalculatorAsync));
            Add(new DelegatedFunction("Empty", (FunctionCalculator)EmptyFunctionCalculatorAsync));
            Add(new DelegatedFunction("Null", (FunctionCalculator)NullFunctionCalculatorAsync));
            Add(new DelegatedFunction("Contains", (FunctionCalculator)ContainsFunctionCalculatorAsync));
        }

        /// <summary>
        /// Checks if stack contains the correct number of function parameters (must be stored on the top of the stack).
        /// </summary>
        /// <param name="stack">The stack with function parameters.</param>
        /// <param name="expectedParamCount">The expected number of function parameters.</param>
        protected static void CheckParamCount(CalculationStack stack, int expectedParamCount)
        {
            Variant paramCount = stack.Peek();
            if (paramCount.Type != VariantType.Integer)
            {
                throw new InvalidProgramException("Internal error.");
            }
            if (expectedParamCount != paramCount.AsInteger)
            {
                throw new InvalidProgramException(String.Format("Expected {0} parameters but was found {1}",
                    expectedParamCount, paramCount.AsInteger));
            }
            if (stack.Length < paramCount.AsInteger)
            {
                throw new InvalidProgramException("Stack does not contain sufficient numeber of function parameters.");
            }
        }

        /// <summary>
        /// Gets function parameter by it's index.
        /// </summary>
        /// <param name="stack">The stack with function parameters.</param>
        /// <param name="paramIndex">Index for the function parameter (0 for the first parameter).</param>
        /// <returns>Function parameter value.</returns>
        protected static Variant GetParameter(CalculationStack stack, int paramIndex)
        {
            Variant paramCount = stack.Peek();
            if (paramCount.Type != VariantType.Integer)
            {
                throw new InvalidProgramException("Internal error.");
            }
            return stack.PeekAt(stack.Length - 1 - paramCount.AsInteger + paramIndex);
        }

        private Task<Variant> TimeFunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            CheckParamCount(stack, 0);
            Variant result = new Variant((System.DateTime.Now.Ticks - 621355968000000000) / 10000);
            return Task.FromResult(result);
        }

        private Task<Variant> MinFunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            int paramCount = stack.Peek().AsInteger;
            if (paramCount < 2)
            {
                throw new InvalidProgramException("Expected at least 2 parameters");
            }
            Variant result = GetParameter(stack, 0);
            for (int i = 1; i < paramCount; i++)
            {
                Variant value = GetParameter(stack, i);
                if (variantOperations.More(result, value).AsBoolean)
                {
                    result = value;
                }
            }
            return Task.FromResult(result);
        }

        private Task<Variant> MaxFunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            int paramCount = stack.Peek().AsInteger;
            if (paramCount < 2)
            {
                throw new InvalidProgramException("Expected at least 2 parameters");
            }
            Variant result = GetParameter(stack, 0);
            for (int i = 1; i < paramCount; i++)
            {
                Variant value = GetParameter(stack, i);
                if (variantOperations.Less(result, value).AsBoolean)
                {
                    result = value;
                }
            }
            return Task.FromResult(result);
        }

        private Task<Variant> SumFunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            int paramCount = stack.Peek().AsInteger;
            if (paramCount < 2)
            {
                throw new InvalidProgramException("Expected at least 2 parameters");
            }
            Variant result = GetParameter(stack, 0);
            for (int i = 1; i < paramCount; i++)
            {
                Variant value = GetParameter(stack, i);
                result = variantOperations.Add(result, value);
            }
            return Task.FromResult(result);
        }

        private Task<Variant> IfFunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            CheckParamCount(stack, 3);
            Variant value1 = GetParameter(stack, 0);
            Variant value2 = GetParameter(stack, 1);
            Variant value3 = GetParameter(stack, 2);
            Variant condition = variantOperations.Convert(value1, VariantType.Boolean);
            Variant result = condition.AsBoolean ? value2 : value3;
            return Task.FromResult(result);
        }

        private Task<Variant> ChooseFunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            int paramCount = stack.Peek().AsInteger;
            if (paramCount < 3)
            {
                throw new InvalidProgramException("Expected at least 3 parameters");
            }

            Variant value1 = GetParameter(stack, 0);
            Variant condition = variantOperations.Convert(value1, VariantType.Integer);
            int paramIndex = condition.AsInteger;

            if (paramCount < paramIndex + 1)
            {
                throw new InvalidProgramException(string.Format("Expected at least {0} parameters",
                    paramIndex + 1));
            }

            Variant result = GetParameter(stack, paramIndex);
            return Task.FromResult(result);
        }

        private Task<Variant> EFunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            CheckParamCount(stack, 0);
            Variant result = new Variant(System.Math.E);
            return Task.FromResult(result);
        }

        private Task<Variant> PiFunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            CheckParamCount(stack, 0);
            Variant result = new Variant(System.Math.PI);
            return Task.FromResult(result);
        }

        private Task<Variant> RndFunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            CheckParamCount(stack, 0);
            Variant result = new Variant(_random.NextDouble());
            return Task.FromResult(result);
        }

        private Task<Variant> AbsFunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            CheckParamCount(stack, 1);
            Variant value = GetParameter(stack, 0);
            Variant result = new Variant();
            switch (value.Type)
            {
                case VariantType.Integer:
                    result.AsInteger = System.Math.Abs(value.AsInteger);
                    break;
                case VariantType.Long:
                    result.AsLong = System.Math.Abs(value.AsLong);
                    break;
                case VariantType.Float:
                    result.AsFloat = System.Math.Abs(value.AsFloat);
                    break;
                case VariantType.Double:
                    result.AsDouble = System.Math.Abs(value.AsDouble);
                    break;
                default:
                    value = variantOperations.Convert(value, VariantType.Double);
                    result.AsDouble = System.Math.Abs(value.AsDouble);
                    break;
            }
            return Task.FromResult(result);
        }

        private Task<Variant> AcosFunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            CheckParamCount(stack, 1);
            Variant value = variantOperations.Convert(GetParameter(stack, 0), VariantType.Double);
            Variant result = new Variant(System.Math.Acos(value.AsDouble));
            return Task.FromResult(result);
        }

        private Task<Variant> AsinFunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            CheckParamCount(stack, 1);
            Variant value = variantOperations.Convert(GetParameter(stack, 0), VariantType.Double);
            Variant result = new Variant(System.Math.Asin(value.AsDouble));
            return Task.FromResult(result);
        }

        private Task<Variant> AtanFunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            CheckParamCount(stack, 1);
            Variant value = variantOperations.Convert(GetParameter(stack, 0), VariantType.Double);
            Variant result = new Variant(System.Math.Atan(value.AsDouble));
            return Task.FromResult(result);
        }

        private Task<Variant> ExpFunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            CheckParamCount(stack, 1);
            Variant value = variantOperations.Convert(GetParameter(stack, 0), VariantType.Double);
            Variant result = new Variant(System.Math.Exp(value.AsDouble));
            return Task.FromResult(result);
        }

        private Task<Variant> LogFunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            CheckParamCount(stack, 1);
            Variant value = variantOperations.Convert(GetParameter(stack, 0), VariantType.Double);
            Variant result = new Variant(System.Math.Log(value.AsDouble));
            return Task.FromResult(result);
        }

        private Task<Variant> Log10FunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            CheckParamCount(stack, 1);
            Variant value = variantOperations.Convert(GetParameter(stack, 0), VariantType.Double);
            Variant result = new Variant(System.Math.Log10(value.AsDouble));
            return Task.FromResult(result);
        }

        private Task<Variant> CeilFunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            CheckParamCount(stack, 1);
            Variant value = variantOperations.Convert(GetParameter(stack, 0), VariantType.Double);
            Variant result = new Variant(System.Math.Ceiling(value.AsDouble));
            return Task.FromResult(result);
        }

        private Task<Variant> FloorFunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            CheckParamCount(stack, 1);
            Variant value = variantOperations.Convert(GetParameter(stack, 0), VariantType.Double);
            Variant result = new Variant(System.Math.Floor(value.AsDouble));
            return Task.FromResult(result);
        }

        private Task<Variant> RoundFunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            CheckParamCount(stack, 1);
            Variant value = variantOperations.Convert(GetParameter(stack, 0), VariantType.Double);
            Variant result = new Variant(System.Math.Round(value.AsDouble));
            return Task.FromResult(result);
        }

        private Task<Variant> TruncFunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            CheckParamCount(stack, 1);
            Variant value = variantOperations.Convert(GetParameter(stack, 0), VariantType.Double);
            Variant result = new Variant((int)value.AsDouble);
            return Task.FromResult(result);
        }

        private Task<Variant> CosFunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            CheckParamCount(stack, 1);
            Variant value = variantOperations.Convert(GetParameter(stack, 0), VariantType.Double);
            Variant result = new Variant(System.Math.Cos(value.AsDouble));
            return Task.FromResult(result);
        }

        private Task<Variant> SinFunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            CheckParamCount(stack, 1);
            Variant value = variantOperations.Convert(GetParameter(stack, 0), VariantType.Double);
            Variant result = new Variant(System.Math.Sin(value.AsDouble));
            return Task.FromResult(result);
        }

        private Task<Variant> TanFunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            CheckParamCount(stack, 1);
            Variant value = variantOperations.Convert(GetParameter(stack, 0), VariantType.Double);
            Variant result = new Variant(System.Math.Tan(value.AsDouble));
            return Task.FromResult(result);
        }

        private Task<Variant> SqrtFunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            CheckParamCount(stack, 1);
            Variant value = variantOperations.Convert(GetParameter(stack, 0), VariantType.Double);
            Variant result = new Variant(System.Math.Sqrt(value.AsDouble));
            return Task.FromResult(result);
        }

        private Task<Variant> EmptyFunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            CheckParamCount(stack, 1);
            Variant value = GetParameter(stack, 0);
            Variant result = new Variant(value.IsEmpty());
            return Task.FromResult(result);
        }

        private Task<Variant> NullFunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            CheckParamCount(stack, 0);
            Variant result = new Variant();
            return Task.FromResult(result);
        }

        private Task<Variant> ContainsFunctionCalculatorAsync(CalculationStack stack, IVariantOperations variantOperations)
        {
            CheckParamCount(stack, 2);
            Variant containerstr = variantOperations.Convert(GetParameter(stack, 0), VariantType.String);
            Variant substring = variantOperations.Convert(GetParameter(stack, 1), VariantType.String);

            if (containerstr.IsEmpty() || containerstr.IsNull())
            {
                return Task.FromResult(new Variant(false));
            }

            Variant result = new Variant(containerstr.AsString.IndexOf(substring.AsString) >= 0);
            return Task.FromResult(result);
        }
    }
}
