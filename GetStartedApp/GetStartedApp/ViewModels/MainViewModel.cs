using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GetStartedApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string _firstNumber = string.Empty;
        private string _secondNumber = string.Empty;
        private ObservableCollection<CalculationLine> _calculationLines = [];

        public MainViewModel()
        {
            AddCommand = new RelayCommand(AddCalculationLine);
            SubtractCommand = new RelayCommand(CalculateSubtraction);
            MultiplyCommand = new RelayCommand(CalculateMultiplication);
            DivideCommand = new RelayCommand(CalculateDivision);
            ClearCommand = new RelayCommand(Clear);
            AddLineCommand = new RelayCommand(AddNewLine);
        }

        public string FirstNumber
        {
            get => _firstNumber;
            set => SetProperty(ref _firstNumber, value);
        }

        public string SecondNumber
        {
            get => _secondNumber;
            set => SetProperty(ref _secondNumber, value);
        }

        public ObservableCollection<CalculationLine> CalculationLines
        {
            get => _calculationLines;
            set => SetProperty(ref _calculationLines, value);
        }

        public ICommand AddCommand { get; }
        public ICommand SubtractCommand { get; }
        public ICommand MultiplyCommand { get; }
        public ICommand DivideCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand AddLineCommand { get; }

        private void AddCalculationLine()
        {
            if (double.TryParse(FirstNumber, out double firstNumber) &&
                double.TryParse(SecondNumber, out double secondNumber))
            {
                CalculationLines.Add(new CalculationLine
                {
                    FirstNumber = FirstNumber,
                    SecondNumber = SecondNumber,
                    Result = CalculateResult(firstNumber, secondNumber)
                });
            }
        }

        private void CalculateSubtraction()
        {
            if (double.TryParse(FirstNumber, out double firstNumber) &&
                double.TryParse(SecondNumber, out double secondNumber))
            {
                CalculationLines.Add(new CalculationLine
                {
                    FirstNumber = FirstNumber,
                    SecondNumber = SecondNumber,
                    Result = (firstNumber - secondNumber).ToString()
                });
            }
        }

        private void CalculateMultiplication()
        {
            if (double.TryParse(FirstNumber, out double firstNumber) &&
                double.TryParse(SecondNumber, out double secondNumber))
            {
                CalculationLines.Add(new CalculationLine
                {
                    FirstNumber = FirstNumber,
                    SecondNumber = SecondNumber,
                    Result = (firstNumber * secondNumber).ToString()
                });
            }
        }

        private void CalculateDivision()
        {
            if (double.TryParse(FirstNumber, out double firstNumber) &&
                double.TryParse(SecondNumber, out double secondNumber))
            {
                // Handle division by zero
                if (secondNumber != 0)
                {
                    CalculationLines.Add(new CalculationLine
                    {
                        FirstNumber = FirstNumber,
                        SecondNumber = SecondNumber,
                        Result = (firstNumber / secondNumber).ToString()
                    });
                }
                else
                {
                    CalculationLines.Add(new CalculationLine
                    {
                        FirstNumber = FirstNumber,
                        SecondNumber = SecondNumber,
                        Result = "Error: Division by zero"
                    });
                }
            }
        }

        private void AddNewLine()
        {
            CalculationLines.Add(new CalculationLine());
        }

        private void Clear()
        {
            CalculationLines.Clear();
        }

        private static string CalculateResult(double firstNumber, double secondNumber)
        {
            return (firstNumber + secondNumber).ToString();
        }
    }

    public class CalculationLine
    {
        public string FirstNumber { get; set; } = string.Empty;
        public string SecondNumber { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
    }
}
