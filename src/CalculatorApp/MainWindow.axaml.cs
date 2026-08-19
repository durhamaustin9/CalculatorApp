using System.Globalization;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using CalculatorApp.Services;

namespace CalculatorApp;

public sealed partial class MainWindow : Window
{
    private const int MaximumInputLength = 16;

    private readonly CalculatorEngine _engine = new();
    private string _entry = "0";
    private decimal? _accumulator;
    private CalculatorOperation? _pendingOperation;
    private CalculatorOperation? _lastOperation;
    private decimal? _lastOperand;
    private bool _startNewEntry = true;
    private bool _justEvaluated;
    private bool _hasError;
    private Button? _activeOperatorButton;

    public MainWindow()
    {
        InitializeComponent();
        AddHandler(KeyDownEvent, OnKeyDown, RoutingStrategies.Tunnel);
    }

    private void OnDigitClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button { Tag: string digit })
        {
            EnterDigit(digit);
        }
    }

    private void OnDecimalClick(object? sender, RoutedEventArgs e) => EnterDecimal();

    private void OnToggleSignClick(object? sender, RoutedEventArgs e) => ToggleSign();

    private void OnPercentClick(object? sender, RoutedEventArgs e) => ApplyPercent();

    private void OnBackspaceClick(object? sender, RoutedEventArgs e) => Backspace();

    private void OnClearClick(object? sender, RoutedEventArgs e) => ClearCalculator();

    private void OnOperatorClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button { Tag: string operationName } button &&
            Enum.TryParse<CalculatorOperation>(operationName, out var operation))
        {
            SelectOperation(operation, button);
        }
    }

    private void OnEqualsClick(object? sender, RoutedEventArgs e) => CalculateEquals();

    private void OnCloseClick(object? sender, RoutedEventArgs e) => Close();

    private void OnMinimizeClick(object? sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (TryHandleKeySymbol(e.KeySymbol))
        {
            e.Handled = true;
            return;
        }

        var handled = true;

        switch (e.Key)
        {
            case Key.D0 or Key.NumPad0: EnterDigit("0"); break;
            case Key.D1 or Key.NumPad1: EnterDigit("1"); break;
            case Key.D2 or Key.NumPad2: EnterDigit("2"); break;
            case Key.D3 or Key.NumPad3: EnterDigit("3"); break;
            case Key.D4 or Key.NumPad4: EnterDigit("4"); break;
            case Key.D5 or Key.NumPad5 when !e.KeyModifiers.HasFlag(KeyModifiers.Shift): EnterDigit("5"); break;
            case Key.D6 or Key.NumPad6: EnterDigit("6"); break;
            case Key.D7 or Key.NumPad7: EnterDigit("7"); break;
            case Key.D8 or Key.NumPad8 when !e.KeyModifiers.HasFlag(KeyModifiers.Shift): EnterDigit("8"); break;
            case Key.D9 or Key.NumPad9: EnterDigit("9"); break;
            case Key.OemPeriod or Key.Decimal: EnterDecimal(); break;
            case Key.Add: SelectOperation(CalculatorOperation.Add, AddButton); break;
            case Key.OemPlus when e.KeyModifiers.HasFlag(KeyModifiers.Shift): SelectOperation(CalculatorOperation.Add, AddButton); break;
            case Key.OemPlus: CalculateEquals(); break;
            case Key.OemMinus or Key.Subtract: SelectOperation(CalculatorOperation.Subtract, SubtractButton); break;
            case Key.Multiply: SelectOperation(CalculatorOperation.Multiply, MultiplyButton); break;
            case Key.D8 when e.KeyModifiers.HasFlag(KeyModifiers.Shift): SelectOperation(CalculatorOperation.Multiply, MultiplyButton); break;
            case Key.Divide or Key.OemQuestion: SelectOperation(CalculatorOperation.Divide, DivideButton); break;
            case Key.D5 when e.KeyModifiers.HasFlag(KeyModifiers.Shift): ApplyPercent(); break;
            case Key.Enter or Key.Return: CalculateEquals(); break;
            case Key.Back: Backspace(); break;
            case Key.Escape or Key.Delete: ClearCalculator(); break;
            default: handled = false; break;
        }

        e.Handled = handled;
    }

    private bool TryHandleKeySymbol(string? symbol)
    {
        switch (symbol)
        {
            case "+": SelectOperation(CalculatorOperation.Add, AddButton); return true;
            case "-" or "−": SelectOperation(CalculatorOperation.Subtract, SubtractButton); return true;
            case "*" or "×": SelectOperation(CalculatorOperation.Multiply, MultiplyButton); return true;
            case "/" or "÷": SelectOperation(CalculatorOperation.Divide, DivideButton); return true;
            case "%": ApplyPercent(); return true;
            case "." or ",": EnterDecimal(); return true;
            case "=": CalculateEquals(); return true;
            default: return false;
        }
    }

    private void EnterDigit(string digit)
    {
        PrepareForInput();

        if (_entry.Count(char.IsDigit) >= MaximumInputLength)
        {
            return;
        }

        if (_startNewEntry || _entry == "0")
        {
            _entry = digit;
            _startNewEntry = false;
        }
        else
        {
            _entry += digit;
        }

        _justEvaluated = false;
        UpdateDisplay();
    }

    private void EnterDecimal()
    {
        PrepareForInput();

        if (_startNewEntry)
        {
            _entry = "0.";
            _startNewEntry = false;
        }
        else if (!_entry.Contains('.'))
        {
            _entry += ".";
        }

        _justEvaluated = false;
        UpdateDisplay();
    }

    private void ToggleSign()
    {
        if (_hasError || _entry == "0")
        {
            return;
        }

        _entry = _entry.StartsWith('-') ? _entry[1..] : $"-{_entry}";
        UpdateDisplay();
    }

    private void ApplyPercent()
    {
        if (!TryReadEntry(out var value))
        {
            return;
        }

        _entry = FormatNumber(value / 100m);
        _startNewEntry = true;
        _justEvaluated = false;
        UpdateDisplay();
    }

    private void Backspace()
    {
        if (_hasError || _startNewEntry)
        {
            return;
        }

        _entry = _entry.Length <= 1 || _entry is "-0" ? "0" : _entry[..^1];
        if (_entry == "-")
        {
            _entry = "0";
        }

        UpdateDisplay();
    }

    private void SelectOperation(CalculatorOperation operation, Button button)
    {
        if (_hasError)
        {
            ClearCalculator();
            return;
        }

        if (!_startNewEntry && _pendingOperation.HasValue && _accumulator.HasValue)
        {
            if (!TryReadEntry(out var chainedOperand) ||
                !TryCalculate(_accumulator.Value, chainedOperand, _pendingOperation.Value))
            {
                return;
            }
        }
        else if (!TryReadEntry(out var currentValue))
        {
            return;
        }
        else
        {
            _accumulator = currentValue;
        }

        _pendingOperation = operation;
        _startNewEntry = true;
        _justEvaluated = false;
        ExpressionText.Text = $"{FormatNumber(_accumulator ?? 0m)} {GetSymbol(operation)}";
        SetActiveOperator(button);
    }

    private void CalculateEquals()
    {
        if (_hasError)
        {
            ClearCalculator();
            return;
        }

        if (_pendingOperation.HasValue && _accumulator.HasValue)
        {
            var rightOperand = _startNewEntry
                ? _accumulator.Value
                : TryReadEntry(out var entryValue) ? entryValue : 0m;

            var operation = _pendingOperation.Value;
            var leftOperand = _accumulator.Value;

            if (!TryCalculate(leftOperand, rightOperand, operation))
            {
                return;
            }

            _lastOperation = operation;
            _lastOperand = rightOperand;
            ExpressionText.Text = $"{FormatNumber(leftOperand)} {GetSymbol(operation)} {FormatNumber(rightOperand)} =";
            _pendingOperation = null;
        }
        else if (_justEvaluated && _lastOperation.HasValue && _lastOperand.HasValue &&
                 TryReadEntry(out var leftOperand))
        {
            var operation = _lastOperation.Value;
            var rightOperand = _lastOperand.Value;

            if (!TryCalculate(leftOperand, rightOperand, operation))
            {
                return;
            }

            ExpressionText.Text = $"{FormatNumber(leftOperand)} {GetSymbol(operation)} {FormatNumber(rightOperand)} =";
        }

        _accumulator = TryReadEntry(out var result) ? result : null;
        _startNewEntry = true;
        _justEvaluated = true;
        SetActiveOperator(null);
    }

    private bool TryCalculate(decimal left, decimal right, CalculatorOperation operation)
    {
        var calculation = _engine.Calculate(left, right, operation);
        if (!calculation.IsSuccess)
        {
            ShowError(calculation.Error ?? "Unable to calculate that result.");
            return false;
        }

        _entry = FormatNumber(calculation.Value.GetValueOrDefault());
        _accumulator = calculation.Value;
        UpdateDisplay();
        return true;
    }

    private void PrepareForInput()
    {
        if (_hasError)
        {
            ClearCalculator();
        }

        if (_justEvaluated && !_pendingOperation.HasValue)
        {
            _accumulator = null;
            _lastOperation = null;
            _lastOperand = null;
            ExpressionText.Text = "Ready";
        }
    }

    private void ClearCalculator()
    {
        _entry = "0";
        _accumulator = null;
        _pendingOperation = null;
        _lastOperation = null;
        _lastOperand = null;
        _startNewEntry = true;
        _justEvaluated = false;
        _hasError = false;
        ExpressionText.Text = "Ready";
        SetActiveOperator(null);
        UpdateDisplay();
    }

    private void ShowError(string message)
    {
        _entry = "Error";
        _accumulator = null;
        _pendingOperation = null;
        _startNewEntry = true;
        _justEvaluated = false;
        _hasError = true;
        ExpressionText.Text = message;
        SetActiveOperator(null);
        UpdateDisplay();
    }

    private void SetActiveOperator(Button? button)
    {
        _activeOperatorButton?.Classes.Remove("active");
        _activeOperatorButton = button;
        _activeOperatorButton?.Classes.Add("active");
    }

    private bool TryReadEntry(out decimal value) =>
        decimal.TryParse(_entry, NumberStyles.Number, CultureInfo.InvariantCulture, out value);

    private void UpdateDisplay()
    {
        DisplayText.Text = _entry;
        DisplayText.FontSize = _entry.Length switch
        {
            > 15 => 31,
            > 12 => 37,
            > 9 => 45,
            _ => 58
        };
    }

    private static string FormatNumber(decimal value) =>
        value.ToString("G29", CultureInfo.InvariantCulture);

    private static string GetSymbol(CalculatorOperation operation) => operation switch
    {
        CalculatorOperation.Add => "+",
        CalculatorOperation.Subtract => "−",
        CalculatorOperation.Multiply => "×",
        CalculatorOperation.Divide => "÷",
        CalculatorOperation.Remainder => "%",
        _ => "?"
    };
}
