namespace CalculatorApp.Services;

public sealed class CalculatorEngine
{
    public CalculationResult Calculate(
        decimal firstNumber,
        decimal secondNumber,
        CalculatorOperation operation)
    {
        if (secondNumber == 0 &&
            operation is CalculatorOperation.Divide or CalculatorOperation.Remainder)
        {
            return CalculationResult.Failure(
                operation == CalculatorOperation.Divide
                    ? "A number cannot be divided by zero."
                    : "A remainder cannot be calculated with zero.");
        }

        try
        {
            var value = operation switch
            {
                CalculatorOperation.Add => firstNumber + secondNumber,
                CalculatorOperation.Subtract => firstNumber - secondNumber,
                CalculatorOperation.Multiply => firstNumber * secondNumber,
                CalculatorOperation.Divide => firstNumber / secondNumber,
                CalculatorOperation.Remainder => firstNumber % secondNumber,
                _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, null)
            };

            return CalculationResult.Success(value);
        }
        catch (OverflowException)
        {
            return CalculationResult.Failure("That result is outside the supported number range.");
        }
    }
}

public enum CalculatorOperation
{
    Add,
    Subtract,
    Multiply,
    Divide,
    Remainder
}

public readonly record struct CalculationResult(decimal? Value, string? Error)
{
    public bool IsSuccess => Value.HasValue;

    public static CalculationResult Success(decimal value) => new(value, null);

    public static CalculationResult Failure(string error) => new(null, error);
}
