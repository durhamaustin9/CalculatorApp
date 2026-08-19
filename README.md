# QuickCalc

QuickCalc is a native desktop calculator built with modern C#, .NET 10, and Avalonia 12. It runs as a real application window on macOS, Windows, and Linux. Its compact design is inspired by the macOS Calculator while retaining its own visual identity.

## Features

- Addition, subtraction, multiplication, division, and percentage
- Decimal and negative-number support
- Friendly validation for invalid input and division by zero
- Chained operations and repeated equals
- Mouse and keyboard controls
- Native close/minimize controls and draggable title area
- Calculation logic separated from the interface

## Run locally

Install the [.NET 10 SDK](https://dotnet.microsoft.com/download), then run:

```bash
dotnet run --project src/CalculatorApp
```

The calculator opens as a native desktop window—there is no browser or local web server.

### Build a double-clickable macOS app

```bash
./scripts/build-macos-app.sh
open artifacts/QuickCalc.app
```

This creates `artifacts/QuickCalc.app`. The bundle uses the locally installed .NET 10 runtime; use Rider or `dotnet run` while developing.

## Run in JetBrains Rider

Open `CalculatorApp.slnx`, select the **CalculatorApp** project as the run target, and press **Run**. If Rider asks you to create a configuration, choose **.NET Project** and select `src/CalculatorApp/CalculatorApp.csproj`.

## Project layout

```text
src/CalculatorApp/
├── MainWindow.axaml             # Native calculator layout and styles
├── MainWindow.axaml.cs          # Keypad and keyboard behavior
├── Services/CalculatorEngine.cs # Calculation rules and result model
├── App.axaml                    # Application theme
└── Program.cs                   # Desktop application entry point
```
