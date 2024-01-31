
public interface ILoanCalculator
{
    LoanCalculationResult Calculate(LoanCalculatorViewModel model);
    LoanCalculationResult CalculateCustom(CustomLoanCalculatorViewModel model);
}

