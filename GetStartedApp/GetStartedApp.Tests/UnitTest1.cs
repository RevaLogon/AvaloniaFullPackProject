using Avalonia.Controls;
using Avalonia.Headless;
using Avalonia.Threading;
using GetStartedApp.Views; // Ensure this namespace contains MainWindow
using GetStartedApp.ViewModels;
using Xunit;

public class CalculatorTests
{
    private MainWindow CreateWindow()
    {
        var window = new MainWindow
        {
            DataContext = new MainViewModel()
        };
        
        window.Show();
        return window;
    }

    [Theory]
    [InlineData("5", "3", "8", "AddButton")]
    [InlineData("10", "4", "6", "SubtractButton")]
    [InlineData("10", "2", "5", "DivideButton")]
    [InlineData("3", "2", "6", "MultiplyButton")]
    public async Task Should_Perform_Operation(string firstNumber, string secondNumber, string expectedResult, string buttonName)
    {
        var window = CreateWindow();

        // Setup controls:
        var firstNumberTextBox = window.FindControl<TextBox>("FirstNumberTextBox");
        var secondNumberTextBox = window.FindControl<TextBox>("SecondNumberTextBox");
        var operationButton = window.FindControl<Button>(buttonName);

        if (firstNumberTextBox == null || secondNumberTextBox == null || operationButton == null)
        {
            Assert.True(false, "One or more controls are not found.");
            return;
        }

        // Simulate input:
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            firstNumberTextBox.Text = firstNumber;
            secondNumberTextBox.Text = secondNumber;
        });

        // Simulate button click:
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            operationButton.Command?.Execute(null); // Execute command bound to the button
        });

        // Allow UI thread to process
        await Dispatcher.UIThread.InvokeAsync(() => { });

        // Assert result:
        var calculationLinesControl = window.FindControl<ItemsControl>("CalculationLinesControl");
        if (calculationLinesControl == null || calculationLinesControl.Items.Count == 0)
        {
            Assert.True(false, "Calculation lines control or items are missing.");
            return;
        }

        var resultTextBlock = (TextBlock)((StackPanel)calculationLinesControl.Items[0]).Children[2]; // Assuming results are in the first item

        if (resultTextBlock == null)
        {
            Assert.True(false, "Result text block is missing.");
            return;
        }

        Assert.Equal(expectedResult, resultTextBlock.Text);
    }
}
